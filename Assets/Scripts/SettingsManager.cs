using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [Space]
    [SerializeField] private UnityEvent onClose;
    
    private void Awake()
    {
        if (settingsCanvas == null)
            settingsCanvas = GetComponent<Canvas>();
    }

    public void OnMusicVolumeChange()
    {
        AudioManager.Instance.musicVolumeChanged(musicVolumeSlider.value);
    }
    
    public void OnEffectsVolumeChange()
    {
        AudioManager.Instance.effectVolumeChanged(effectsVolumeSlider.value);
    }

    private void SetVolumeSliderValues()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
    }
    
    public void OpenMenu()
    {
        SetVolumeSliderValues();
        settingsCanvas.enabled = true;
    }

    public void CloseMenu()
    {
        settingsCanvas.enabled = false;
        onClose?.Invoke();
    }
}