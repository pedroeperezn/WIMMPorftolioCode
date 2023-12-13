using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFinalDoor : MonoBehaviour
{
    private PuzzleManager PuzzleManager;
    private bool _hasOpen = false;

    //Managers
    private AudioManager audioManager;
    private FMODEvents fmod;

    void Update()
    {
        if (GetComponent<ComboCheck>().comboTriggered && PuzzleManager.CheckerboardSolved) 
        { 
            GetComponent<Animator>().SetTrigger("Open");
            if (!_hasOpen)
            {
                audioManager.PlayOneShot(fmod.normalDoor, transform.position);
                _hasOpen = true;
                PuzzleManager.finalDoorSolved = true;
                PuzzleManager.FinalDoorSparkles.SetActive(false);
            }
        }
    }

    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }
}
