using UnityEngine;
using TMPro;

public class LockDoor : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Animator lockDoorAnimator;
    private bool playerNearby = false;
    private bool isUnlocked = false;
    public Inventory playerInventory;

    AudioSource audioSource;
    public AudioClip OpenSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F) && !isUnlocked)
        {
            if (playerInventory.HasItem("Key"))
            {
                lockDoorAnimator.Play("LockCubeWizad");
                messageText.gameObject.SetActive(false);
                StartOpenSound();
                isUnlocked = true;
            }else
            {
                messageText.text = "You need a key!";
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            messageText.text = "locked";
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
        }
    }

    public void StartOpenSound(){
        if (audioSource.clip != OpenSound) {
            audioSource.Stop();
            audioSource.clip = OpenSound;
            audioSource.Play();
        }
    }
}
