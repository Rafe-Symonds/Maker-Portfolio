using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;


    [RuntimeInitializeOnLoadMethod]
    public void ChangeSettingsOnGameLoad()
    {
        Screen.fullScreen = SaveAndLoad.Get().GetFullScreen();
        if(SaveAndLoad.Get().GetResolutionWidth() != 0 && SaveAndLoad.Get().GetResolutionWidth() != 0)
        {
            Screen.SetResolution(SaveAndLoad.Get().GetResolutionWidth(), SaveAndLoad.Get().GetResolutionWidth(), SaveAndLoad.Get().GetFullScreen());
        }
        DontDestroyOnLoad(audioMixer);
    }



    void OnEnable()
    {
        GameObject.Find("Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        GameObject.Find("FullScreenToggle").GetComponent<Toggle>().isOn = SaveAndLoad.Get().GetFullScreen();

        int savedResolutionHeight = SaveAndLoad.Get().GetResolutionHeight();
        int savedResolutionWidth = SaveAndLoad.Get().GetResolutionWidth();
        if(savedResolutionHeight == 0 || savedResolutionWidth == 0)
        {
            return;
        }
        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].height == savedResolutionHeight && resolutions[i].width == savedResolutionWidth)
            {
                resolutionDropDown.value = i;
                //Debug.Log("resolutions saved value " + i);
                resolutionDropDown.RefreshShownValue();
                break;
            }
        }
    }

    void Awake()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();


        List<string> options = new List<string>();

        int currentResolutionIdex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) 
            {
                currentResolutionIdex = i;
            }
        }
        //Debug.Log("resolutions deafult");
        resolutionDropDown.AddOptions(options);
        
        if (SaveAndLoad.Get().GetResolutionHeight() == 0)
        {
            //Debug.Log("****** refresh show values");
            resolutionDropDown.value = currentResolutionIdex;
            resolutionDropDown.RefreshShownValue();
        }
    }
    public void SetResolution(int resolutionIndex)
    {
        //Debug.Log("Changing resolution");
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        SaveAndLoad.Get().SetResolution(resolution);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        //SaveAndLoad.Get().SetVolume(volume);
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        SaveAndLoad.Get().SetFullScreen(isFullScreen);
    }
    public void SaveData()
    {
        SaveAndLoad.SaveData();
    }
}
