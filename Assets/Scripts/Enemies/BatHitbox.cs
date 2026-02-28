using UnityEngine;

public class BatHitbox : MonoBehaviour
{
    public int damage = 20;
    private bool canDamage = false;
    private bool alreadyHit = false;

    [Header("Sound")]
    public AudioClip hitSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDamage && !alreadyHit && other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }

            PlayHitSound();
            alreadyHit = true;// Only hit once per swing
        }
    }

    public void EnableDamage()
    {
        canDamage = true;
        alreadyHit = false;// Reset when starting a new attack
    }

    public void DisableDamage()
    {
        canDamage = false;
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
