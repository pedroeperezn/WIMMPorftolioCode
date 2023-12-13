using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UIComponents;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AMInstance;
    private EventInstance musicInstance;

    //list of events and emmiters
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmiters;

    //FMODEvnents class
    private FMODEvents fmod;

    private void Awake()
    {
        if (AMInstance == null) 
        {
            AMInstance = this;
        }

        eventInstances = new List<EventInstance>();
        eventEmiters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        fmod = FMODEvents.FMInstance;
        InitMusic(fmod.music);
    }

    //define play one shot function for easier playing
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    { 
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    //create FMOD event instance and add it to my list
    public EventInstance CreateEventInstance(EventReference reference)
    { 
        EventInstance eventInstance = RuntimeManager.CreateInstance(reference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    //create a FMOD emmiter and add it to my list
    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject) 
    { 
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmiters.Add(emitter);
        return emitter;
    }

    //start playing music
    public void InitMusic(EventReference musicReference)
    {
        musicInstance = CreateEventInstance(musicReference);
        musicInstance.start();
    }

    //Whenever I change from area in the game, the music changes
    public void SetMusicArea(MusicPlayer area)
    { 
        musicInstance.setParameterByName("MusicArea", (float) area);
    }

    //release all used FMOD events
    private void Cleanup() 
    {
        foreach(EventInstance eventInstance in eventInstances) 
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach(StudioEventEmitter emitter in eventEmiters) 
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        Cleanup();
    }
}
