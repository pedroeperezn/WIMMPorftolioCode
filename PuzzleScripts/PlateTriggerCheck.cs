using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlateTriggerCheck : MonoBehaviour
{
    [SerializeField] private GameObject PlatesManagerObject;


    private PreasurePlateManager PlatesManager;
    private AudioManager audioManager;
    private FMODEvents fmod;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            PlatesManager.PlayerInPosition = true;
            audioManager.PlayOneShot(fmod.PressurePlate,transform.position);
        }

        if (other.gameObject.tag == "Husks")
        {

            PlatesManager.HuskInPosition = true;
            audioManager.PlayOneShot(fmod.PressurePlate, transform.position);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            PlatesManager.PlayerInPosition = false;
            audioManager.PlayOneShot(fmod.PressurePlate, transform.position);

        }

        if (other.gameObject.tag == "Husks")
        {

            PlatesManager.HuskInPosition = false;
            audioManager.PlayOneShot(fmod.PressurePlate, transform.position);

        }
    }

    private void Start()
    {
        PlatesManager = PlatesManagerObject.GetComponent<PreasurePlateManager>();
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }
}
