using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerFootsteps : MonoBehaviour
{
    private EventInstance _footstepsInstance;

    //Managers
    private AudioManager audioManager;
    private FMODEvents fmod;


    private void Start()
    {
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;

        _footstepsInstance = audioManager.CreateEventInstance(fmod.NewFootsteps);
    }

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
        {
            PLAYBACK_STATE fsplayback;
            _footstepsInstance.getPlaybackState(out fsplayback);

            if (fsplayback.Equals(PLAYBACK_STATE.STOPPED))
            {
                _footstepsInstance.start();
            }
        }

        else 
        {
            _footstepsInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

/*    private void OnDisable()
    {
        _footstepsInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _footstepsInstance.release();

    }*/
}
