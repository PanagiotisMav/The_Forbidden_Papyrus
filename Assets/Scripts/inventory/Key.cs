using UnityEngine;

public class Key : MonoBehaviour
{
    public Inventory inventory;
    AudioSource audioSource;
    public AudioClip KeysSounds;
    public UnityEngine.Audio.AudioMixerGroup mixerGroup;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound();
            inventory.AddItem("Key");
            Destroy(gameObject);
        }
    }

    void PlaySound()
    {
        // Create a temporary GameObject for the sound
        GameObject soundObj = new GameObject("TempAudio");
        AudioSource source = soundObj.AddComponent<AudioSource>();
        source.clip = KeysSounds;
        source.volume = 0.1f;
        source.spatialBlend = 0f; // 0 means 2D sound

        source.outputAudioMixerGroup = mixerGroup;

        source.Play();

        // Destroy sound object after sound finishes
        Destroy(soundObj, KeysSounds.length);
    }

}
