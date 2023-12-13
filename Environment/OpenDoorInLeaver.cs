using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class OpenDoorInLeaver : MonoBehaviour
{
    [SerializeField] private GameObject DoorToOpen;
    [SerializeField] private GameObject HuskForLeaver;

    [SerializeField] private GameObject HUD;

    [SerializeField] private InputActionReference _openDoor;

    private PuzzleManager _puzzleManager;

    //Managers
    private AudioManager audioManager;
    private FMODEvents fmod;

    public bool _doorIsOpen = false;

    private bool _playerInLeaver = false;

    public bool PlayerInLeaver { get => _playerInLeaver; private set => _playerInLeaver = value; }

    private void OnDisable()
    {
        _openDoor.action.performed -= OnTriggerDoor;
    }



    private void OnTriggerDoor(InputAction.CallbackContext obj)
    {
        if(_doorIsOpen) 
        {

            DoorToOpen.GetComponent<Animator>().SetTrigger("Close");
            audioManager.PlayOneShot(fmod.barDoor, DoorToOpen.transform.position);
            _doorIsOpen= false;
        }

        if(!_doorIsOpen) 
        {
            DoorToOpen.GetComponent<Animator>().SetTrigger("Open");
            _puzzleManager.Leaver2Sparkles.SetActive(false);

            //Activate next sparkles
            _puzzleManager.SecondHuskSparkles.SetActive(true);
            _puzzleManager.PPlate2_1Sparkles.SetActive(true);
            _puzzleManager.PPlate2_2Sparkles.SetActive(true);

            audioManager.PlayOneShot(fmod.barDoor, DoorToOpen.transform.position);
            _doorIsOpen= true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HUD.SetActive(true);
            _openDoor.action.performed += OnTriggerDoor;
            //_playerInLeaver = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HUD.SetActive(false);
            _openDoor.action.performed -= OnTriggerDoor;
            //audioManager.PlayOneShot(fmod.barDoor, DoorToOpen.transform.position);
            //_playerInLeaver = false;
        }
    }

    private void Start()
    {
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
        _puzzleManager = PuzzleManager.PMInstance;
    }



}
