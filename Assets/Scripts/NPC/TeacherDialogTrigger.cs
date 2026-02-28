using UnityEngine;
using TMPro;

public class TeacherDialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public ManageChoice choiceManager;


    private bool playerInRange = false;
    private bool dialogueStarted = false;

    public TextMeshProUGUI InteractButton;
    public RectTransform backgroundBox;


    void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.F))
        {
            InteractButton.text = "Next [F]";
            
            StartDialogue();
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
            choiceManager.HideAllButtons();
            playerInRange = false;
            dialogueStarted = false;
        }
    }


    
}

[System.Serializable]
public class TeacherMessage{
    public int actorId;
    public string message;
}

[System.Serializable]
public class TeacherActor{
    public string name;
    public Sprite sprite;
}



