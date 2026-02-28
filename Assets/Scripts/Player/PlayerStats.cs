using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public GameObject deathCanvas;
    private bool shouldFade = false;


    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetSlider(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetSlider(currentHealth);
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isDead = true;
        animator.SetTrigger("Die");

        if (MusicManager.Instance != null)
        {
            if (MusicManager.Instance.ambientMusic != null)
                MusicManager.Instance.ambientMusic.Stop();

            if (MusicManager.Instance.chaseMusic != null)
                MusicManager.Instance.chaseMusic.Stop();

            if (MusicManager.Instance.skeletonMusic != null)
                MusicManager.Instance.skeletonMusic.Stop();
        }
        

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
        
    }



    public bool IsDead()
    {
        return isDead;
    }


}
