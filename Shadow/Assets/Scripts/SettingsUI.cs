using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    public Slider totalVol;
    public Slider bgmVol;
    public Slider sfxVol;

    public TMP_Dropdown qualDropdown;
    public TMP_Dropdown resDropdown;
    public Toggle fullscrnToggle;
    Resolution[] resolutions;

    void OnEnable()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentResIndex = i;

        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();

        qualDropdown.value = QualitySettings.GetQualityLevel();
        qualDropdown.RefreshShownValue();

        fullscrnToggle.SetIsOnWithoutNotify(Screen.fullScreen);    // ison will trigger below method and cause chaos

        ChangeTotalVolume(PlayerPrefs.GetFloat("tvol", 10f));
        ChangeBGMVolume(PlayerPrefs.GetFloat("bgmvol", 10f));
        ChangeSFXVolume(PlayerPrefs.GetFloat("sfxvol", 10f));
        totalVol.value = AudioManager.scriptInstance.totalVolume;
        bgmVol.value = AudioManager.scriptInstance.bgmVolume;
        sfxVol.value = AudioManager.scriptInstance.sfxVolume;
    }

    public void ChangeTotalVolume(float value)
    {
        AudioManager.scriptInstance.totalVolume = (int) value;
        PlayerPrefs.SetFloat("tvol", value);
    }

    public void ChangeBGMVolume(float value)
    {
        AudioManager.scriptInstance.bgmVolume = (int) value;
        PlayerPrefs.SetFloat("bgmvol", value);
    }

    public void ChangeSFXVolume(float value)
    {
        AudioManager.scriptInstance.sfxVolume = (int) value;
        PlayerPrefs.SetFloat("sfxvol", value);
    }

    public void ToggleFullscreen(bool value)
    {
        Screen.fullScreen = !Screen.fullScreen;
        PlayerPrefs.SetInt("fullscreen", Screen.fullScreen ? 1 : 0);

       /*switch (value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            default:
                Debug.LogWarning("Unknown display option " + value + " selected.");
                break;
        }*/
    }

    public void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("qual", value);
    }

    public void ChangeResolution(int value)
    {
        Screen.SetResolution(resolutions[value].width, resolutions[value].height, Screen.fullScreen);
        PlayerPrefs.SetInt("res", value);
    }

    public void MuteAll()
    {
        ChangeTotalVolume(0f);
    }

}
