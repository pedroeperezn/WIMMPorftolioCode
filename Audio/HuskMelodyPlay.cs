using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HuskMelodyPlay : MonoBehaviour
{
    
    [SerializeField] private bool _isHusk1;

    private EventInstance _huskMelodyInstance;

    //Managers
    private AudioManager audioManager;
    private FMODEvents fmod;


    private StudioEventEmitter emitter;

    private void Start()
    {

        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;

        if (_isHusk1)
        {
            emitter = audioManager.InitializeEventEmitter(fmod.HuskMelody1, gameObject);
        }

        else 
        {
            emitter = audioManager.InitializeEventEmitter(fmod.HuskMelody2, gameObject);

        }

        emitter.Play();
    }

    private void OnDisable()
    {
        emitter.Stop();
    }

    /*    private void Start()
        {
            audioManager = AudioManager.AMInstance;
            fmod = FMODEvents.FMInstance;

            if (_isHusk1)
            {
                _huskMelodyInstance = audioManager.CreateEventInstance(fmod.HuskMelody1);
            }

            else 
            {
                _huskMelodyInstance = audioManager.CreateEventInstance(fmod.HuskMelody2);
            }

            _huskMelodyInstance.start();
        }

        private void Update()
        {
            _huskMelodyInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.GetComponent<Rigidbody>().position));
        }*/
    /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                _huskMelodyInstance.start();
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                _huskMelodyInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                //_huskMelodyInstance.release();

            }
        }*/
}
