using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlateManager : MonoBehaviour
{

    [SerializeField] private GameObject objectToInteract;
    [SerializeField] private GameObject objectToShow;
    [SerializeField] private bool _isBookshelf;
    [SerializeField] private bool _playerInPosition;
    [SerializeField] private bool _huskInPosition;

    public bool isSolved = false;

    //Managers
    private PuzzleManager _puzzleManager;
    private AudioManager audioManager;
    private FMODEvents fmod;

    public bool PlayerInPosition { get => _playerInPosition; set => _playerInPosition = value; }
    public bool HuskInPosition { get => _huskInPosition; set => _huskInPosition = value; }

    private bool CheckPositions()
    {
        if (_playerInPosition && _huskInPosition)
        {
            objectToInteract.GetComponent<Animator>().SetTrigger("Rotated");
            objectToShow.GetComponent<SphereCollider>().enabled = true;
            

            if (_isBookshelf && !isSolved)
            {
                _puzzleManager.PressurePlate1Solved = true;
                //activate sparkles for next puzzle (door)
                _puzzleManager.FirstSheetSparkles.SetActive(true);
                _puzzleManager.KalimbaDoor1Sparkles.SetActive(true);

                //deactivate sparkles for previous puzzles (husk moving)
                _puzzleManager.PPlate1_1Sparkles.SetActive(false);
                _puzzleManager.PPlate1_2Sparkles.SetActive(false);
                //_puzzleManager.BarDoor1Sparkles.SetActive(false);

                //play sound
                StartCoroutine(BookshelfAudioCoroutine());
                
            }

            else if (!_isBookshelf && !isSolved)
            {
                _puzzleManager.PressurePlate2Solved = true;
                _puzzleManager.PPlate2_1Sparkles.SetActive(false);
                _puzzleManager.PPlate2_2Sparkles.SetActive(false);

                _puzzleManager.ChestSparkles.SetActive(true);

                StartCoroutine(ChestSoundCoroutine());
                audioManager.PlayOneShot(fmod.Chest, transform.position);
            }

            isSolved = true;
            return true;
        }

        return false;
    
    }

    private IEnumerator ChestSoundCoroutine()
    {
        audioManager.PlayOneShot(fmod.Chest, transform.position);
        yield return new WaitForSeconds(1.5f);
        audioManager.PlayOneShot(fmod.CorrectAnswer, transform.position);
    }

    private IEnumerator BookshelfAudioCoroutine()
    {
        audioManager.PlayOneShot(fmod.Bookshelf, transform.position);
        yield return new WaitForSeconds(1.5f);
        audioManager.PlayOneShot(fmod.CorrectAnswer, transform.position);
    }

    private void Update()
    {
        CheckPositions();
    }

    private void Start()
    {
        _puzzleManager = PuzzleManager.PMInstance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

}
