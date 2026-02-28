using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    void Start()
    {
        LoadSettings();

        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);

        // Load saved resolution index or use current
        int savedResolution = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionsDropdown.value = savedResolution;
        resolutionsDropdown.RefreshShownValue();
    }

    void LoadSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.0f); // 0 dB
        audioMixer.SetFloat("Volume", savedVolume);
        if (volumeSlider != null)
            volumeSlider.value = savedVolume;

        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        if (sensitivitySlider != null)
            sensitivitySlider.value = savedSensitivity;

        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = isFullscreen;
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetResolutions(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }
}
