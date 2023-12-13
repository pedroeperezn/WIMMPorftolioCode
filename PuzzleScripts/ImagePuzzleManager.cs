using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UIComponents;

public class ImagePuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject _puzzleUI;
    [SerializeField] private GameObject _puzzleTrigger;
    [SerializeField] private SwitchActionMaps _switchActionMaps;
    [SerializeField] public GameObject[] imageArray;
    private PuzzleManager PuzzleManager;

    [SerializeField] public bool isIcarus;
    [SerializeField] public bool isCheckerboard;

    private bool puzzleSolved;
    private bool hasCorrectPlayed = false;
    private bool allCorrect;

    //Managers
    private AudioManager audioManager;
    private FMODEvents fmod;



    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

    private void PlayCorrectSound()
    {
        if (!hasCorrectPlayed)
        {
            audioManager.PlayOneShot(fmod.CorrectAnswer, transform.position);
        }

        hasCorrectPlayed = true;
    }

    void Update()
    {

        allCorrect = true;

        if (isIcarus)
        {

            foreach (var image in imageArray)
            {
                if (!image.GetComponent<CheckIcarusImage>().isImageCorrect)
                {
                    allCorrect = false;
                    break;
                }
            }

            if (allCorrect)
            {
                puzzleSolved = true;
                PuzzleManager.IcarusSolved = true;
                _puzzleUI.SetActive(false);
                _puzzleTrigger.SetActive(false);
                _switchActionMaps.SwitchMap("Player");
                PlayCorrectSound();
            }
        }

        if (isCheckerboard)
        {

            //separate checkerboard checker script
            foreach (var image in imageArray)
            {
                if (!image.GetComponent<CheckCheckerboardImage>().isImageCorrect)
                {
                    allCorrect = false;
                    break;
                }
            }

            if (allCorrect)
            {
                puzzleSolved = true;
                PuzzleManager.CheckerboardSolved = true;
                PuzzleManager.CheckersSparkles.SetActive(false);
                PuzzleManager.FinalDoorSparkles.SetActive(true);
                _puzzleUI.SetActive(false);
                _puzzleTrigger.SetActive(false);
                _switchActionMaps.SwitchMap("Player");
                PlayCorrectSound();
            }
        }
    }
}
