using UnityEngine;
using TMPro;

public class DoubleDoor : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public Animator DoubleDoorRigthAnimator;
    private bool playerNearby = false;

    AudioSource audioSource;

    public AudioClip OpenSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            if (DialogueManager.hasTalkedToAlex)
            {
                DoubleDoorRigthAnimator.Play("LockCube");
                messageText.gameObject.SetActive(false);
                StartOpenSound();
            }
            else
            {
                messageText.text = "You must talk to Alex first!";
            }
        }
           
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "Press [F] interact with door";
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
