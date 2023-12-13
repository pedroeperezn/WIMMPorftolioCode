using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaverCombinationCheck : MonoBehaviour
{
    [SerializeField] private GameObject LeaverHusk;
    [SerializeField] private GameObject DoorToOpen;

    private PuzzleManager PuzzleManager;
    private GameManager gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Husks")
        {
            PuzzleManager.BarDoor1Opened = true;
            DoorToOpen.GetComponent<OpenDoor>().DoOpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Husks")
        {
            DoorToOpen.GetComponent<OpenDoor>().CloseDoor();
        }
    }

    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
        gameManager = GameManager.Instance;
    }

}
