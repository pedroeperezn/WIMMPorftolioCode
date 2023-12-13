using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    private Animator DoorAnimator;

    public void DoOpenDoor()
    {
        DoorAnimator.SetTrigger("Open");
    }


    public void CloseDoor()
    { 
        DoorAnimator.SetTrigger("Close");
    
    }

    private void Start()
    {
        DoorAnimator = GetComponent<Animator>();
    }

}
