using System;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public int doorState = 0; //state of the door
    public Animator doorAnimator;

    private void Start()
    {
        doorState = 0;
        Debug.Log("Door State = " + GetDoorState());
                
    }


    public void AnimationPlay(String stage)
    {
        switch (stage)
        {
            case "DoorOpenFromOUTside":
                doorAnimator.Play("DoorOpenFromOUTside");
                break;
            case "DoorClosedFromOUTside":
                doorAnimator.Play("DoorClosedFromOUTside");
                break;
            case "DoorOpenFromINside":
                doorAnimator.Play("DoorOpenFromINside");
                break;
            case "DoorClosedFromINside":
                doorAnimator.Play("DoorClosedFromINside");
                break;
            default:
                break;
        }
    }


    public int GetDoorState(){
        return doorState;
    }

    public void SetDoorState(int number){
        doorState = number;
    }
}
