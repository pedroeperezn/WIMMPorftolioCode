using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MusicButtonsCombinationCheck : MonoBehaviour
{
    [SerializeField] private GameObject DoorToOpen;

    private GameManager gameManager;
    
    private PuzzleManager PuzzleManager;

    private bool _musicButtonsSolved = false;

    public void MusicButtonsComboOk()
    {

        if (this.GetComponent<ComboCheck>().comboTriggered && PuzzleManager.BarDoor1Opened)
        {
            if (!_musicButtonsSolved)
            {
                gameManager.DisableKalimba();
            }

            _musicButtonsSolved = true;

            DoorToOpen.GetComponent<OpenDoor>().DoOpenDoor();
            //PuzzleManager.FloorNotesSolved = true;
        }

        else 
        {
            this.GetComponent<ComboCheck>().comboTriggered = false;
        }

        
    }

    private void FixedUpdate()
    {
        MusicButtonsComboOk();
    }

    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
        gameManager = GameManager.Instance;
    }
}
