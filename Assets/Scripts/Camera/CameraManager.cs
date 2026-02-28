using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private float _distanceToPlayer;
    private Vector2 _input;
    private CameraRotation _cameraRotation;

    [SerializeField] private Transform target;
    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;

    private PlayerStats playerStats;
    
    private float currentDistance;
    private float desiredDistance;  

    private void Start()
    {
        currentDistance = _distanceToPlayer;

        GameObject playerObject = GameObject.FindWithTag("Player");
        playerStats = playerObject.GetComponent<PlayerStats>();

        // Load saved mouse sensitivity
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            mouseSensitivity.horizontal = savedSensitivity;
            mouseSensitivity.vertical = savedSensitivity;
        }
    }

    public void RefreshSensitivity()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            mouseSensitivity.horizontal = savedSensitivity;
            mouseSensitivity.vertical = savedSensitivity;
            Debug.Log("Sensitivity refreshed: " + savedSensitivity);
        }
    }



    private void Awake()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, target.position);
    }

    public void Look(InputAction.CallbackContext context){
        _input = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (playerStats != null && playerStats.IsDead())
        {
            return; // Don't allow camera movement if player is dead
        }
        
        _cameraRotation.Yaw += _input.x * mouseSensitivity.horizontal * Time.deltaTime;
        _cameraRotation.Pitch += _input.y* -mouseSensitivity.vertical * Time.deltaTime;
        _cameraRotation.Pitch = Mathf.Clamp(_cameraRotation.Pitch, cameraAngle.min, cameraAngle.max);
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(_cameraRotation.Pitch, _cameraRotation.Yaw, 0.0f);

        Vector3 targetOffset = target.position + Vector3.up * 1.5f;
        desiredDistance = _distanceToPlayer;

        // Check for wall collision
        RaycastHit hit;
        float radius = 0.3f;
        if (Physics.SphereCast(targetOffset, radius, -transform.forward, out hit, _distanceToPlayer))
        {
            desiredDistance = hit.distance - 0.2f;
        }

        desiredDistance = Mathf.Clamp(desiredDistance, 0.5f, _distanceToPlayer);
        
        // Smooth the distance
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * 10f);

        // Update camera position
        transform.position = targetOffset - transform.forward * currentDistance;
    }

    
}

[Serializable]
public struct MouseSensitivity{
    public float horizontal;
    public float vertical;
}

public struct CameraRotation{
    public float Pitch;
    public float Yaw;
}

[Serializable]
public struct CameraAngle{
    public float min;
    public float max;
}