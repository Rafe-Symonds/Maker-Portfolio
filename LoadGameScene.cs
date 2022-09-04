using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    public GameObject hotbar;
    public enum LevelType { Basic, Forest, Desert, Mountain, Snow, Lava, RuinedCity, Underdark }
    public static LevelType levelType;
    public static DamageNew.DamageType damageEffectType;
    public static float damageEffectAmount;
    public static DamageNew.DamageType constantDamageType;
    public static int constantDamageAmount;
    public static MapShieldScript.ConstantDamageTime constantDamageTime;
    public static int moneyPerSecond = 1;
    public GameObject treePrefab;
    public GameObject riverPrefab;
    public GameObject mountainPrefab;
    private static bool sceneCallBackIsSet = false;
    
    private void Awake()
    {
        if(sceneCallBackIsSet == false)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            sceneCallBackIsSet = true;
        }
        moneyPerSecond = 4 - SaveAndLoad.Get().GetGameDifficulty();
    }
    
    // Start is called before the first frame update
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        //Debug.Log("SceneLoad");

        if (SceneManager.GetActiveScene().name.Contains("BattleScene"))
        {
            TimeManagement.SetPauseTimeToZero();
            Debug.Log("loading into " + SceneManager.GetActiveScene().name);
            if (SaveAndLoad.Get().GetHotbarSkills() != null)
            {
                for (int i = 0; i <= SaveAndLoad.Get().GetHotbarSkills().Length - 1; i++)
                {
                    
                    float pos = -7f + (1.5f * i);
                    float posY = -4f;
                    GameObject button = Instantiate(hotbar, new Vector3(pos, posY, -0.001f), Quaternion.identity);
                    button.name = "Hotbar" + i.ToString();
                    if (SaveAndLoad.Get().GetHotbarSkillImages()[i] != null)
                    {
                        GameObject buttonImage = Instantiate(Resources.Load("Prefabs/TechTree/" + 
                                                 SaveAndLoad.Get().GetHotbarSkillImages()[i]) as GameObject, 
                                                 new Vector3(pos, posY, 0f), Quaternion.identity);
                        //Debug.Log("buttonImage = " + buttonImage);
                        //Destroy so the button works on game screen
                        //Destroy(buttonImage.GetComponent<GeneralTechTree>());  Disabled so the image on the Battle Scene still displayes for the hotbar
                        Destroy(buttonImage.GetComponent<MeshRenderer>());
                        Destroy(buttonImage.GetComponent<BoxCollider>());
                        buttonImage.transform.localScale = new Vector3(1f, 1f, 1f);
                        buttonImage.transform.SetParent(button.transform);
                        StreamReader stream = new StreamReader(Application.streamingAssetsPath + "/Text/TechTreeText/" + SaveAndLoad.Get().GetHotbarSkills()[i] + "_Skill.txt");
                        TextMeshProUGUI tooltip = button.transform.Find("Canvas").Find("ToolTip").GetComponent<TextMeshProUGUI>();
                        if (SceneManager.GetActiveScene().name == "BattleScene1")
                        {
                            tooltip.color = Color.black;
                        }
                        try
                        {
                            tooltip.text = stream.ReadLine();
                            string subtitle = stream.ReadLine();
                            bool endOfFile = false;
                            while (!endOfFile)
                            {
                                string temp = stream.ReadLine();
                                if(temp == "" || temp == null)
                                {
                                    break;
                                }
                                if (!temp.Contains("Money") && !temp.Contains("Mana"))
                                {
                                    tooltip.text += "\n" + temp;
                                }
                                
                            }
                            
                            
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("file too short " + button.name);
                        }
                        
                    }
                    //Debug.Log("Prefabs/TechTree/" + SaveAndLoad.Get().GetHotbarSkills()[i]);
                    //Debug.Log("Spider Prefab = " + (Resources.Load("Prefabs/TechTree/" + SaveAndLoad.Get().GetHotbarSkills()[i]) as GameObject));
                    button.GetComponent<GeneralSpawnNew>().prefab = Resources.Load("Prefabs/SpellsNew/" +
                                                                 SaveAndLoad.Get().GetHotbarSkills()[i]) as GameObject;
                    
                    if (button.GetComponent<GeneralSpawnNew>().prefab == null)
                    {
                        button.GetComponent<GeneralSpawnNew>().prefab = Resources.Load("Prefabs/TroopsNew/" +
                                                                 SaveAndLoad.Get().GetHotbarSkills()[i]) as GameObject;
                    }
                    //Debug.Log("LoadGameScene +  " + button.GetComponent<GeneralSpawn>().prefab);

                }
            }//Hotbar instantiation
            switch (levelType)
            {
                case LevelType.Forest:
                    Vector3 pos = new Vector3(-10, -.4f, 0);
                    System.Random rand = new System.Random();
                    for (int i = 0; i < 40; i++)
                    {

                        GameObject tree = Instantiate(treePrefab, pos, Quaternion.identity);
                        PlaceOnGround(tree, pos);
                        pos.x += ((float)rand.Next(1, 5) / 4);
                    }
                    break;

            }
        }
        
    }

    public void PlaceOnGround(GameObject thing, Vector3 pos)
    {
        bool found = false;
        RaycastHit2D[] hitsUp = Physics2D.RaycastAll(pos, Vector2.up, 10);
        //Debug.DrawRay(pos, Vector3.up, UnityEngine.Color.green, 10);
        for (int j = 0; j <= hitsUp.Length - 1; j++)
        {
            //Debug.Log(hit[i].collider.name);
            if (hitsUp[j].collider != null && hitsUp[j].collider.name == "Ground")
            {
                Vector2 point = hitsUp[j].point;
                pos.y = point.y;
                found = true;
                break;
            }

        }

        if (found == false)
        {
            RaycastHit2D[] hitsDown = Physics2D.RaycastAll(pos, Vector2.down, 10);
            //Debug.DrawRay(pos, Vector3.down, UnityEngine.Color.red, 10);
            for (int j = 0; j <= hitsDown.Length - 1; j++)
            {
                //Debug.Log(hit[i].collider.name);
                if (hitsDown[j].collider != null && hitsDown[j].collider.name == "Ground")
                {
                    Vector2 point = hitsDown[j].point;
                    pos.y = point.y;
                    break;
                }
            }
        }
        thing.transform.position = pos;

        thing.transform.position = new Vector3(thing.transform.position.x,
                                thing.transform.position.y + thing.GetComponent<BoxCollider2D>().bounds.extents.y,
                                thing.transform.position.z);


    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
