using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;

    private int resolutionWidth;
    private int resolutionHeight;

    void Start ()
    {
        // use PlayerPref to save setting
        CheckPlayerPref();

        SetSizeScreen();
        Canvas.ForceUpdateCanvases();
    }

    private void CheckPlayerPref()
    {
        if (PlayerPrefs.HasKey("isFullscreen"))
            if (PlayerPrefs.GetInt("isFullscreen") == 1)
            {
                SetFullscreen(true);
                fullScreenToggle.isOn = true;
            }
            else
                SetFullscreen(false);
                

        if (PlayerPrefs.HasKey("volume"))
        {
            float v = PlayerPrefs.GetFloat("volume");
            SetVolume(v);
            volumeSlider.value = v;
        }
        else
            SetVolume(0);

        if (PlayerPrefs.HasKey("quality"))
        {
            int q = PlayerPrefs.GetInt("quality");
            SetQuality(q);
            qualityDropdown.value = q;
        }
        else
            SetQuality(0);

        if (PlayerPrefs.HasKey("resolution"))
            SetResolution(PlayerPrefs.GetInt("resolution"));
        else
            SetResolution(0);
            
    }

    private void SetSizeScreen()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        string option = "";

        // 1280 x 720
        option = "1280 x 720";
        options.Add(option);

        // 1600 x 900
        option = "1600 x 900";
        options.Add(option);

        // 1920 x 1080
        option = "1920 x 1080";
        options.Add(option);

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        resolutionDropdown.RefreshShownValue();
    }


    public void SetResolution (int resolutionIndex)
    {
        PlayerPrefs.SetInt("resolution", resolutionIndex);

        if (resolutionIndex == 0)
            Screen.SetResolution(1280, 720, Screen.fullScreen, 60);

        if (resolutionIndex == 1)
            Screen.SetResolution(1600, 900, Screen.fullScreen, 60);

        if (resolutionIndex == 2)
            Screen.SetResolution(1920, 1080, Screen.fullScreen, 60);
    }

    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen)
            PlayerPrefs.SetInt("isFullscreen", 1);
        else
            PlayerPrefs.SetInt("isFullscreen", 0);
    }

}
