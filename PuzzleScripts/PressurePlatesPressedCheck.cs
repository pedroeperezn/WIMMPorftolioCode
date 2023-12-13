using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatesPressedCheck : MonoBehaviour
{
    
    private PuzzleManager PuzzleManager;
    private GameManager gameManager;

    [SerializeField] private bool playerInPosition = false;
    [SerializeField] private bool huskInPosition = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInPosition = true;
            float distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("Player");


            if (distance < 0.2f)
            {
                playerInPosition = true;
            }
        }

        else if (other.gameObject.tag == "Husks")
        {

            huskInPosition = true;
            float distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("Husk");

            if (distance < 0.2f)
            {
                huskInPosition = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //playerInPosition = false;
            float distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("Player Exit");
            if (distance > 0.2f)
            {
                playerInPosition = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (huskInPosition && playerInPosition)
        {
            PuzzleManager.PressurePlate2Solved = true;
            Debug.Log("Open Chest lid");

        }
    }

    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
        gameManager = GameManager.Instance;
        
    }
}
