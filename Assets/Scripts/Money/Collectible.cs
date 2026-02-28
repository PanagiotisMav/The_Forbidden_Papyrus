using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip ChaChing;

    public static event Action OnCollected;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        transform.localRotation = Quaternion.Euler(180f, Time.time * 100f, -90f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound();
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }

    void PlaySound()
    {
        // Create a temporary GameObject for the sound
        GameObject soundObj = new GameObject("TempAudio");
        AudioSource source = soundObj.AddComponent<AudioSource>();
        source.clip = ChaChing;
        source.volume = 0.1f;
        source.spatialBlend = 0f;

        source.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;

        source.Play();

        // Destroy sound object after sound finishes
        Destroy(soundObj, ChaChing.length);
    }

}
