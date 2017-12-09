/*
 https://github.com/owik100/Unity

 Drag and drop reference to resolutionDropdown and fullScreenToggle.
 On Dropdown 'on value changed' set ResolutionSettings -> SetResolution.
 On Toggle 'on value changed' set ResolutionSettings -> SetFullScreen.
 Tested in Unity 2017.2.0f3 (64-bit)
*/


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour {

    private Resolution[] resolutions;
    private List<string> listResolutions;

    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;

    void Start()
    {
        resolutions = Screen.resolutions;
        listResolutions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (!listResolutions.Contains(option))
            {
                listResolutions.Add(option);
            }
        }

        for (int i = 0; i < listResolutions.Count; i++)
        {
            string[] resolution = listResolutions[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (Convert.ToInt32(resolution[0]) == Screen.width && Convert.ToInt32(resolution[2]) == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(listResolutions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        if (Screen.fullScreen)
        {
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }

    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        string[] resolution = listResolutions[resolutionIndex].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        Screen.SetResolution(Convert.ToInt32(resolution[0]), Convert.ToInt32(resolution[2]), Screen.fullScreen);
    }
}
