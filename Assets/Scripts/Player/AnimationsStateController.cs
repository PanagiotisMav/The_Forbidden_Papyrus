using UnityEngine;

public class AnimationsStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    
    int idleToJumpHash;
    int walkToJumpHash;
    int runToJumpHash;
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        
        idleToJumpHash = Animator.StringToHash("idleToJump");
        walkToJumpHash = Animator.StringToHash("walkToJump");
        runToJumpHash = Animator.StringToHash("runToJump");
        
    }

    
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        
        
        if (!isWalking && forwardPressed){ 
            animator.SetBool(isWalkingHash, true); 
        } 
        if (isWalking && !forwardPressed){  
            animator.SetBool(isWalkingHash, false);
        }


        if(!isRunning && (forwardPressed && runPressed)){
            animator.SetBool(isRunningHash, true);
        }
        if(isRunning && (!forwardPressed || !runPressed)){
            animator.SetBool(isRunningHash, false);
        }

        
        // Idle to Jump
        if (!isWalking && !isRunning && jumpPressed)
        {
            animator.SetBool(idleToJumpHash, true);
        }
        else
        {
            animator.SetBool(idleToJumpHash, false);
        }

        // Walk to Jump logic
        if (isWalking && !isRunning && jumpPressed)
        {
            animator.SetBool(walkToJumpHash, true);
        }
        else
        {
            animator.SetBool(walkToJumpHash, false);
        }

        // Run to Jump
        if (isRunning && jumpPressed){
            animator.SetBool(runToJumpHash, true);
        }
        else{
            animator.SetBool(runToJumpHash, false);
        }
        //
    }
}
