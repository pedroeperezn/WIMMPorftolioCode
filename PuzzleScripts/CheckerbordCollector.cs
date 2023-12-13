using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckerbordCollector : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject _puzzleTrigger;
    [SerializeField] private InputActionReference _input;

    private PuzzleManager _puzzleManager;

    private AudioManager audioManager;
    private FMODEvents fmod;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            _input.action.performed += OnCollect;
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

        _puzzleManager.ChestSparkles.SetActive(false);
        _puzzleManager.CheckersSparkles.SetActive(true);
        _puzzleTrigger.gameObject.SetActive(true);
        _input.action.performed -= OnCollect;
        HUD.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        _puzzleManager = PuzzleManager.PMInstance;

        audioManager = AudioManager.AMInstance;

        fmod = FMODEvents.FMInstance;

    }
}
