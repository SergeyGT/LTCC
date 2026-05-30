using FMOD;
using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public bool MusicOn => musicOn;
    public bool SoundsOn => soundsOn;

    private EventInstance musicEvent;
    private EventInstance ambienceEvent;
    private Transform listenerTransform;
    private bool musicOn = true;
    private bool soundsOn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            UnityEngine.Debug.LogWarning("Multiple instances of AudioManager detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        FindListener();

        //PlayMusic(mainMenuMusic);
    }

    private void FindListener()
    {
        var listener = FindObjectOfType<StudioListener>();
        if (listener != null)
        {
            listenerTransform = listener.transform;
        }
        else if (Camera.main != null)
        {
            listenerTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (listenerTransform == null)
        {
            FindListener();
        }
    }

    public void TurnOnOffSounds()
    {
        soundsOn = !soundsOn;

    }

    public void TurnOnOffMusic()
    {
        musicOn = !musicOn;
        if (musicOn) musicEvent.start();
        else StopMusic();
    }

    public void ResumeMusic()
    {
        if (musicOn) musicEvent.start();
    }

    public bool GetSoundsOn()
    {
        return soundsOn;
    }

    public bool GetMusicOn()
    {
        return musicOn;
    }

    public void PlayOneShot(EventReference sound, Vector3 position = new Vector3())
    {
        if (soundsOn)
            RuntimeManager.PlayOneShot(sound, position);
    }

    public void PlayOnClick()
    {
        
    }

    public void PlayMusic(EventReference music)
    {
        if (musicEvent.isValid())
        {
            musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicEvent.release();
        }

        musicEvent = CreateInstance(music);
        
        if (musicOn)
        {
            musicEvent.start();
        }
    }

    public void StopMusic()
    {
        musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayAmbience(EventReference sound, Vector3 position)
    {
        if (ambienceEvent.isValid())
        {
            ambienceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambienceEvent.release();
        }

        ambienceEvent = CreateInstance(sound, position);

        if (musicOn)
        {
            ambienceEvent.start();
        }
    }

    public void StopAmbience()
    {
        ambienceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public EventInstance CreateInstance(EventReference sound)
    {
        EventInstance instance = RuntimeManager.CreateInstance(sound);

        Set3DAttributes(instance, Vector3.zero);

        return instance;
    }

    private void Set3DAttributes(EventInstance instance, Vector3 position)
    {
        if (instance.isValid())
        {
            ATTRIBUTES_3D attributes;

            if (listenerTransform != null)
            {
                attributes = RuntimeUtils.To3DAttributes(listenerTransform, position);
            }
            else
            {
                attributes = RuntimeUtils.To3DAttributes(position);
            }

            instance.set3DAttributes(attributes);
        }
    }

    public EventInstance CreateInstance(EventReference sound, Vector3 position)
    {
        EventInstance instance = RuntimeManager.CreateInstance(sound);
        Set3DAttributes(instance, position);
        return instance;
    }

    public void UpdateInstancePosition(EventInstance instance, Vector3 position)
    {
        Set3DAttributes(instance, position);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        HandleFocus(hasFocus);
    }

    private void HandleFocus(bool hasFocus)
    {
        if (!RuntimeManager.StudioSystem.isValid())
            return;

        if (!hasFocus)
        {
            RuntimeManager.PauseAllEvents(true);    
            RuntimeManager.CoreSystem.mixerSuspend(); 
        }
        else
        {
            RuntimeManager.PauseAllEvents(false);
            RuntimeManager.CoreSystem.mixerResume();
        }
    }
    private void OnDestroy()
    {
        if (musicEvent.isValid())
        {
            musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicEvent.release();
        }
    }
}