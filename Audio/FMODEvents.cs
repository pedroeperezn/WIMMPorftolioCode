using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    //FMOD events for kalimba notes
    [Header("Kalimba Notes")]
    [SerializeField] public EventReference KalimbaTile1;
    [SerializeField] public EventReference KalimbaTile2;
    [SerializeField] public EventReference KalimbaTile3;
    [SerializeField] public EventReference KalimbaTile4;
    [SerializeField] public EventReference KalimbaTile5;
    [SerializeField] public EventReference KalimbaTile6;
    [SerializeField] public EventReference KalimbaTile7;
    [SerializeField] public EventReference KalimbaTile8;

    //footsteps
    [Header("Player")]
    [SerializeField] public EventReference NewFootsteps;

    //game music
    [Header("Music")]
    [SerializeField] public EventReference music;

    //husk melodies events
    [Header("Husk Melodies")]
    [SerializeField] public EventReference HuskMelody1;
    [SerializeField] public EventReference HuskMelody2;

    //icarus puzzle sounds
    [Header("Icarus Puzzle")]
    [SerializeField] public EventReference IcarusDragImage;
    [SerializeField] public EventReference IcarusDropImage;

    //checker puzzle sounds
    [Header("Checker Puzzle")]
    [SerializeField] public EventReference CheckerDragImage;
    [SerializeField] public EventReference CheckerDropImage;

    //doors sounds
    [Header("Doors")]
    [SerializeField] public EventReference cabinetDoor;
    [SerializeField] public EventReference normalDoor;
    [SerializeField] public EventReference barDoor;

    //prop sounds
    [Header("Props")]
    [SerializeField] public EventReference PressurePlate;
    [SerializeField] public EventReference Bookshelf;
    [SerializeField] public EventReference Chest;
    [SerializeField] public EventReference Drawer;
    [SerializeField] public EventReference Phone;


    //feedback sounds
    [Header("Player Feedback")]
    [SerializeField] public EventReference CorrectAnswer;
    [SerializeField] public EventReference Interact;



    public static FMODEvents FMInstance;

    private void Awake()
    {
        if (FMInstance == null)
        {
            FMInstance = this;
        }
    }
}
