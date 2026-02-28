using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _CharacterController;
    private Vector3 _direction;
    private float _gravity = -9.81f;
    private float _velocity;
    private Camera _mainCamera;
    
    //[SerializeField] Expose private variables in the Unity Editor. 
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float speed;
    [SerializeField] private float gravityMultiplaier = 3.0f;
    [SerializeField] private float jumpPower;
    [SerializeField] private Movement movement;

    private PlayerStats playerStats;
    private AudioSource audioSource;


    private void Awake()
    {
        //Grabs the CharacterController component attached to the same GameObject (in this case the Player CharacterController) when the game starts.
        _CharacterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        playerStats = GetComponent<PlayerStats>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerStats != null && playerStats.IsDead())
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            return;
        }
            
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
        
    }



    private void ApplyGravity()
    {
        if (_CharacterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplaier * Time.deltaTime;
        }

        // Always apply vertical movement
        _direction.y = _velocity;
    }


    private void ApplyRotation(){
        
        if (playerStats != null && playerStats.IsDead()){
            return;
        }
            
        
        //ensures that rotation only happens when there’s input (if we do not do that the player will snap forward evry time coz targetAngle will be 0)
        if(_input.sqrMagnitude == 0)
        {
            return;
        }
        
        _direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement(){
        if (playerStats != null && playerStats.IsDead()){
            return;
        }
            

        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);

        _CharacterController.Move(_direction * movement.currentSpeed * Time.deltaTime);
    }



    public void Move(InputAction.CallbackContext context){
        if (playerStats != null && playerStats.IsDead())
        {
            _input = Vector2.zero;
            return;
        }

        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (playerStats != null && playerStats.IsDead()){
            return;
        }
            

        if (!context.started || !_CharacterController.isGrounded)
        {
            return;
        }

        _velocity = jumpPower;
    }


    public void Sprint(InputAction.CallbackContext context){
        if (playerStats != null && playerStats.IsDead())
        {
            movement.isSprinting = false;
            return;
        }

        movement.isSprinting = context.started || context.performed;
    }

}


[Serializable]
public struct Movement{
    public float speed;
    public float multiplier;
    public float acceleration;

    [HideInInspector] public bool isSprinting;
    [HideInInspector] public float currentSpeed;
}