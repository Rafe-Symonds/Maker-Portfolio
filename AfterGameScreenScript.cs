using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AfterGameScreenScript : MonoBehaviour
{
    public static bool addNewStars;
    public static bool addNewPath;
    public static int levelNumberStarsJustAddedTo;
    public static int numberOfStarsBeforeLevelBeaten;
    GameObject wizard;
    GameObject star1;
    GameObject star2;
    GameObject star3;
    public bool compeletedStarSetActive = false;
    private static AfterGameScreenScript afterGameScreenScript;
    public int StarsEarned()
    {
        if(wizard != null)
        {
            HealthNew health = wizard.GetComponentInChildren<HealthNew>();
            if (health != null)
            {
                int currentHealth = health.health;
                int totalHealth = health.GetTotalHealth();
                if (currentHealth > (.9 * totalHealth))
                {
                    //Debug.Log("returning 3 stars after wizard");
                    return 3;
                }
                if (currentHealth > (.5 * totalHealth))
                {
                    return 2;
                }
                if (currentHealth > 0)
                {
                    return 1;
                }
            }
        }
        Debug.Log("returning 0 stars after wizard");
        return 0; 
    }
    private void ShowVictoryScreen()
    {
        gameObject.SetActive(true); 
        int stars = StarsEarned();
        //Debug.Log(stars);
        for (int i = stars; i > 0; i -= 1)
        {
            string star = "Star" + i.ToString();
            gameObject.transform.Find(star).gameObject.SetActive(true);
        }
        if(stars > 0 && compeletedStarSetActive == false)
        {
            compeletedStarSetActive = true;
            gameObject.transform.Find("VictoryText").gameObject.SetActive(true);
            //int starDifference = stars - SaveAndLoad.Get().
            //Above needs to find the number of stars previous attempt
            string currentBattleLevelNumber = SceneManager.GetActiveScene().name.Last().ToString();
            int starsBeforeUpdate = SaveAndLoad.Get().GetLevelsCompleted()[int.Parse(currentBattleLevelNumber)];
            SaveAndLoad.Get().UpdateLevelCompleted(int.Parse(currentBattleLevelNumber), stars);
            SaveAndLoad.SaveData();
            numberOfStarsBeforeLevelBeaten = starsBeforeUpdate;


            int battleNumber = int.Parse(currentBattleLevelNumber);
            if (battleNumber == 0)
            {
                SteamConnection.UnlockAchievement("ACH_LEVEL1");
            }
            else if (battleNumber == 4)
            {
                SteamConnection.UnlockAchievement("ACH_LEVEL5");
            }
            else if (battleNumber == 9)
            {
                SteamConnection.UnlockAchievement("ACH_LEVEL10");
            }
            if (stars > numberOfStarsBeforeLevelBeaten)
            {
                if (starsBeforeUpdate == 0)
                {
                    addNewPath = true;
                }
                else
                {
                    addNewPath = false;
                }
                
                if (battleNumber < 3)
                {
                    SaveAndLoad.Get().UpdateTechTreePoints(4 * (stars - starsBeforeUpdate));
                }
                else if (battleNumber < 5)
                {
                    SaveAndLoad.Get().UpdateTechTreePoints(6 * (stars - starsBeforeUpdate));
                }
                else if(battleNumber < 7) {
                    SaveAndLoad.Get().UpdateTechTreePoints(8 * (stars - starsBeforeUpdate));
                }
                else if (battleNumber < 8)
                {
                    SaveAndLoad.Get().UpdateTechTreePoints(12 * (stars - starsBeforeUpdate));
                }
                else
                {
                    SaveAndLoad.Get().UpdateTechTreePoints(15 * (stars - starsBeforeUpdate));
                }
                SaveAndLoad.SaveData();
                if(SaveAndLoad.Get().GetLevelsCompleted()[9] <= 0)
                {
                    addNewStars = true;
                }
            }
            else
            {
                addNewStars = false;
                addNewPath = false;
            }
            levelNumberStarsJustAddedTo = int.Parse(currentBattleLevelNumber);
            //Debug.Log(SaveAndLoad.Get().GettechTreePoints());

        }
        else
        {
            addNewStars = false;
            addNewPath = false;
        }
        if (stars == 0 && compeletedStarSetActive == false)
        {
            compeletedStarSetActive = true;
            gameObject.transform.Find("DefeatText").gameObject.SetActive(true);
        }

    }
    public static void ShowVictoryScreen_Static()
    {
        afterGameScreenScript.ShowVictoryScreen();
    }
    private void HideVictoryScreen()
    {
        gameObject.SetActive(false);
    }
    public static void HideVictoryScreen_Static()
    {
        afterGameScreenScript.HideVictoryScreen();
    }
    public void ContinueButton()
    {
        GameObject pauseMenu = GameObject.Find("Canvas");
        pauseMenu.GetComponent<PauseMenuScript>().Resume();
        //TimeManagement.StopPause();
        SceneManager.LoadScene("Map");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        afterGameScreenScript = this;
        wizard = GameObject.Find("Wizard");
        //Debug.Log(wizard);
        HideVictoryScreen_Static();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
