using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public TextMeshProUGUI InteractButton;
    public ManageChoice choiceManager;


   
    public PlayerStats playerStats;
    public TMP_Text warningText;
    public static bool hasTalkedToAlex = false;
    public GameObject objectToReveal;
    public CollectibleCount collectibleCounter; 




    Message[] currentMessages;
    Actor[] currentActor;
    int activeMessage = 0;
    public static bool isActive = false;
    private string currentActorName;

    public Inventory playerInventory;
    private bool choiceMade = false;

    public CameraManager cameraManager;
    public PlayerController playerController;


    public void OpenDialogue(Message[] messages, Actor[] actors){
        currentMessages = messages;
        currentActor = actors;
        activeMessage = 0;
        isActive = true;

        currentActorName = actors[messages[2].actorId].name;

        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
    }

    public void DisplayMessage(){
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActor[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        AnimateTextColor();
    }
    
    public void NextMessage()
    {
        
        if (activeMessage == 9 && currentActorName == "Skeleton" && !choiceMade)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (cameraManager != null)
            cameraManager.enabled = false;
            if (playerController != null)
            playerController.enabled = false;
            choiceManager.ShowAllButtons();
            Debug.Log("Choice must be made before continuing.");
            return;
        }

        choiceManager.HideAllButtons();

        activeMessage++;

        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            InteractButton.gameObject.SetActive(false);
            choiceManager.HideAllButtons();
            isActive = false;

            if (currentActorName == "Wizard")
            {
                playerInventory.AddItem("Scroll");
            }

            if (currentActorName == "Alex")
            {
                hasTalkedToAlex = true;
            }
            if (currentActorName == "Skeleton" && objectToReveal != null)
            {
                objectToReveal.SetActive(true);
            }
            if (currentActorName == "Zombie")
            {
                playerInventory.AddItem("Diploma");
                Debug.Log("Diploma awarded by the Secretariat.");

                if (collectibleCounter != null)
                {
                    collectibleCounter.Subtract(1000);
                }
            }

        }
    }

    public void AnimateTextColor()
    {
        Color originalColor = messageText.color;
        originalColor.a = 0;
        messageText.color = originalColor;

        LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((float val) => {
            Color c = messageText.color;
            c.a = val;
            messageText.color = c;
        });
    }

    public void YouWin(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (cameraManager != null)
        cameraManager.enabled = true;
        if (playerController != null)
        playerController.enabled = true;
        choiceMade=true;
        NextMessage();
    }


    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        objectToReveal.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isActive)
        {
            NextMessage();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isActive)
        {
            if (currentActorName == "Skeleton" && playerStats != null && playerInventory.HasItem("Scroll"))
            {
                warningText.text = "You tried to defy the Skeleton with forbidden knowledge...";
                warningText.fontSize = 25;
                playerStats.Die();
                Debug.Log("You pressed Tab during a lesson with the Teacher while holding the Scroll. Fatal mistake.");
            }
        }
    }



}
