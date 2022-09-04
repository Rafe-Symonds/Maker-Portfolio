using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour
{
    private TimeSpan timeBewteenDamages = new TimeSpan(0, 0, 10);
    private DateTime nextDamageOverTime = TimeManagement.CurrentTime();
    public static void ModifyDamage(DamageNew.DamageType damageType, float damageIncreaseAmount, GameObject gameobject)
    {
        
        
            HealthNew health = gameobject.GetComponent<HealthNew>();
            DamageNew.DamageType[] damageTypes = health.creatureCharacteristics;
            
            foreach(DamageNew.DamageType damageTypeOnTroop in damageTypes)
            {
                if(damageTypeOnTroop == damageType)
                {
                    DamageNew damage = gameobject.GetComponent<DamageNew>();
                    damage.AddDamageEffect(new DamageNew.DamageEffect(damageIncreaseAmount, 1000000));
                }
            }
        
    }
    public static void TakeDamageOverTime(DamageNew.DamageType damageType, int damagePerTime)
    {
        if(damagePerTime == 0)
        {
            return;
        }
        List<GameObject> gameobjects = new List<GameObject>();
        foreach (GameObject gameobject in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (gameobject != null && gameobject.name == "ContactCollider")
            {
                if(!gameobject.transform.parent.CompareTag("team1Base") && !gameobject.transform.parent.CompareTag("team2Base"))
                {
                    gameobjects.Add(gameobject);
                } 
            }   
        }
        foreach (GameObject gameobject in gameobjects)
        {
            bool creatureHasCorrectProtection = false;
            HealthNew health = gameobject.GetComponent<HealthNew>();
            DamageNew.DamageType[] damageTypes = health.creatureCharacteristics;
            foreach (DamageNew.DamageType damageTypeOnTroop in damageTypes)
            {
                //Debug.Log("looping through creature's protections");
                if (damageTypeOnTroop == damageType)
                {
                    creatureHasCorrectProtection = true;
                    //Debug.Log("Not Dealing Damage");
                    break;
                }
            }
            if(creatureHasCorrectProtection == false)
            {
                health.TakeDamage(damagePerTime, damageType);
                //Debug.Log("Dealing Damage");
            }
        }
    }
    






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(nextDamageOverTime < TimeManagement.CurrentTime())
        {
            GameObject sky = GameObject.Find("Sky");
            if (LoadGameScene.constantDamageTime == MapShieldScript.ConstantDamageTime.All)
            {
                nextDamageOverTime = TimeManagement.CurrentTime() + timeBewteenDamages;
                TakeDamageOverTime(LoadGameScene.constantDamageType, LoadGameScene.constantDamageAmount);
            }
            else if (LoadGameScene.constantDamageTime == MapShieldScript.ConstantDamageTime.Day && sky.GetComponent<SunSpawn>().GetSunPresent() == true)
            {
                nextDamageOverTime = TimeManagement.CurrentTime() + timeBewteenDamages;
                TakeDamageOverTime(LoadGameScene.constantDamageType, LoadGameScene.constantDamageAmount);
            }
            else if (LoadGameScene.constantDamageTime == MapShieldScript.ConstantDamageTime.Night && sky.GetComponent<SunSpawn>().GetMoonPresent() == true)
            {
                nextDamageOverTime = TimeManagement.CurrentTime() + timeBewteenDamages;
                TakeDamageOverTime(LoadGameScene.constantDamageType, LoadGameScene.constantDamageAmount);
            }
        }
    }
}
