using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboCheck : MonoBehaviour
{
    //Assign correct combo for the object
    [SerializeField]
    public int[] correctCombo = new int[4];

    //Checks if combo has been triggered previously
    public bool comboTriggered = false;
    private bool canPlaySound = true;

    [SerializeField] public int[] playedCombo = new int[4];


    private AudioManager audioManager;
    private FMODEvents fmod;

    public bool Check(int[] KalimbaCombo)
    {
        playedCombo = KalimbaCombo;


        if (KalimbaCombo.SequenceEqual(correctCombo))
        {
            //Check which method is going to be called.
            comboTriggered = true;
            return comboTriggered;
        }

        return comboTriggered;
    }

    private void Start()
    {
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

}
