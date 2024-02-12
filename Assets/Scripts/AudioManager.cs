using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] effectSounds;
    public Sound[] bgSounds;

    private int currBgMusicIndex = -1;      // set -1 to signify no song playing

    public static AudioManager instance;

    private float mVol;     // Global music volume
    private float eVol;     // Global effects volume
    
    public bool isMuted = false;
    
    void Awake() 
    {
        if (instance == null) 
        {
            instance = this; 
        } 
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // get preferences
        mVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        eVol = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        createAudioSources(effectSounds, eVol);     // create sources for effects
        createAudioSources(bgSounds, mVol);   // create sources for music
    }
    
    void Start() 
    {
        if (bgSounds.Length > 0)
        {
            PlayMusic(true);
        }
        
        SetMuteState();
    }
    
    void Update() 
    {
        // if we are playing a track (which wasn't on loop) from the playlist
        if (currBgMusicIndex != -1) 
        {
            // and it has stopped playing
            if (!bgSounds[currBgMusicIndex].source.isPlaying)
            {
                PlayMusic(true);
            }
        }
    }
    
    // create sources
    private void createAudioSources(Sound[] sounds, float volume) 
    {
        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(AudioType audioName) 
    {
        Sound s = Array.Find(effectSounds, sound => sound.name == audioName);
        
        if (s == null) 
        {
            Debug.LogWarning("Unable to play sound " + audioName);
            return;
        }
        
        s.source.Play();
    }
    
    public void StopSound(AudioType audioName)
    {
        Sound s = Array.Find(effectSounds, sound => sound.name == audioName);
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + audioName + " not found");
            return;
        }
        
        s.source.Stop();
    }
    
    public void PlayMusic(bool random, AudioType audioName = AudioType.None)
    {
        // If already playing on Music, stop it first
        if (currBgMusicIndex != -1)
        {
            StopMusic();
        }
        
        if (random || audioName == AudioType.None)
        {
            // pick a random song from our playlist
            currBgMusicIndex = GetRandomBgMusicIndex();
        }
        else
        {
            currBgMusicIndex = Array.FindIndex(effectSounds, sound => sound.name == audioName);

            if (currBgMusicIndex < 0)
            {
                // pick a random song from our playlist
                currBgMusicIndex = GetRandomBgMusicIndex();
            }
        }
            
        bgSounds[currBgMusicIndex].source.Play();
    }

    public void StopMusic()
    {
        bgSounds[currBgMusicIndex].source.Stop();
        currBgMusicIndex = -1;
    }

    private int GetRandomBgMusicIndex()
    {
        return UnityEngine.Random.Range(0, bgSounds.Length - 1);
    }
    
    public AudioType getCurrBgSongName() 
    {
        return bgSounds[currBgMusicIndex].name;
    }

    // if the music volume change update all the audio sources
    public void musicVolumeChanged(float val)
    {
        mVol = val;
        PlayerPrefs.SetFloat("MusicVolume", mVol);
        
        foreach (Sound m in bgSounds) 
        {
            m.source.volume = m.volume * mVol;
        }
    }

    // if the effects volume changed update the audio sources
    public void effectVolumeChanged(float val) 
    {
        eVol = val;
        PlayerPrefs.SetFloat("EffectsVolume", eVol);
        
        foreach (Sound s in effectSounds) 
        {
            s.source.volume = s.volume * eVol;
        }
        
        // effectSounds[0].source.Play();
    }
    
    private void SetMuteState()
    {
        isMuted = PlayerPrefs.GetInt("Muted") == 1;
        SetAudioListener();
    }
    
    public void ToggleMuteState()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        SetAudioListener();
    }

    private void SetAudioListener()
    {
        AudioListener.pause = isMuted;
    }
}
