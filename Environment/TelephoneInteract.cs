using System;
using System.Collections;
using System.Collections.Generic;
using UIComponents;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class TelephoneInteract : MonoBehaviour
{

    [SerializeField] private GameObject HUD;

    [SerializeField] private GameObject DialogueBox;

    [SerializeField] private InputActionReference _openInteraction;
    [SerializeField] private InputActionReference _closeInteraction;

    private bool interacting;

    [SerializeField] private SwitchActionMaps _switchMap;

    private EventInstance telephoneInstance;

    private PuzzleManager _puzzleManager;
    private FMODEvents fmod;
    private AudioManager audioManager;

    private void OnEnable()
    {
        _openInteraction.action.performed -= OnOpenInteraction;
        _closeInteraction.action.performed -= OnCloseInteraction;
        interacting = false;
    }


    private void OnDisable()
    {
        _openInteraction.action.performed -= OnOpenInteraction;
        _closeInteraction.action.performed -= OnCloseInteraction;
        interacting = false;

        telephoneInstance.release();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HUD.SetActive(true);
            _openInteraction.action.performed += OnOpenInteraction;
            _closeInteraction.action.performed += OnCloseInteraction;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HUD.SetActive(false);
            _openInteraction.action.performed -= OnOpenInteraction;
            _closeInteraction.action.performed -= OnCloseInteraction;
            interacting = false;

        }
    }


    private void OnOpenInteraction(InputAction.CallbackContext obj)
    {
        if (!interacting)
        {
            HUD.SetActive(false);
            interacting = true;
            telephoneInstance.start();
            audioManager.PlayOneShot(fmod.Interact, this.transform.position);
            DialogueBox.SetActive(true);
            _switchMap.SwitchMap("Puzzle");
        }
    }
    private void OnCloseInteraction(InputAction.CallbackContext obj)
    {
        HUD.SetActive(true) ;
        audioManager.PlayOneShot(fmod.Interact, this.transform.position);
        telephoneInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        interacting = false;
        DialogueBox.SetActive(false);
        _switchMap.SwitchMap("Player");
    }


    // Start is called before the first frame update
    void Start()
    {
        _puzzleManager = PuzzleManager.PMInstance;
        fmod = FMODEvents.FMInstance;
        audioManager = AudioManager.AMInstance;

        telephoneInstance = audioManager.CreateEventInstance(fmod.Phone);
    }

}
