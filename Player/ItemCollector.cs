using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemCollector : MonoBehaviour
{
   
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject ItemUI;
    [SerializeField] private GameObject ItemEnabledUI;
    [SerializeField] private InputActionReference _input;

    ItemManager _ItemManager;

    private bool collectedItem = false;

    private PuzzleManager _puzzleManager;
    private AudioManager audioManager;
    private FMODEvents fmod;

    private void OnEnable()
    {
        _input.action.performed -= OnCollect;
    }

    private void OnDisable()
    {
        _input.action.performed -= OnCollect;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        { 

            if (!collectedItem) 
            {
                _input.action.performed += OnCollect;
            }
            
            HUD.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            _input.action.performed -= OnCollect;
            HUD.SetActive(false);
        }
    }


    private void OnCollect(InputAction.CallbackContext obj)
    {

        audioManager.PlayOneShot(fmod.Interact, transform.position);

        _puzzleManager.CollectKalimbaSparkles.SetActive(false);
        _puzzleManager.Husk1Sparkles.SetActive(true);
        //_puzzleManager.BarDoor1Sparkles.SetActive(true);
        _puzzleManager.PPlate1_1Sparkles.SetActive(true);
        _puzzleManager.PPlate1_2Sparkles.SetActive(true);
        _input.action.performed -= OnCollect;
        collectedItem = true;
        HUD.SetActive(false);
        _ItemManager.Add(this.ItemUI);
        _ItemManager._kalimbaAquired = true;
        ItemEnabledUI.SetActive(true); 
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        collectedItem = false;

        if (_ItemManager == null)
        { 
            _ItemManager = ItemManager.Instance;
        }

        _puzzleManager = PuzzleManager.PMInstance;

        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
        
    }
}
