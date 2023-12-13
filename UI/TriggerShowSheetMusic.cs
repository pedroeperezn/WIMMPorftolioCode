using System.Collections;
using System.Collections.Generic;
using UIComponents;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerShowSheetMusic : MonoBehaviour
{
    [SerializeField] private GameObject _sheetMusicUI;
    [SerializeField] private GameObject HUD;

    [SerializeField] private InputActionReference _openPuzzle;
    [SerializeField] private InputActionReference _closePuzzle;

    private bool displayed;

    [SerializeField] private SwitchActionMaps _switchMap;
    [SerializeField] private GameObject _kalimbaActionListener;

    private PuzzleManager _puzzleManager;
    private AudioManager audioManager;
    private FMODEvents fmod;

    private void OnEnable()
    {
        _openPuzzle.action.performed -= OnOpenPuzzle;
        _closePuzzle.action.performed -= OnClosePuzzle;
        displayed = false;
    }

    private void OnDisable()
    {
        _openPuzzle.action.performed -= OnOpenPuzzle;
        _closePuzzle.action.performed -= OnClosePuzzle;
        displayed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            _kalimbaActionListener.SetActive(false);
            HUD.SetActive(true);
            _openPuzzle.action.performed += OnOpenPuzzle;
            _closePuzzle.action.performed += OnClosePuzzle;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _kalimbaActionListener.SetActive(true);
            HUD.SetActive(false);
            _openPuzzle.action.performed -= OnOpenPuzzle;
            _closePuzzle.action.performed -= OnClosePuzzle;

            displayed = false;

        }
    }

    private void OnOpenPuzzle(InputAction.CallbackContext obj)
    {
        if (!displayed)
        {
            HUD.SetActive(false);
            audioManager.PlayOneShot(fmod.Interact, this.transform.position);
            _puzzleManager.FirstSheetSparkles.SetActive(false);
            displayed = true;
            _sheetMusicUI.SetActive(true);
            _switchMap.SwitchMap("Puzzle");
        }


    }

    private void OnClosePuzzle(InputAction.CallbackContext obj)
    {
        if (displayed)
        {
            HUD.SetActive(true);
            audioManager.PlayOneShot(fmod.Interact, this.transform.position);
            displayed = false;
            _sheetMusicUI.SetActive(false);
            _switchMap.SwitchMap("Player");

        }

    }

    private void Start()
    {
        _puzzleManager = PuzzleManager.PMInstance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

}
