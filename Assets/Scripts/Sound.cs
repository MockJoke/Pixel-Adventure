using UnityEngine;

[System.Serializable]
public class Sound
{
    [Tooltip("The name of our music/effect")]
    public AudioType name;
    
    [Tooltip("The actual music/effect clip")]
    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float volume = 1f;
    
    [Range(0.1f, 3f)] [Tooltip("set the pitch for our music/effect")]
    public float pitch = 1f;
    
    [HideInInspector] public AudioSource source;        // the source that will play the sound
    
    [Tooltip("should this sound loop or not")]
    public bool loop = false;
}
