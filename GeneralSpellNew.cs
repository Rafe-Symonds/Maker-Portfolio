using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class GeneralSpellNew : MonoBehaviour
{
    public int manaUsage;
    public int duration;
    public int coolDownInSeconds;
    public bool overTimeAffects;
    public bool offSetPositionOnCreation;
    private bool done = false;


    public enum SpellType { Stationary, Moving, Summoner, FullScreen, TwoClicks, TroopSpell };
    public SpellType spellType;

    public EmptyBackgroundScript.OverridePosition overridePosition;
    private SpecificSpellNew specialSpellScript = null;
    private GameObject troop = null;
    public GameObject onSpawnEffectPrefab;


    public int timeIntervalInMS;
    private TimeSpan timeSpanInMS;
    private DateTime nextTime = TimeManagement.CurrentTime();
    
    public void spellAffect(GameObject gameobject)
    {
        if (specialSpellScript != null)
        {
            specialSpellScript.spellAffectPolymorphic(gameobject);
            nextTime = TimeManagement.CurrentTime() + timeSpanInMS;
        }
    }
    public void spellAffectOverTime(GameObject gameobject)
    {

        //
        //
        //DOES NOT WORK because it shares a timer with the SpellAffectOverTimeNotNeedingTrigger
        //
        //



        //Debug.Log("spell affect over time" + specialSpellScript + "      " + overTimeAffects);
        if (nextTime < TimeManagement.CurrentTime() && overTimeAffects && specialSpellScript != null)
        {
            //Debug.Log("time to do the spell affect over time");
            specialSpellScript.spellAffectOverTimePolymorphic(gameobject);


            nextTime = TimeManagement.CurrentTime() + timeSpanInMS;
        }
    }
    public void spellAffectOverTimeEnd(GameObject gameobject)
    {
        if (specialSpellScript != null)
        {
            //Debug.Log("General Spell SpellAffectOverTimeEnd");
            specialSpellScript.spellAffectOverTimeEndPolymorphic(gameobject);
        }
    }
    public bool OffSetCreationPositionOffGround()
    {
        return offSetPositionOnCreation;
    }
    public void SetTroop(GameObject troop)
    {
        this.troop = troop;
    }
    public GameObject Gettroop()
    {
        return troop;
    }


    private void OnDestroy()
    {
        if (done == false)
        {
            done = true;

            GeneralSpawnNew generalSpawn = gameObject.GetComponentInParent<GeneralSpawnNew>();
            if(generalSpawn != null)
            {
                generalSpawn.FinishCastingSpell();
            }
            
        }
    }
    void Awake()
    {
        timeSpanInMS = new TimeSpan(0, 0, 0, 0, timeIntervalInMS);


        specialSpellScript = GetComponent<SpecificSpellNew>();
        //Debug.Log(specialSpellScript);
        if (specialSpellScript != null)
        {
            specialSpellScript.updateSpellCleanUpAfterSeconds(duration);
        }
        //Debug.Log(duration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nextTime < TimeManagement.CurrentTime() && overTimeAffects)
        {
            specialSpellScript.spellAffectOverTimeNotNeedingTrigger();
            nextTime = TimeManagement.CurrentTime() + timeSpanInMS;
        }
    }
}
