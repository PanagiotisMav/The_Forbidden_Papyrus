using UnityEngine;
using TMPro;

public class ZombieDialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    private bool playerInRange = false;
    private bool dialogueStarted = false;

    public TextMeshProUGUI InteractButton;
    public RectTransform backgroundBox;

    public CollectibleCount collectibleCounter;

    void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.F))
        {
            if (collectibleCounter.GetCount() >= 4)
            {
                InteractButton.text = "Next [F]";
                StartDialogue();
            }
            else
            {
                InteractButton.text = "Gather the four memory pieces";
            }
        }
    }

    public void StartDialogue(){
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
        dialogueStarted = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.text = "Press [F] to Talk";
            InteractButton.gameObject.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            InteractButton.gameObject.SetActive(false);
            playerInRange = false;
            dialogueStarted = false;
        }
    }
}

[System.Serializable]
public class ZombieMessage{
    public int actorId;
    public string message;
}

[System.Serializable]
public class ZombieActor{
    public string name;
    public Sprite sprite;
}
