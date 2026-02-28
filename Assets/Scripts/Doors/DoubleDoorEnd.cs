using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DoubleDoorEnd : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Animator DoubleDoorRigthAnimator;
    public AudioClip closeSound;
    public Inventory playerInventory;
    public Image fadeImage;
    public float fadeDuration = 3f;  

    private AudioSource audioSource;
    private bool playerNearby = false;
    private bool hasExitedOnce = false;
    private bool isFading = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F) && !isFading)
        {
            if (playerInventory.HasItem("Diploma"))
            {
                StartCoroutine(FadeOutAndLoadScene(2));
            }
            else
            {
                messageText.text = "You need the Forbidden Papyrus to exit!";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "Press [F] to interact with door";
            messageText.gameObject.SetActive(true);
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(false);
            playerNearby = false;

            if (!hasExitedOnce)
            {
                DoubleDoorRigthAnimator.Play("LockCubeCloed");
                CloseOpenSound();
                hasExitedOnce = true;
            }
        }
    }

    public void CloseOpenSound()
    {
        if (audioSource.clip != closeSound)
        {
            audioSource.Stop();
            audioSource.clip = closeSound;
            audioSource.Play();
        }
    }

    private IEnumerator FadeOutAndLoadScene(int sceneIndex)
    {
        isFading = true;

        float t = 0;
        Color originalColor = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, normalizedTime);
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
