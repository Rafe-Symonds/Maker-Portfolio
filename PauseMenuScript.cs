using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenu;

    private bool previousEscapePress = false;

    void Start()
    {

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && previousEscapePress == false)
        {
            
            //Debug.Log("Escape");
            if(gameIsPaused)
            {
                Resume();
                previousEscapePress = true;
            }
            else
            {
                Pause();
                previousEscapePress = true;
            }
        }
        if(!Input.GetKeyDown(KeyCode.Escape))
        {
            previousEscapePress = false;
        }
    }
    public void Resume()
    {
        TimeManagement.StopPause();
        //Debug.Log("Resume");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        //Cursor.visible = false;
    }
    public static void StaticResume()
    {
        TimeManagement.StopPause();
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        TimeManagement.StartPause();
        //Debug.Log("Pause");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Resume();
    }
}
