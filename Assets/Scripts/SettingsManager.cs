using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;

    public void OnMusicVolumeChange()
    {
        AudioManager.instance.musicVolumeChanged(musicVolumeSlider.value);
    }
    
    public void OnEffectsVolumeChange()
    {
        AudioManager.instance.effectVolumeChanged(effectsVolumeSlider.value);
    }
}
