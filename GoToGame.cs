using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{
    public void PlayGame()
    {
        SaveAndLoad.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CampainMap()
    {
        SaveAndLoad.SaveData();
        SceneManager.LoadScene("Map");
    }
    public void TechTreePointChange()
    {
        TextMeshProUGUI pointText = transform.Find("TechTreePoints").Find("Points").GetComponent<TextMeshProUGUI>();
        int totalPoints = SaveAndLoad.Get().GettechTreePoints();
        pointText.text = totalPoints.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        TechTreePointChange();
        SaveAndLoad.techTreePointsEvents.AddListener(TechTreePointChange);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //PauseMenuScript.StaticResume();
    }
}
