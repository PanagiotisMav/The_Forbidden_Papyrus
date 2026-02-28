using UnityEngine;

public class FootSteps : MonoBehaviour
{
    AudioSource audioSource;
    CharacterController characterController;

    public AudioClip WalkingSound;
    public AudioClip RunningSound;
    public AudioClip JumpingSound;

    bool hasPlayedJumpSound = false;
    bool wasGroundedLastFrame = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded && !wasGroundedLastFrame)
        {
            ResetJumpSound();
        }

        wasGroundedLastFrame = characterController.isGrounded;
    }

    public void StartWalkingSound(){
        if (audioSource.clip != WalkingSound || !audioSource.isPlaying) {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = WalkingSound;
            audioSource.Play();
        }
    }

    public void StartRunningSound(){
        if (audioSource.clip != RunningSound) {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = RunningSound;
            audioSource.Play();
        }
    }

    public void StartJumpingSound(){
        if (!hasPlayedJumpSound) {
            audioSource.PlayOneShot(JumpingSound);
            hasPlayedJumpSound = true;
        }
    }

    public void ResetJumpSound(){
        hasPlayedJumpSound = false;
    }


    public void StopSound(){
        audioSource.Stop();
    }
}
