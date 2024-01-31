using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] effectSounds;
    public Sound[] bgSounds;

    private int currBgMusicIndex = -1;      // set high to signify no song playing

    // a play music flag so we can stop playing music during cutscenes etc
    private bool shouldPlayMusic = false; 

    public static AudioManager instance; // will hold a reference to the first AudioManager created

    private float mvol; // Global music volume
    private float evol; // Global effects volume
    
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
        mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);

        createAudioSources(effectSounds, evol);     // create sources for effects
        createAudioSources(bgSounds, mvol);   // create sources for music
    }
    
    void Start() 
    {
        if (bgSounds.Length > 0)
        {
            PlayMusic(true);
        }
    }
    
    void Update() 
    {
        // if we are playing a track from the playlist && it has stopped playing
        // if (currentPlayingIndex != 999 && !bgSounds[currentPlayingIndex].source.isPlaying) 
        // {
        //     currentPlayingIndex++; // set next index
        //     
        //     if (currentPlayingIndex >= bgSounds.Length) 
        //     { //have we went too high
        //         currentPlayingIndex = 0; // reset list when max reached
        //     }
        //     
        //     bgSounds[currentPlayingIndex].source.Play(); // play that funky music
        // }
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
        if (shouldPlayMusic == false) 
        {
            shouldPlayMusic = true;

            if (random)
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
    }

    // stop music
    public void StopMusic() 
    {
        if (shouldPlayMusic) 
        {
            shouldPlayMusic = false;
            currBgMusicIndex = 999; // reset playlist counter
        }
    }

    private int GetRandomBgMusicIndex()
    {
        return UnityEngine.Random.Range(0, bgSounds.Length - 1);
    }
    
    // get the song name
    public AudioType getBgSongName() 
    {
        return bgSounds[currBgMusicIndex].name;
    }

    // if the music volume change update all the audio sources
    public void musicVolumeChanged()
    {
        mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        
        foreach (Sound m in bgSounds) 
        {
            m.source.volume = m.volume * mvol;
        }
    }

    // if the effects volume changed update the audio sources
    public void effectVolumeChanged() 
    {
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        
        foreach (Sound s in effectSounds) 
        {
            s.source.volume = s.volume * evol;
        }
        
        // effectSounds[0].source.Play();
    }
}
