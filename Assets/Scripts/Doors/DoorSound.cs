using UnityEngine;

public class DoorSound : MonoBehaviour
{
    
    AudioSource audioSource;

    public AudioClip OpenSound;
    public AudioClip ClosedSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    public void StartOpenSound(){
        if (audioSource.clip != OpenSound) {
            audioSource.Stop();
            audioSource.clip = OpenSound;
            audioSource.Play();
        }
    }

    public void StartClosedSound(){
        if (audioSource.clip != ClosedSound) {
            audioSource.Stop();
            audioSource.clip = ClosedSound;
            audioSource.Play();
        }
    }


}
