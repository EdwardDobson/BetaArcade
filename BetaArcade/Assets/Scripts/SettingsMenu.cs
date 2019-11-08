using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle fullscreenToggle;
    public Slider volume;
    public TMP_Dropdown qualitySettings;
    bool isFullscreen;
    Resolution[] resolutions;
    public void Start()
    {
        isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.isOn = isFullscreen;
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("Volume"));
        volume.value = PlayerPrefs.GetFloat("Volume");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualitySettings"));
        qualitySettings.value = PlayerPrefs.GetInt("QualitySettings");
    }
    public void SetVolume(float _value)
    {
        audioMixer.SetFloat("MasterVolume", _value);
        PlayerPrefs.SetFloat("Volume", _value);
    }
    public void SetQuality(int _index)
    {
        QualitySettings.SetQualityLevel(_index);
        PlayerPrefs.SetInt("QualitySettings", _index);
    }
    public void SetFullscreen(bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);

    }
}
