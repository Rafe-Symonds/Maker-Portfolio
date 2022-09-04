using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    void OnEnable()
    {
        if(settingsMenu != null)
        {
            settingsMenu.GetComponent<SettingsMenu>().SetVolume(PlayerPrefs.GetFloat("volume"));
        }
        if (GameObject.Find("MainMenuManager") != null)
        {
            GameObject.Find("MainMenuManager").GetComponent<MainMenuManagerScript>().TurnOnTroopSpawns();
        }
    }
    public void PlayGame()
    {
        if(SaveAndLoad.Get().CurrentLevel(0) > 10)
        {
            GameObject.Find("Save0").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Complete at " + ReturnDifficultyString(0);
        }
        else 
        {
            GameObject.Find("Save0").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Current Level: " + SaveAndLoad.Get().CurrentLevel(0);
        }

        if (SaveAndLoad.Get().CurrentLevel(1) > 10)
        {
            GameObject.Find("Save1").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Complete at " + ReturnDifficultyString(1);
        }
        else
        {
            GameObject.Find("Save1").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Current Level: " + SaveAndLoad.Get().CurrentLevel(1);

        }

        if (SaveAndLoad.Get().CurrentLevel(2) > 10)
        {
            GameObject.Find("Save2").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Complete at " + ReturnDifficultyString(2);
        }
        else
        {
            GameObject.Find("Save2").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Current Level: " + SaveAndLoad.Get().CurrentLevel(2);

        }

        if (SaveAndLoad.Get().CurrentLevel(3) > 10)
        {
            GameObject.Find("Save3").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Complete at " + ReturnDifficultyString(3);
        }
        else
        {
            GameObject.Find("Save3").transform.Find("SaveInfo").GetComponent<TextMeshProUGUI>().text = "Current Level: " + SaveAndLoad.Get().CurrentLevel(3);

        }
        GameObject.Find("Save0").transform.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = ReturnDifficultyString(0);
        GameObject.Find("Save1").transform.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = ReturnDifficultyString(1);
        GameObject.Find("Save2").transform.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = ReturnDifficultyString(2);
        GameObject.Find("Save3").transform.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = ReturnDifficultyString(3);
        //SceneManager.LoadScene("Map");
    }
    public string ReturnDifficultyString(int save)
    {
        if (SaveAndLoad.Get().GetGameDifficulty(save) == 0)
        {
            return "Difficulty Level: ";
        }
        else if(SaveAndLoad.Get().GetGameDifficulty(save) == 1)
        {
            return "Difficulty Level: Easy";
        }
        else if (SaveAndLoad.Get().GetGameDifficulty(save) == 2)
        {
            return "Difficulty Level: Medium";
        }
        else
        {
            return "Difficulty Level: Hard";
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void TechTree()
    {
        SceneManager.LoadScene("TechTree");
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //PauseMenuScript.StaticResume();
    }
    
}
