using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using FMODUnity;
using FMOD.Studio;
using System.Runtime.Serialization.Formatters;
using System.Runtime.InteropServices;
using System;
using System.Linq;

public class KalimbaController : MonoBehaviour
{

    //Input Action References for tiles
    [SerializeField] InputActionReference PlayTile1;
    [SerializeField] InputActionReference PlayTile2;
    [SerializeField] InputActionReference PlayTile3;
    [SerializeField] InputActionReference PlayTile4;
    [SerializeField] InputActionReference PlayTile5;
    [SerializeField] InputActionReference PlayTile6;
    [SerializeField] InputActionReference PlayTile7;
    [SerializeField] InputActionReference PlayTile8;

    //Images of the UI
    [SerializeField] private Image tile1Image;
    [SerializeField] private Image tile2Image;
    [SerializeField] private Image tile3Image;
    [SerializeField] private Image tile4Image;
    [SerializeField] private Image tile5Image;
    [SerializeField] private Image tile6Image;
    [SerializeField] private Image tile7Image;
    [SerializeField] private Image tile8Image;


    //List implementation: All the notes played by the player will be stored on a list, so it will not be checked only when you play
    //4 notes, but will be always checking the last 4 notes you played
    private List<int> KalimbaPlayedNotes = new List<int>();

    //Check bools
    private bool _disableKalimba;
    private bool correctHasPlayed;
    private bool CanPlayCorrectSound;

    //The combination has four notes, so for the combo check we just pass an array the last four notes that were played
    public int[] ComboToCheck = new int[4];

    //Managers (Singletons)
    GameManager gameManager;
    AudioManager audioManager;
    FMODEvents fmod;

    
    //Input actions for playing each key
    private void OnPlayTile1(InputAction.CallbackContext obj)
    {
        audioManager.PlayOneShot(fmod.KalimbaTile1,transform.position);
        KalimbaComboCreator(1);
        FlashKey(tile1Image);
    }

    private void OnPlayTile2(InputAction.CallbackContext obj)
    {
        audioManager.PlayOneShot(fmod.KalimbaTile2, transform.position);
        KalimbaComboCreator(2);
        FlashKey(tile2Image);

    }

    private void OnPlayTile3(InputAction.CallbackContext obj)
    {
        audioManager.PlayOneShot(fmod.KalimbaTile3, transform.position);
        KalimbaComboCreator(3);
        FlashKey(tile3Image);

    }

    private void OnPlayTile4(InputAction.CallbackContext obj)
    {

        audioManager.PlayOneShot(fmod.KalimbaTile4, transform.position);
        FlashKey(tile4Image);
        KalimbaComboCreator(4);

    }  
    
    private void OnPlayTile5(InputAction.CallbackContext obj)
    {
        audioManager.PlayOneShot(fmod.KalimbaTile5, transform.position);
        FlashKey(tile5Image);
        KalimbaComboCreator(5);

    }

    private void OnPlayTile6(InputAction.CallbackContext obj)
    {
        audioManager.PlayOneShot(fmod.KalimbaTile6, transform.position);
        FlashKey(tile6Image);
        KalimbaComboCreator(6);

    }

    private void OnPlayTile7(InputAction.CallbackContext obj)
    {
        audioManager.PlayOneShot(fmod.KalimbaTile7, transform.position);
        FlashKey(tile7Image);
        KalimbaComboCreator(7);

    }

    private void OnPlayTile8(InputAction.CallbackContext obj)
    {
        audioManager.PlayOneShot(fmod.KalimbaTile8, transform.position);
        FlashKey(tile8Image);
        KalimbaComboCreator(8);

    }





    //Coroutine for the tiles to flash on pressed
    private void FlashKey(Image keyImage, float duration = 0.25f)
    {
        StartCoroutine(FlashKeyRoutine(keyImage, duration));
    }

    //Change color of tiles when played
    private IEnumerator FlashKeyRoutine(Image keyImage, float duration = 0.25f)
    {
        keyImage.color = Color.yellow;
        yield return new WaitForSeconds(duration);
        keyImage.color = Color.white;
    }

    //Add notes to the KalimbaPlayedNotes list and validates them
    private void KalimbaComboCreator(int tileNo)
    {
        KalimbaPlayedNotes.Add(tileNo);
        ValidateCombo();
    }

    //If the played notes are of the length of a combo, copy it to the ComboToCheck array
    private void ValidateCombo()
    {
        //Assigns the last 4 digits of the list to the Combination to be checked (array of ints)
        if (KalimbaPlayedNotes.Count >= 4)
        {
            KalimbaPlayedNotes.GetRange(KalimbaPlayedNotes.Count - 4, 4).CopyTo(ComboToCheck);
        }

        //Keeps the list at 4 elements (we dont need to store every note played)
        if (KalimbaPlayedNotes.Count > 4)
        {
            KalimbaPlayedNotes.RemoveAt(0);
        }

    }

    //Enable input actions on UI creation
    private void OnEnable()
    {
        PlayTile1.action.performed += OnPlayTile1;
        PlayTile2.action.performed += OnPlayTile2;
        PlayTile3.action.performed += OnPlayTile3;
        PlayTile4.action.performed += OnPlayTile4;
        PlayTile5.action.performed += OnPlayTile5;
        PlayTile6.action.performed += OnPlayTile6;
        PlayTile7.action.performed += OnPlayTile7;
        PlayTile8.action.performed += OnPlayTile8;
    }

    //Enable input actions on UI destruction
    private void OnDisable()
    {
        PlayTile1.action.performed -= OnPlayTile1;
        PlayTile2.action.performed -= OnPlayTile2;
        PlayTile3.action.performed -= OnPlayTile3;
        PlayTile4.action.performed -= OnPlayTile4;
        PlayTile5.action.performed -= OnPlayTile5;
        PlayTile6.action.performed -= OnPlayTile6;
        PlayTile7.action.performed -= OnPlayTile7;
        PlayTile8.action.performed -= OnPlayTile8;
    }

    //Assign singletons in game
    private void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

    //Constantly check for combinations
    private void FixedUpdate()
    {
        //We now are checking for the combination on every fixed update it it has already reached the desired number of notes
        if (KalimbaPlayedNotes.Count >= 4)
        {
            try
            {
                //Iterates through the array of objects that have combinations according to the game manager
                foreach (GameObject comboObject in gameManager.ObjectsWithCombo)
                {
                    //Checks for the combination on each of the elements stored
                    if (comboObject.GetComponent<ComboCheck>().Check(ComboToCheck))
                    {
/*                        if (CanPlayCorrectSound)
                        { 
                            audioManager.PlayOneShot(fmod.CorrectAnswer,transform.position);
                            CanPlayCorrectSound = false;
                        }*/
                        Debug.Log("Correct");
                        for (int i = 0; i < ComboToCheck.Length; i++)
                        {
                            ComboToCheck[i] = 0;
                        }
                    }
                }
            }

            catch
            {
                Debug.Log("Missing comparable combo object");
            }
        }

    }
}
