using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixerGroup master;
    public AudioMixerGroup effects;
    public AudioMixerGroup music;
    public Toggle fullscreenToggle;
    public Toggle fullscreenTogglePause;
    public Slider masterVolume;
    public Slider effectsVolume;
    public Slider musicVolume;
    public Slider masterVolumePause;
    public Slider effectsVolumePause;
    public Slider musicVolumePause;
    public TMP_Dropdown qualitySettings;
    public TMP_Dropdown qualitySettingsPause;
    bool isFullscreen;
    public void Start()
    {
        SetFullscreen(PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false);
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenTogglePause.isOn = Screen.fullScreen;
        Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        master.audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("VolumeMaster"));
        effects.audioMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("VolumeEffects"));
        music.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("VolumeMusic"));
        masterVolume.value = PlayerPrefs.GetFloat("VolumeMaster");
        effectsVolume.value = PlayerPrefs.GetFloat("VolumeEffects");
        musicVolume.value = PlayerPrefs.GetFloat("VolumeMusic");
    
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualitySettings"));
        qualitySettings.value = PlayerPrefs.GetInt("QualitySettings");
        qualitySettingsPause.value = PlayerPrefs.GetInt("QualitySettings");
      
    }
    public void SetMainMenuSettings()
    {
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;
        masterVolume.value = PlayerPrefs.GetFloat("VolumeMaster");
        effectsVolume.value = PlayerPrefs.GetFloat("VolumeEffects");
        musicVolume.value = PlayerPrefs.GetFloat("VolumeMusic");
    }
    public void SetQualityDropdowns()
    {
        qualitySettings.value = PlayerPrefs.GetInt("QualitySettings");
        qualitySettingsPause.value = PlayerPrefs.GetInt("QualitySettings");
    }
    public void SetVolumeSliders()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            masterVolume.value = PlayerPrefs.GetFloat("VolumeMaster");
            effectsVolume.value = PlayerPrefs.GetFloat("VolumeEffects");
            musicVolume.value = PlayerPrefs.GetFloat("VolumeMusic");
        }
 
        masterVolumePause.value = PlayerPrefs.GetFloat("VolumeMaster");
        effectsVolumePause.value = PlayerPrefs.GetFloat("VolumeEffects");
        musicVolumePause.value = PlayerPrefs.GetFloat("VolumeMusic");
    }
    public void SetVolume(float _value)
    {
        master.audioMixer.SetFloat("MasterVolume", _value);
        effects.audioMixer.SetFloat("MasterVolume", _value);
        music.audioMixer.SetFloat("MasterVolume", _value);
        PlayerPrefs.SetFloat("VolumeMaster", _value);
        SetVolumeSliders();
    }
    public void SetEffectsVolume(float _value)
    {
        effects.audioMixer.SetFloat("EffectsVolume", _value);
        PlayerPrefs.SetFloat("VolumeEffects", _value);
        SetVolumeSliders();
    }
    public void SetMusicVolume(float _value)
    {
        music.audioMixer.SetFloat("MusicVolume", _value);
        PlayerPrefs.SetFloat("VolumeMusic", _value);
        SetVolumeSliders();
    }
    public void SetQuality(int _index)
    {
        QualitySettings.SetQualityLevel(_index);
        PlayerPrefs.SetInt("QualitySettings", _index);
        SetQualityDropdowns();
    }
    public void SetFullscreen(bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        fullscreenTogglePause.isOn = _isFullscreen;
        fullscreenToggle.isOn = _isFullscreen;

    }
}
