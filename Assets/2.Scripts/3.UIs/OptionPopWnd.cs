using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionPopWnd : MonoBehaviour
{
    [SerializeField] Slider _bgmSlider, _sfxSlider;
    public void OpenWindow()
    {
        Debug.Log(gameObject.activeSelf);
        gameObject.SetActive(true);
    }
    public void OptionWndClose()
    {
        Debug.Log("setActiveFalse");
        gameObject.SetActive(false);
    }
    public void ChangeBgmVolume()
    {
        SoundManager._instance.SetBgmVolume(_bgmSlider.value);
    }
    public void ChangeSFXVolume()
    {
        SoundManager._instance.SetSFXVolume(_sfxSlider.value);
    }
}
