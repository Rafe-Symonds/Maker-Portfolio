using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpawnNew : MonoBehaviour
{
    public GameObject prefab;
    public GameObject skillInfoText;
    private ManaBar manabar;
    EmptyBackgroundScript backgroundScript;
    private GameObject emptyBackground;
    private Vector3 firstClick = Vector3.zero;
    private GameObject spell = null;
    //private GameObject hotbarSparkle = null;
    private GameObject hotbarSpinEffect = null;
    private GameObject hotbarSpinEffectPrefab;
    private GameObject hotbarExplodeEffectPrefab;
    private GameObject hotbarGlow = null;
    private GameObject coolDownSquarePrefab = null;
    //private GameObject hotbarSparklePrefab = null;
    private GameObject hotbarGlowContinuesPrefab = null;
    private GameObject wizard = null;

    private int sparkleDirection = 0;



    public void BackgroundClick(Vector3 pos)
    {
        //Debug.Log("BackgroundClick");
        if (prefab != null)
        {
            GeneralSpellNew generalSpell = prefab.GetComponent<GeneralSpellNew>();
            if (manabar.CheckAndReduceMana(generalSpell.manaUsage) == true)
            {
                if (generalSpell.spellType == GeneralSpellNew.SpellType.Moving)
                {
                    //e.g. fireball
                    GameObject pointToSpawn = GameObject.Find("SpellSpawnPoint");
                    if(pointToSpawn != null)
                    {
                        GameObject spell = Instantiate(prefab, pointToSpawn.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
                        spell.GetComponent<Projectile>().MoveProjectile(pos);
                        //Debug.Log(spawnPoint);
                        SpellInProgress();
                    }
                    
                }
                else if (generalSpell.spellType == GeneralSpellNew.SpellType.TwoClicks)
                {
                    if (firstClick == Vector3.zero)
                    {
                        firstClick = pos;
                    }
                    else
                    {
                        GameObject instantiate1 = Instantiate(prefab, firstClick, Quaternion.identity, gameObject.transform);
                        GameObject instantiate2 = Instantiate(prefab, pos, Quaternion.identity, gameObject.transform);
                        if (generalSpell.OffSetCreationPositionOffGround())
                        {
                            instantiate1.transform.position = new Vector3(instantiate1.transform.position.x,
                                            instantiate1.transform.position.y + instantiate1.GetComponent<BoxCollider2D>().bounds.extents.y,
                                            instantiate1.transform.position.z);
                            instantiate2.transform.position = new Vector3(instantiate2.transform.position.x,
                                            instantiate2.transform.position.y + instantiate2.GetComponent<BoxCollider2D>().bounds.extents.y,
                                            instantiate2.transform.position.z);


                        }
                        SpellInProgress();
                        backgroundScript.ClearCallBack();
                    }
                }
                else
                {
                    //Stationary Spawn e.g. Arrow spell
                    GameObject instantiate = Instantiate(prefab, pos, Quaternion.identity, gameObject.transform) as GameObject;
                    SpellInProgress();


                }
                backgroundScript.ClearCallBack();
            }
        }
    }


    public void TroopClick(GameObject troop)
    {

        if (prefab != null)
        {
            Debug.Log("GeneralSpawn troop click");

            GameObject spell = Instantiate(prefab, troop.transform.position, Quaternion.identity, gameObject.transform);
            GeneralSpellNew generalSpell = spell.GetComponent<GeneralSpellNew>();
            generalSpell.SetTroop(troop);
            SpecificSpellNew specificSpell = spell.GetComponent<SpecificSpellNew>();
            specificSpell.spellAffectOnSpecificTroopPolymorphic();

            SpellInProgress();
            backgroundScript.ClearCallBackTroop();
        }


    }



    public void WaitingForTarget()
    {
        hotbarSpinEffect = Instantiate(hotbarSpinEffectPrefab, transform.position, Quaternion.identity, gameObject.transform);
        //hotbarSparkle = Instantiate(hotbarSparklePrefab, new Vector3(transform.position.x, transform.position.y + .5f, -1), Quaternion.identity, gameObject.transform);
        GameObject mouseCursor = GameObject.Find("CastingCursor");
        mouseCursor.GetComponent<MouseCursor>().CastingSpell();
        if (wizard != null)
        {
            wizard.GetComponent<Animator>().SetBool("inMelee", true);
        }
    }
    public void SpellInProgress()
    {
        GameObject mouseCursor = GameObject.Find("CastingCursor");
        mouseCursor.GetComponent<MouseCursor>().StoppedCastingSpell();
        if (wizard != null)
        {
            wizard.GetComponent<Animator>().SetBool("inMelee", false);
        }
        if(hotbarSpinEffect != null)
        {
            hotbarSpinEffect.GetComponent<Animator>().SetBool("explode", true);
        }
        /*
        Debug.Log("Trying to Destroying hotbarsparkle");
        if (hotbarSparkle != null)
        {
            Debug.Log("Destroying hotbarsparkle");
            Destroy(hotbarSparkle);
            hotbarSparkle = null;
            
        }
        else
        {
            if (wizard != null)
            {
                wizard.GetComponent<Animator>().SetBool("inMelee", true);
            }
        }
        */
        hotbarGlow = Instantiate(hotbarGlowContinuesPrefab, transform.position, Quaternion.identity, gameObject.transform);
    }
    public void FinishCastingSpell()
    {
        if (hotbarGlow != null && hotbarGlow.name.Equals("HotbarGlowContinues(Clone)"))
        {
            //Debug.Log(hotbarGlow.name);
            Destroy(hotbarGlow);
            hotbarGlow = null;
        }
        GameObject coolDownSquare = gameObject.transform.Find("CoolDownSquare").gameObject;
        CoolDownScript coolDownScript = coolDownSquare.GetComponent<CoolDownScript>();
        coolDownScript.StartTimer();
    }
    public void CancelSpell()
    {
        if (hotbarSpinEffect != null)
        {
            Transform coolDownSquare = gameObject.transform.Find("CoolDownSquare");
            Transform hotbarGlow = gameObject.transform.Find("HotbarGlowContinues(Clone)");
            if (coolDownSquare != null && backgroundScript.CallBackFunction != null && hotbarGlow == null)
            {
                Destroy(coolDownSquare.gameObject);
            }
            else if(coolDownSquare != null && backgroundScript.CallBackFunctionOnTroop != null && hotbarGlow == null)
            {
                Destroy(coolDownSquare.gameObject);
            }
            Destroy(hotbarSpinEffect);
            hotbarSpinEffect = null;
        }
        if (wizard != null)
        {
            wizard.GetComponent<Animator>().SetBool("inMelee", false);
        }
        GameObject mouseCursor = GameObject.Find("CastingCursor");
        mouseCursor.GetComponent<MouseCursor>().StoppedCastingSpell();
    }

    /*
    public void CreateSparkleEffect()
    {
        //when casting the spell and waiting for where to spawn it
        
        hotbarSparkle = Instantiate(hotbarSparklePrefab, new Vector3(transform.position.x, transform.position.y + .5f, -1), Quaternion.identity, gameObject.transform);

        GameObject wizard = GameObject.Find("Wizard");
        if (wizard != null)
        {
            wizard.GetComponent<Animator>().SetBool("inMelee", true);
        }
    }

    public void ClearHotbarGlow()
    {
        if (hotbarGlow != null)
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
        GameObject wizard = GameObject.Find("Wizard");
        if (wizard != null)
        {
            wizard.GetComponent<Animator>().SetBool("inMelee", false);
        }
        Transform coolDownSquare = gameObject.transform.Find("CoolDownSquare");
        if (coolDownSquare != null && backgroundScript.CallBackFunction != null)
        {
            Destroy(coolDownSquare.gameObject);
        }
    }
    public void ChangeHotbarGlow()
    {
        Debug.Log("Creating Green Hot Bar Glow");
        
        Destroy(hotbarSparkle);
        hotbarSparkle = null;
        GameObject wizard = GameObject.Find("Wizard");
        if (wizard != null)
        {
            wizard.GetComponent<Animator>().SetBool("inMelee", false);
        }
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
    */
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
        if(prefab == null)
        {
            return;
        }
        GeneralSpellNew generalSpell = prefab.GetComponent<GeneralSpellNew>();
        if (generalSpell != null)
        {
            string name = prefab.name;
            //Debug.Log(name);
        }

        Debug.Log("GeneralSpawn clicked");
        if (prefab != null)
        {

            if (generalSpell != null)
            {
                if (generalSpell.spellType == GeneralSpellNew.SpellType.Moving || generalSpell.spellType == GeneralSpellNew.SpellType.Stationary)
                {

                    backgroundScript.SetCallBack(new Action<Vector3>(BackgroundClick), generalSpell.overridePosition, false, gameObject);
                    WaitingForTarget();

                    //This line moves empty background forwards so that it can be clicked on instead on a troop
                    emptyBackground.transform.position = new Vector3(emptyBackground.transform.position.x, emptyBackground.transform.position.y, -4f);
                }
                else if (generalSpell.spellType == GeneralSpellNew.SpellType.TwoClicks)
                {
                    backgroundScript.SetCallBack(new Action<Vector3>(BackgroundClick), generalSpell.overridePosition, true, gameObject);
                    WaitingForTarget();
                }
                else if (generalSpell.spellType == GeneralSpellNew.SpellType.Summoner)
                {

                    if (GameObject.FindGameObjectWithTag("team1Summoner") == null)
                    {
                        if (manabar.CheckAndReduceMana(generalSpell.manaUsage) == true)
                        {
                            GameObject spawnPoint = GameObject.Find("SummonerSpawnPoint");
                            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, gameObject.transform);
                            WaitingForTarget();
                            //UnityEngine.Debug.Log("summoner");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (generalSpell.spellType == GeneralSpellNew.SpellType.FullScreen)
                {
                    if (manabar.CheckAndReduceMana(generalSpell.manaUsage))
                    {
                        if (generalSpell.overridePosition == EmptyBackgroundScript.OverridePosition.Top)
                        {
                            Instantiate(prefab, new Vector3(0, 2.5f, 0), Quaternion.identity, gameObject.transform);
                        }
                        else
                        {
                            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
                        }

                        SpellInProgress();
                    }
                    else
                    {
                        return;
                    }
                }
                else if (generalSpell.spellType == GeneralSpellNew.SpellType.TroopSpell)
                {
                    Debug.Log("SetTroopCallBack");
                    backgroundScript.SetCallBackTroop(new Action<GameObject>(TroopClick), gameObject);
                    WaitingForTarget();

                }
                CreateCoolDown(prefab.GetComponent<GeneralSpellNew>().coolDownInSeconds);
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
            */

            //
            //
            //   Troop Spawn Below
            //
            //
            //


            Transform contactCollider = prefab.transform.Find("ContactCollider");
            if (contactCollider != null)
            {
                GeneralMovementNew generalMovementNew = contactCollider.GetComponent<GeneralMovementNew>();

                //Money
                GeneralMoney money = GameObject.Find("Money").GetComponent<GeneralMoney>();
                int moneyCost = prefab.transform.Find("ContactCollider").GetComponent<GeneralMovementNew>().moneyCost;
                if (money.CheckAndReduceMoney(moneyCost))
                {
                    Vector3 spawnPoint = GameObject.Find("team1SpawnPoint").transform.position;
                    if(generalMovementNew.spawnLocation == GeneralMovementNew.SpawnLocation.Random)
                    {
                        float xCord = UnityEngine.Random.Range(-90, 0) / 10;
                        float yCord = UnityEngine.Random.Range(5, 40) / 10;
                        spawnPoint = new Vector3(xCord, yCord, 0);
                    }


                    
                    
                    GameObject creatureSpawned = Instantiate(prefab, spawnPoint, Quaternion.identity);
                    Instantiate(hotbarExplodeEffectPrefab, transform.position, Quaternion.identity, gameObject.transform);
                    creatureSpawned.transform.parent = gameObject.transform;

                    // Code below is for adjusting spwan point for how big the groundCollider is so the creature does not spawn in the gorund
                    GameObject groundCollider = creatureSpawned.transform.Find("GroundCollider").gameObject;
                    spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y +
                                    groundCollider.GetComponent<Collider2D>().bounds.size.y, spawnPoint.z);
                    
                    creatureSpawned.transform.position = spawnPoint;

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

    void OnMouseOver()
    {
        skillInfoText.SetActive(true);
    }
    void OnMouseExit()
    {
        skillInfoText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        coolDownSquarePrefab = Resources.Load("Prefabs/General/CoolDownSquare") as GameObject;
        manabar = GameObject.Find("ManaBarNew").GetComponent<ManaBar>();
        backgroundScript = GameObject.FindObjectOfType(typeof(EmptyBackgroundScript)) as EmptyBackgroundScript;
        emptyBackground = GameObject.Find("EmptyBackground");
        //hotbarSparklePrefab = Resources.Load("Prefabs/General/CoolDownEffect") as GameObject;
        hotbarSpinEffectPrefab = Resources.Load("Prefabs/General/HotBarSpin") as GameObject;
        hotbarExplodeEffectPrefab = Resources.Load("Prefabs/General/HotBarExplode") as GameObject;
        hotbarGlowContinuesPrefab = Resources.Load("Prefabs/General/HotbarGlowContinues") as GameObject;
        wizard = GameObject.Find("Wizard");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (hotbarSparkle != null)
        {
            if (sparkleDirection == 0)
            {
                hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                      new Vector3(transform.position.x + .5f, transform.position.y + 0.5f, -1), .02f);
                if (hotbarSparkle.transform.position == new Vector3(transform.position.x + .5f, transform.position.y + 0.5f, -1))
                {
                    sparkleDirection++;
                }
            }
            if (sparkleDirection == 1)
            {
                hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                      new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, -1), .02f);
                if (hotbarSparkle.transform.position == new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, -1))
                {
                    sparkleDirection++;
                }
            }
            if (sparkleDirection == 2)
            {
                hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                      new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, -1), .02f);
                if (hotbarSparkle.transform.position == new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, -1))
                {
                    sparkleDirection++;
                }
            }
            if (sparkleDirection == 3)
            {
                hotbarSparkle.transform.position = Vector3.MoveTowards(hotbarSparkle.transform.position,
                                                      new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, -1), .02f);
                if (hotbarSparkle.transform.position == new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, -1))
                {
                    sparkleDirection = 0;
                }
            }


        }
        */
    }
}
