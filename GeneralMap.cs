using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneralMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeManagement.SetPauseTimeToZero();
        SetUpMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpMap()
    {
        int[] levels = SaveAndLoad.Get().GetLevelsCompleted();
        //Debug.Log("-------------------------- " + levels.Length);
        //Debug.Log(levels[0]);
        //Debug.Log(levels[1]);
        //Debug.Log(levels[2]);


        for (int i = 0; i <= levels.Length - 1; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                GameObject mapShield = GameObject.Find("MapShield" + i.ToString());
                //Debug.Log(mapShield);
                if (mapShield != null)
                {
                    GameObject star = mapShield.transform.Find("Star" + j.ToString()).gameObject;
                    //Debug.Log(star);
                    SpriteRenderer spriteRenderer = star.GetComponent<SpriteRenderer>();
                    spriteRenderer.enabled = false;
                }
            }


            //Debug.Log(levels[i]);
            if (levels[i] == -1)
            {
                //Debug.Log("i = " + i + " value = " + levels[i]);
                GameObject mapShield = GameObject.Find("MapShield" + i.ToString());
                //Debug.Log("MapShield" + i.ToString());
                //Debug.Log(mapShield);
                if (mapShield != null)
                {
                    SpriteRenderer spriteRenderer = mapShield.GetComponent<SpriteRenderer>();
                    spriteRenderer.enabled = false;
                    mapShield.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else
            {
                GameObject mapShield = GameObject.Find("MapShield" + i.ToString());
                //Debug.Log(mapShield);
                mapShield.GetComponent<MapShieldScript>().ShowStars();    // Do not need to worry about the new shield and animation because new shield will have no stars
                //Debug.Log(mapShield.GetComponent<MapShieldScript>());
                //Debug.Log("************ Call Show Stars " + mapShield);
            }

            if (levels[i] <= 0)
            {
                GameObject mapShield = GameObject.Find("MapShield" + i.ToString());

                if (mapShield != null)
                {
                    int numberOfChildren = mapShield.transform.childCount;

                    for (int j = 0; j < numberOfChildren - 4; j++)
                    {
                        Transform mapPath = mapShield.transform.Find("MapPath" + j.ToString());
                        if (mapPath != null)
                        {
                            mapPath.GetComponent<SpriteRenderer>().enabled = false;
                            //Debug.Log("Hiding path " + j);
                        }

                    }
                }
            }
        }

        int showNewLevelAnimation;
        if (AfterGameScreenScript.addNewPath == true)
        {
            showNewLevelAnimation = 2;
        }
        else
        {
            showNewLevelAnimation = 1;
        }

        for (int i = 0; i <= levels.Length - showNewLevelAnimation; i++)
        {
            if (levels[i] > 0)
            {
                //Debug.Log("i = " + i + " value = " + levels[i]);
                GameObject mapShield = GameObject.Find("MapShield" + (i + 1).ToString());
                //Debug.Log("+++++++++++++++ MapShield" + (i + 1).ToString());
                //Debug.Log("++++++++++++++++" + mapShield);
                if (mapShield != null)
                {

                    if (levels[i + 1] == -1)
                    {
                        //Debug.Log("+++++++++++++ " + mapShield);
                        SpriteRenderer spriteRenderer = mapShield.GetComponent<SpriteRenderer>();
                        spriteRenderer.enabled = true;
                        mapShield.GetComponent<BoxCollider2D>().enabled = true;
                        levels[i + 1] = 0;
                        break;
                    }

                }
            }
            //Debug.Log("Shield Number = " + i + "    Stars = " + levels[i]);
        }

        if (showNewLevelAnimation == 2)
        {
            // do animation - maybe move to update
            // ^ shows path and new shield
        }

    }
}
