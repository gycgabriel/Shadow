using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    
    public void ChangeTotalVolume(float value)
    {
        AudioManager.scriptInstance.totalVolume = (int) value;
    }

    public void ChangeBGMVolume(float value)
    {
        AudioManager.scriptInstance.bgmVolume = (int) value;
    }

    public void ChangeSFXVolume(float value)
    {
        AudioManager.scriptInstance.sfxVolume = (int) value;
    }

}
