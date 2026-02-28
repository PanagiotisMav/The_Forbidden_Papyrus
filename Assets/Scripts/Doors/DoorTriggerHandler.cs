using UnityEngine;
using TMPro;

public class DoorTriggerHandler : MonoBehaviour
{

    public TextMeshProUGUI messageText;
    public DoorManager doorManager;


    private bool playerNearby = false;
    public bool Action = false;
    
    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            // Check if the current trigger is the outside or inside one and change the door state
            if (gameObject.CompareTag("DoorAnimationOUTside"))
            {
                if(doorManager.GetDoorState() == 0){
                    doorManager.SetDoorState(1);
                    doorManager.AnimationPlay("DoorOpenFromOUTside");
                }
                else if (doorManager.GetDoorState() == 1){
                    doorManager.SetDoorState(0);
                    doorManager.AnimationPlay("DoorClosedFromOUTside");
                }
                else if (doorManager.GetDoorState() == 2){
                    doorManager.SetDoorState(0);
                    doorManager.AnimationPlay("DoorClosedFromINside");
                }
            }
            else if (gameObject.CompareTag("DoorAnimationINside"))
            {
                if(doorManager.GetDoorState() == 0){
                    doorManager.SetDoorState(2);
                    doorManager.AnimationPlay("DoorOpenFromINside");
                }
                else if (doorManager.GetDoorState() == 1){
                    doorManager.SetDoorState(0);
                    doorManager.AnimationPlay("DoorClosedFromOUTside");
                }
                else if (doorManager.GetDoorState() == 2){
                    doorManager.SetDoorState(0);
                    doorManager.AnimationPlay("DoorClosedFromINside");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "Press [F] interact with door";
            messageText.gameObject.SetActive(true);
            Action = true;
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.gameObject.SetActive(false);
            Action = false;
            playerNearby = false;
        }
    }

}
