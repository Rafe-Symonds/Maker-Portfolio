using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSaveButton : MonoBehaviour
{
    public GameObject difficultySelectScreen;
    public void LoadSave0()
    {
        if (SaveAndLoad.Get().CurrentLevel(0) <= 0)
        {
            GameObject.Find("SaveSelect").SetActive(false);
            difficultySelectScreen.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
        SaveAndLoad.Get().ChangeCurrentSave(0);
    }
    public void DeleteSave0()
    {
        SaveAndLoad.Get().DeleteSave(0);
    }
    public void MakeSaveEasy()
    {
        SaveAndLoad.Get().SetGameDifficulty(1);
        SceneManager.LoadScene("Map");
    }
    public void MakeSaveMedium()
    {
        SaveAndLoad.Get().SetGameDifficulty(2);
        SceneManager.LoadScene("Map");
    }
    public void MakeSaveHard()
    {
        SaveAndLoad.Get().SetGameDifficulty(3);
        SceneManager.LoadScene("Map");
    }
    public void LoadSave1()
    {
        if (SaveAndLoad.Get().CurrentLevel(1) <= 0)
        {
            GameObject.Find("SaveSelect").SetActive(false);
            difficultySelectScreen.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
        SaveAndLoad.Get().ChangeCurrentSave(1);
    }
    public void DeleteSave1()
    {
        SaveAndLoad.Get().DeleteSave(1);
    }
    public void LoadSave2()
    {
        if (SaveAndLoad.Get().CurrentLevel(2) <= 0)
        {
            GameObject.Find("SaveSelect").SetActive(false);
            difficultySelectScreen.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
        SaveAndLoad.Get().ChangeCurrentSave(2);
    }
    public void DeleteSave2()
    {
        SaveAndLoad.Get().DeleteSave(2);
    }
    public void LoadSave3()
    {
        if (SaveAndLoad.Get().CurrentLevel(3) <= 0)
        {
            GameObject.Find("SaveSelect").SetActive(false);
            difficultySelectScreen.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
        SaveAndLoad.Get().ChangeCurrentSave(3);
    }
    public void DeleteSave3()
    {
        SaveAndLoad.Get().DeleteSave(3);
    }
}
