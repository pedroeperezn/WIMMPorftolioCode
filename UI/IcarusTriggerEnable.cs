using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UIComponents;
using UnityEngine;
using UnityEngine.InputSystem;

public class IcarusTriggerEnable : MonoBehaviour
{

    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject IcarusUI;

    [SerializeField] private InputActionReference _openPuzzle;
    [SerializeField] private InputActionReference _closePuzzle;

    [SerializeField] private SwitchActionMaps _switchMap;
    //[SerializeField] private GameObject _ListenerContainer;
    //private InputActionListener _InputListener;

    private AudioManager audioManager;
    private FMODEvents fmod;

    private bool displayed;

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
        if (HUD != null) 
        {
            HUD.SetActive(false);
        }
        displayed = false;
    }

    private void Start()
    {
        //_InputListener = _ListenerContainer.GetComponent<InputActionListener>();
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            HUD.SetActive(true);
            //_InputListener.enabled = true;
            _openPuzzle.action.performed += OnOpenPuzzle;
            _closePuzzle.action.performed += OnClosePuzzle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HUD.SetActive(false);
            //_InputListener.enabled = false;
            _openPuzzle.action.performed -= OnOpenPuzzle;
            _closePuzzle.action.performed -= OnClosePuzzle;

            displayed = false;
        }
    }

    private void OnOpenPuzzle(InputAction.CallbackContext obj)
    {
        if (!displayed)
        {
            audioManager.PlayOneShot(fmod.Interact, this.transform.position);
            HUD.SetActive(false);
            displayed = true;
            IcarusUI.SetActive(true);
            _switchMap.SwitchMap("Puzzle");
        }


    }

    private void OnClosePuzzle(InputAction.CallbackContext obj)
    {
        if (displayed)
        {
            audioManager.PlayOneShot(fmod.Interact, this.transform.position);
            HUD.SetActive(true);
            displayed = false;
            IcarusUI.SetActive(false);
            _switchMap.SwitchMap("Player");

        }

    }

    
}
