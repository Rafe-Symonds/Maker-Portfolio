using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayLevelButtonScript : MonoBehaviour
{
    public static int levelNumber;


    private static MapPlayLevelButtonScript mapPlay;

    public void PlayButton()
    {
        string sceneName = "BattleScene" + levelNumber.ToString();
        SceneManager.LoadScene(sceneName);
        //DontDestroyOnLoad(levelType);
    }
    public void TechTreeButton()
    {
        SceneManager.LoadScene("TechTree");
    }

    private void Show_Scroll()
    {
         
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/Text/Story Text/StoryText" + levelNumber.ToString() + ".txt");
        TextMeshProUGUI titleText = gameObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bodyText = gameObject.transform.Find("BodyText").GetComponent<TextMeshProUGUI>();
        titleText.text = reader.ReadLine();
        bodyText.text = reader.ReadToEnd();

        reader.Close();

        gameObject.SetActive(true);
    }
    public void Hide_Scroll()
    {
        gameObject.SetActive(false);
    }
    public static void Show_Scroll_Static()
    {
        
        mapPlay.Show_Scroll();
        
    }
    public static void Hide_Scroll_Static()
    {
        mapPlay.Hide_Scroll();
    }

    // Start is called before the first frame update
    void Awake()
    {
        mapPlay = this;
        Hide_Scroll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
