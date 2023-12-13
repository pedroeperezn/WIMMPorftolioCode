/*using Sirenix.OdinInspector.Editor.GettingStarted;
using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;

public class SolveFirstDoorPuzzle : MonoBehaviour
{
    private bool _hasOpened = false;
    
    //Managers
    PuzzleManager PuzzleManager;
    AudioManager audioManager = AudioManager.AMInstance;
    FMODEvents fmod = FMODEvents.FMInstance;


    private void Update()
    {
        if (PuzzleManager.PressurePlate1Solved && this.GetComponent<ComboCheck>().comboTriggered)
        {
            GetComponent<OpenDoor>().DoOpenDoor();

            if (!_hasOpened)
            {
                PuzzleManager.KalimbaDoor1Solved = true;
                PuzzleManager.KalimbaDoor1Sparkles.SetActive(false);
                audioManager.PlayOneShot(fmod.normalDoor, transform.position);
                _hasOpened = true;
            }

        }

        else 
        {
            GetComponent<ComboCheck>().comboTriggered = false;
        }

    }

    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }
}


