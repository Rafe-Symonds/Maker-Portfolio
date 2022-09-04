using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawn : MonoBehaviour
{
    public GameObject sunPrefab;
    public float sunSize = 1;
    public GameObject moonPrefab;
    public float moonSize = 1;
    public GameObject midDay;
    public GameObject dawn;
    private SkyTest sky;
    private GameObject sun;
    private GameObject moon;
    public bool levelHasSun = true;
    public bool levelHasMoon = true;
    private bool moonPresent = false;
    private bool sunPresent = false;
    TimeSpan timeBetweenDamageModification = new TimeSpan(0, 0, 10);
    DateTime nextDamageModification = TimeManagement.CurrentTime();

    // Start is called before the first frame update
    void Start()
    {
        sky = gameObject.GetComponent<SkyTest>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            if (sky.colorValue > 0.8 && moonPresent == false)
            {
                if (levelHasMoon == true)
                {
                    moon = Instantiate(moonPrefab, dawn.transform.position, Quaternion.identity);
                    moon.transform.localScale *= moonSize;
                    moonPresent = true;
                }
                if (levelHasSun == true)
                {
                    Destroy(sun);
                    sunPresent = false;
                }
            }
            if (sky.colorValue < 0.2 && sunPresent == false)
            {
                if (levelHasSun == true)
                {
                    sun = Instantiate(sunPrefab, dawn.transform.position, Quaternion.identity);
                    sun.transform.localScale *= sunSize;
                    sunPresent = true;
                }
                if (levelHasMoon == true)
                {
                    Destroy(moon);
                    moonPresent = false;
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error in the sunspawn script");
        }
        
        /*
        if (nextDamageModification < TimeManagement.CurrentTime() && moonPresent == true)
        {
            
            List<GameObject> gameobjects = new List<GameObject>();
            nextDamageModification = TimeManagement.CurrentTime() + timeBetweenDamageModification;
            foreach (GameObject gameobject in GameObject.FindObjectsOfType(typeof(GameObject)))
            {
                if (gameobject.name == "ContactCollider")
                {
                    if (!gameobject.transform.parent.CompareTag("team1Base") && !gameobject.transform.parent.CompareTag("team2Base"))
                    {
                        gameobjects.Add(gameobject);
                    }
                }
            }
            foreach (GameObject gameobject in gameobjects)
            {
                if(gameobject != null)
                {
                    HealthNew health = gameobject.GetComponent<HealthNew>();
                    DamageNew.DamageType[] damageTypes = health.creatureCharacteristics;
                    foreach (DamageNew.DamageType damageTypeOnTroop in damageTypes)
                    {
                        //Debug.Log("looping through creature's protections");
                        if (damageTypeOnTroop == DamageNew.DamageType.undead)
                        {
                            DamageNew damage = gameobject.GetComponent<DamageNew>();
                            damage.AddDamageEffect(new DamageNew.DamageEffect(0.5f, 9));
                            break;
                        }
                    }
                }
                
            }
            
        } 
        */
    }
    public bool GetSunPresent()
    {
        return sunPresent;
    }
    public bool GetMoonPresent()
    {
        return moonPresent;
    }
}
