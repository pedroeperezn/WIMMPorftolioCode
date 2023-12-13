using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSheetMusic : MonoBehaviour
{
    [SerializeField] private GameObject _sheetTrigger;

    PuzzleManager _puzzleManager;
    AudioManager audioManager;
    FMODEvents fmod;

    private bool _audioHasPlayed = false;

    private void Update()
    {
        if (_puzzleManager.CheckerboardSolved)
        {
            GetComponent<Animator>().SetTrigger("OpenDrawer");
            if(!_audioHasPlayed) 
            { 
                audioManager.PlayOneShot(fmod.Drawer,transform.position);
                _audioHasPlayed = true;
            }
            _sheetTrigger.SetActive(true);
        }
    }

    private void Start()
    {
        _puzzleManager = PuzzleManager.PMInstance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }
}
