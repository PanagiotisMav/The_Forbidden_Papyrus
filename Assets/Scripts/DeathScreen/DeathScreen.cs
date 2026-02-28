using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    
    public AudioClip DeathSound;
    private AudioSource audioSource;


    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    public void PlayDeathSound(){
        audioSource.clip = DeathSound;
        audioSource.Play();
    }

    
}
