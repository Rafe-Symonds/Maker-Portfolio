using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpawn : MonoBehaviour
{ }
/*
private GameObject spawnPoint;
public GameObject prefab;
private ManaBar manabar;
EmptyBackgroundScript backgroundScript;
private GameObject emptyBackground;
private GameObject pointToSpawn;
private Vector3 firstClick = Vector3.zero;
private GameObject spell;
private GameObject hotbarSparkle;
private GameObject hotbarGlow;
private GameObject coolDownSquarePrefab;


private int sparkleDirection = 0;



public void BackgroundClick(Vector3 pos)
{
    //Debug.Log("BackgroundClick");
    if (prefab != null)
    {
        GeneralSpell generalSpell = prefab.GetComponent<GeneralSpell>();
        if (manabar.CheckAndReduceMana(generalSpell.manaUsage) == true)
            {
            if (generalSpell.spellType == GeneralSpell.SpellType.Moving)
            {
                //e.g. fireball
                GameObject instantiate = Instantiate(prefab, pointToSpawn.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
                instantiate.GetComponent<GeneralMovingSpellScript>().Target(pos);
                //Debug.Log(spawnPoint);
                ChangeHotbarGlow();
            }
            else if(generalSpell.spellType == GeneralSpell.SpellType.TwoClicks)
            {
                if( firstClick == Vector3.zero)
                {
                    firstClick = pos;
                }
                else
                {
                    GameObject instantiate1 = Instantiate(prefab, firstClick, Quaternion.identity, gameObject.transform);
                    GameObject instantiate2 = Instantiate(prefab, pos, Quaternion.identity, gameObject.transform);
                    if(generalSpell.OffSetCreationPositionOffGround())
                    {
                        instantiate1.transform.position = new Vector3(instantiate1.transform.position.x, 
                                        instantiate1.transform.position.y + instantiate1.GetComponent<BoxCollider2D>().bounds.extents.y, 
                                        instantiate1.transform.position.z);
                        instantiate2.transform.position = new Vector3(instantiate2.transform.position.x,
                                        instantiate2.transform.position.y + instantiate2.GetComponent<BoxCollider2D>().bounds.extents.y,
                                        instantiate2.transform.position.z);


                    }
                    ChangeHotbarGlow();
                    backgroundScript.ClearCallBack();
                }
            }
            else
            {
                //Stationary Spawn e.g. Arrow spell
                GameObject instantiate = Instantiate(prefab, pos, Quaternion.identity, gameObject.transform) as GameObject;
                ChangeHotbarGlow();


            }
            backgroundScript.ClearCallBack();



        }
    }
}


public void TroopClick(GameObject troop)
{

    if (prefab != null)
    {
        //Debug.Log("GeneralSpawn troop click");

        GameObject spell = Instantiate(prefab, troop.transform.position, Quaternion.identity, gameObject.transform);
        GeneralSpell generalSpell = spell.GetComponent<GeneralSpell>();
        generalSpell.SetTroop(troop);
        SpecificSpell specificSpell = spell.GetComponent<SpecificSpell>();
        specificSpell.spellAffectOnSpecificTroopPolymorphic();

        ChangeHotbarGlow();
        backgroundScript.ClearCallBackTroop();

    }


}

/*
public void HotbarGlow(bool yellow)
{


    GameObject hotbarGlowPrefab = Resources.Load("Prefabs/HotbarGlow") as GameObject;
    GameObject hotbarGlowContinuesPrefab = Resources.Load("Prefabs/HotbarGlowContinues") as GameObject;

    GeneralSpell generalSpell = prefab.GetComponent<GeneralSpell>();
    if(yellow)
    {
        hotbarGlow = Instantiate(hotbarGlowPrefab, transform.position, Quaternion.identity, gameObject.transform);
    }
    else
    {
        hotbarGlow = Instantiate(hotbarGlowContinuesPrefab, transform.position, Quaternion.identity, gameObject.transform);
    }    

}



public void CreateSparkleEffect()
{
    GameObject hotbarSparklePrefab = Resources.Load("Prefabs/General/CoolDownEffect") as GameObject;
    hotbarSparkle = Instantiate(hotbarSparklePrefab, new Vector3(transform.position.x, transform.position.y + .5f, -1), Quaternion.identity, gameObject.transform);
}    

public void ClearHotbarGlow()
{
    if(hotbarGlow != null)
    {
        Destroy(hotbarGlow);
        hotbarGlow = null;
    }

}
public void ClearYellowHotbarGlow()
{

    //Debug.Log(hotbarGlow.name);
    Destroy(hotbarSparkle);
    hotbarGlow = null;
    hotbarSparkle = null;

    Transform coolDownSquare = gameObject.transform.Find("CoolDownSquare");
    if(coolDownSquare != null)
    {
        Destroy(coolDownSquare.gameObject);

    }
}
public void ChangeHotbarGlow()
{
    GameObject hotbarGlowContinuesPrefab = Resources.Load("Prefabs/General/HotbarGlowContinues") as GameObject;
    Destroy(hotbarSparkle);
    hotbarGlow = Instantiate(hotbarGlowContinuesPrefab, transform.position, Quaternion.identity, gameObject.transform);
}
public void ClearGreenHotbarGlow()
{
    if (hotbarGlow != null && hotbarGlow.name.Equals("HotbarGlowContinues(Clone)"))
    {
        //Debug.Log(hotbarGlow.name);
        Destroy(hotbarGlow);
        hotbarGlow = null;
    }
}
public void CreateCoolDown(int timeInSeconds)
{
    GameObject coolDownObject = Instantiate(coolDownSquarePrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity, gameObject.transform);
    coolDownObject.name = "CoolDownSquare";
    coolDownObject.GetComponent<CoolDownScript>().timeInSeconds = timeInSeconds;
    MeshRenderer meshRenderer = coolDownObject.GetComponent<MeshRenderer>();
    meshRenderer.sortingOrder = 10;
}

public void OnMouseDown()
{
    //Debug.Log(prefab);
    GeneralSpell generalSpell = prefab.GetComponent<GeneralSpell>();
    if (generalSpell != null)
    {
        string name = prefab.name;
        //Debug.Log(name);
        if (generalSpell.oneAtATime && GameObject.Find(name + "(Clone)") != null)
        {

            //To ckeck if a spell is still running and is a once only
            return;
        }
    }

    Debug.Log("GeneralSpawn clicked");
    if (prefab != null)
    {

        if (generalSpell != null)
        {
            if (generalSpell.spellType == GeneralSpell.SpellType.Moving || generalSpell.spellType == GeneralSpell.SpellType.Stationary)
            {

                backgroundScript.SetCallBack(new Action<Vector3>(BackgroundClick), generalSpell.overridePosition, false, new Action(ClearYellowHotbarGlow), new Action(ClearYellowHotbarGlow));
                CreateSparkleEffect();

                //This line moves empty background forwards so that it can be clicked on instead on a troop
                emptyBackground.transform.position = new Vector3(emptyBackground.transform.position.x, emptyBackground.transform.position.y, -.01f);
            }
            else if(generalSpell.spellType == GeneralSpell.SpellType.TwoClicks)
            {
                backgroundScript.SetCallBack(new Action<Vector3>(BackgroundClick), generalSpell.overridePosition, true, new Action(ClearYellowHotbarGlow), new Action(ClearYellowHotbarGlow));
                CreateSparkleEffect();
            }
            else if (generalSpell.spellType == GeneralSpell.SpellType.Summoner)
            {

                if (GameObject.FindGameObjectWithTag("team1Summoner") == null)
                {
                    if (manabar.CheckAndReduceMana(generalSpell.manaUsage) == true)
                    {
                        Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, gameObject.transform);
                        CreateSparkleEffect();
                        //UnityEngine.Debug.Log("summoner");
                    }
                }
                else
                {
                    return;
                }
            }
            else if( generalSpell.spellType == GeneralSpell.SpellType.FullScreen)
            {
                Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity, gameObject.transform);
                CreateSparkleEffect();
            }
            else if(generalSpell.spellType == GeneralSpell.SpellType.TroopSpell)
            {
                Debug.Log("SetTroopCallBack");
                backgroundScript.SetCallBackTroop(new Action<GameObject>(TroopClick), ClearYellowHotbarGlow, new Action(ClearYellowHotbarGlow));
                CreateSparkleEffect();

            }
            CreateCoolDown(prefab.GetComponent<GeneralSpell>().coolDownInSeconds);
        }


        /*
        GeneralMovement generalMovement = prefab.GetComponent<GeneralMovement>();
        if (generalMovement != null)
        {
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, gameObject.transform);
            CreateCoolDown(prefab.GetComponent<GeneralMovement>().coolDownInSeconds);
            Transform coolDownSquare = gameObject.transform.Find("CoolDownSquare");

            if(coolDownSquare != null)
            {
                CoolDownScript coolDownScript = coolDownSquare.GetComponent<CoolDownScript>();
                if(coolDownScript != null)
                {
                    coolDownScript.StartTimer();

                }
            }
        }


        Transform contactCollider = prefab.transform.Find("ContactCollider");
        if (contactCollider != null)
        {
            GeneralMovementNew generalMovementNew = contactCollider.GetComponent<GeneralMovementNew>();

            //Money
            GeneralMoney money = GameObject.Find("Money").GetComponent<GeneralMoney>();
            int moneyCost = prefab.transform.Find("ContactCollider").GetComponent<GeneralMovementNew>().moneyCost;
            if (money.CheckAndReduceMoney(moneyCost))
            {
                Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, gameObject.transform);
                CreateCoolDown(contactCollider.GetComponent<GeneralMovementNew>().coolDownInSeconds);
                Transform coolDownSquare = gameObject.transform.Find("CoolDownSquare");

                if (coolDownSquare != null)
                {
                    CoolDownScript coolDownScript = coolDownSquare.GetComponent<CoolDownScript>();
                    if (coolDownScript != null)
                    {
                        coolDownScript.StartTimer();

                    }
                }
            }

        }
    }

}




// Start is called before the first frame update
void Start()
{
    coolDownSquarePrefab = Resources.Load("Prefabs/General/CoolDownSquare") as GameObject;
    manabar = GameObject.Find("Manabar").GetComponent<ManaBar>();
    backgroundScript = GameObject.FindObjectOfType(typeof(EmptyBackgroundScript)) as EmptyBackgroundScript;
    emptyBackground = GameObject.Find("EmptyBackground");
    if (prefab != null)
    {
        GeneralSpell generalSpell = prefab.GetComponent<GeneralSpell>();
        if (generalSpell != null)
        {
            spawnPoint = generalSpell.spawnPoint;
            string name = spawnPoint.name;
            pointToSpawn = GameObject.Find(name);
        }  
        else
        {
            spawnPoint = GameObject.Find("target0");
        }
    }
}

// Update is called once per frame
void Update()
{

    if(hotbarSparkle != null)
    {
        if(sparkleDirection == 0)
        {
            hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                  new Vector3(transform.position.x + .5f, transform.position.y + 0.5f, -1), .001f);
            if(hotbarSparkle.transform.position == new Vector3(transform.position.x + .5f, transform.position.y + 0.5f, -1))
            {
                sparkleDirection++;
            }
        }
        if (sparkleDirection == 1)
        {
            hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                  new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, -1), .001f);
            if (hotbarSparkle.transform.position == new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, -1))
            {
                sparkleDirection++;
            }
        }
        if (sparkleDirection == 2)
        {
            hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                  new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, -1), .001f);
            if (hotbarSparkle.transform.position == new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, -1))
            {
                sparkleDirection++;
            }
        }
        if (sparkleDirection == 3)
        {
            hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                  new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, -1), .001f);
            if (hotbarSparkle.transform.position == new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, -1))
            {
                sparkleDirection = 0;
            }
        }


    }

}

}
*/