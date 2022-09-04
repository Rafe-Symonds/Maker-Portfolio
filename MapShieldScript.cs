using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapShieldScript : MonoBehaviour
{
    public LoadGameScene.LevelType levelType;
    public DamageNew.DamageType modifyDamageType;
    public float modifyDamageAmount;
    public DamageNew.DamageType constantDamageType;
    public int constantDamageAmount;
    public enum ConstantDamageTime{ Day, Night, All}
    public ConstantDamageTime constantDamageTime;
    private int levelNumber;
    bool startedStarAnimation = false;
    bool completedStarAnimation = false;
    DateTime timeStarExplosionAnimationEnds;



    DateTime mapPathCreation;
    private int numberOfChildren;
    private int currentMapPath;
    private bool setMapPathCreation = false;


    void OnMouseDown()
    {
        MapPlayLevelButtonScript.levelNumber = levelNumber;
        //Debug.Log("Map Shield " + levelNumber);
        LoadGameScene.levelType = levelType;
        LoadGameScene.damageEffectType = modifyDamageType;
        LoadGameScene.damageEffectAmount = modifyDamageAmount;
        LoadGameScene.constantDamageType = constantDamageType;
        LoadGameScene.constantDamageAmount = constantDamageAmount;
        LoadGameScene.constantDamageTime = constantDamageTime;
        MapPlayLevelButtonScript.Show_Scroll_Static();
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("            ***************** " + levelNumber +" " + gameObject);
        string name = gameObject.name;
        name = name.Substring(9);
        levelNumber = int.Parse(name);
        numberOfChildren = gameObject.transform.childCount;
        currentMapPath = 0;
        if (AfterGameScreenScript.addNewPath == true)
        {
            GameObject mapShield = GameObject.Find("MapShield" + AfterGameScreenScript.levelNumberStarsJustAddedTo.ToString());
            int numberOfChildren = mapShield.transform.childCount;

            for (int j = 0; j < numberOfChildren - 4; j++)
            {
                Transform mapPath = mapShield.transform.Find("MapPath" + j.ToString());
                if (mapPath != null)
                {
                    mapPath.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log("Setting mapPath to false in the ShowStars Method " + mapPath);
                }
            }
        }
    }

    public void ShowStars()
    {
        int[] levels = SaveAndLoad.Get().GetLevelsCompleted();
        int stars = levels[levelNumber];
        //Debug.Log(stars);
        
        for (int i = stars; i > 0; i -= 1)
        {
            GameObject star = gameObject.transform.Find("Star" + i.ToString()).gameObject;
            //Debug.Log(gameObject);
            //Debug.Log("         " + star);
            SpriteRenderer spriteRenderer = star.GetComponent<SpriteRenderer>();
            //Debug.Log("         " + spriteRenderer);
            spriteRenderer.enabled = true;
        }
        if (AfterGameScreenScript.addNewStars == true && startedStarAnimation == false)
        {
            startedStarAnimation = true;


            int numberOfStarsAfterLevelBeaten = SaveAndLoad.Get().GetLevelsCompleted()[AfterGameScreenScript.levelNumberStarsJustAddedTo];
            int numberOfStarsBeforeLevelBeaten = AfterGameScreenScript.numberOfStarsBeforeLevelBeaten;
            int differenceInStars = numberOfStarsAfterLevelBeaten - numberOfStarsBeforeLevelBeaten;
            Debug.Log("Difference In Stars When Adding More Stars = " + differenceInStars);


            GameObject mapShield = GameObject.Find("MapShield" + AfterGameScreenScript.levelNumberStarsJustAddedTo.ToString());
            if (gameObject == mapShield)
            {
                for (int i = 3; i > numberOfStarsBeforeLevelBeaten; i--)
                {
                    //hiding the new shield stars so they can have an animation
                    GameObject newStar = gameObject.transform.Find("Star" + i.ToString()).gameObject;
                    SpriteRenderer spriteRenderer = newStar.GetComponent<SpriteRenderer>();
                    spriteRenderer.enabled = false;
                }


                GameObject starPrefab = Resources.Load("Prefabs/General/StarMapMoving") as GameObject;
                GameObject spawnPointLeft = GameObject.Find("SpawnPointLeft");
                GameObject spawnPointRight = GameObject.Find("SpawnPointRight");
                GameObject spawnPointTop = GameObject.Find("SpawnPointTop");

                if (numberOfStarsAfterLevelBeaten == 3) //3rd star is always animated when 3rd star is gained
                {
                    GameObject star3 = Instantiate(starPrefab, spawnPointTop.transform.position, Quaternion.identity);
                    star3.GetComponent<MovingMapStar>().MoveStarToShield(mapShield);
                }
                if(numberOfStarsAfterLevelBeaten >= 2 && numberOfStarsBeforeLevelBeaten < 2) //2nd star
                {
                    GameObject star2 = Instantiate(starPrefab, spawnPointRight.transform.position, Quaternion.identity);
                    star2.GetComponent<MovingMapStar>().MoveStarToShield(mapShield);
                }
                if(numberOfStarsAfterLevelBeaten >= 1 && numberOfStarsBeforeLevelBeaten < 1) //1st star
                {
                    GameObject star1 = Instantiate(starPrefab, spawnPointLeft.transform.position, Quaternion.identity);
                    star1.GetComponent<MovingMapStar>().MoveStarToShield(mapShield);
                }
                if(numberOfStarsAfterLevelBeaten == 0)
                {
                    return;
                }
                Invoke("ShowStars", 1);
                Invoke("StarExplosionAnimation", 2);
            }
        }

        
    }

    public void StarExplosionAnimation()
    {
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        if (animator.GetBool("starExplosion") == false)
        {
            animator.SetBool("starExplosion", true);
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
            timeStarExplosionAnimationEnds = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 0, 0, (int)animator.GetCurrentAnimatorStateInfo(0).length * 1000);
            completedStarAnimation = true;
            Invoke("SetStarExplosionAnimatorToTrue", (int)animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void SetStarExplosionAnimatorToTrue()
    {
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        animator.SetBool("starExplosion", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if(TimeManagement.CurrentTime() > timeStarExplosionAnimationEnds && completedStarAnimation == true)
        {
            Animator animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetBool("starExplosion", false);
        }
        */
        if(completedStarAnimation == true)
        {
            GameObject mapShield = GameObject.Find("MapShield" + AfterGameScreenScript.levelNumberStarsJustAddedTo.ToString());
            if (AfterGameScreenScript.addNewPath == true && gameObject == mapShield)
            {
                if (setMapPathCreation == false)
                {
                    mapPathCreation = TimeManagement.CurrentTime();
                    setMapPathCreation = true;
                }
                


                if (TimeManagement.CurrentTime() > mapPathCreation)
                {
                    if (currentMapPath < numberOfChildren)
                    {
                        Transform mapPath = gameObject.transform.Find("MapPath" + currentMapPath.ToString());
                        if (mapPath != null)
                        {
                            mapPath.GetComponent<SpriteRenderer>().enabled = true;
                            Debug.Log("Showing the path " + currentMapPath);
                            currentMapPath++;
                            
                        }
                        mapPathCreation = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 1);
                    }
                }
            }
        }
    }
}
