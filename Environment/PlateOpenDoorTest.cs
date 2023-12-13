using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateOpenDoorTest : MonoBehaviour
{

    [SerializeField] private GameObject DoorToOpen;
    [SerializeField] private GameObject Bookshelf;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            //playerInPosition = true;
            Debug.Log("LEaver detected Husk");
            DoorToOpen.GetComponent<OpenDoor>().DoOpenDoor();

            Bookshelf.GetComponent<Animator>().SetTrigger("Rotated");
        }


    }

/*    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //playerInPosition = false;
            DoorToOpen.GetComponent<OpenDoor>().CloseDoor();

        }
    }*/
}
