
//public class GeneralSpell : MonoBehaviour
//{ }
/*
    public int manaUsage;
    public GameObject spawnPoint;
    public enum SpellType { Stationary, Moving, Summoner, FullScreen, TwoClicks, TroopSpell };
    public SpellType spellType;
    public EmptyBackgroundScript.OverridePosition overridePosition;
    public bool overTimeAffects;
    public int manaOverTime;
    public int duration;
    
    private Action finishedCallBack;
    private TimeSpan timeBetweenManaReduction = new TimeSpan(0, 0, 1);
    private DateTime nextManaReduction = DateTime.Now;
    
    
    public int timeIntervalInMS = 1;
    private TimeSpan timeSpanInMS;
    private DateTime nextTime = DateTime.Now;
    
    
    public SpecificSpellNew specialSpellScript;

    public bool offSetPositionOnCreation;
    public bool oneAtATime;




    private GameObject troop = null;


    public GameObject onSpawnEffectPrefab;


    private bool done = false;
    public int coolDownInSeconds;

    public void spellAffect(GameObject gameobject)
    {
        specialSpellScript.spellAffectPolymorphic(gameobject);
        nextTime = DateTime.Now + timeSpanInMS;
    }
    public void spellAffectOverTime(GameObject gameobject)
    {
        if(nextTime < DateTime.Now && overTimeAffects)
        {
            specialSpellScript.spellAffectOverTimePolymorphic(gameobject);


            nextTime = DateTime.Now + timeSpanInMS;
        }
    }
    public void spellAffectOverTimeEnd(GameObject gameobject)
    {
        //Debug.Log("General Spell SpellAffectOverTimeEnd");
        specialSpellScript.spellAffectOverTimeEndPolymorphic(gameobject);

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
        //Debug.Log("General Spell Destroy 1");
        if (done == false)
        {
            //Debug.Log("General Spell Destroy 2");
            done = true;
            if(gameObject != null && gameObject.transform != null)
            {
                //Debug.Log("General Spell Destroy 3");
                Transform parent = gameObject.transform.parent;
                if(parent != null)
                {
                    GeneralSpawn generalSpawn = parent.GetComponent<GeneralSpawn>();
                    if(generalSpawn != null)
                    {
                        generalSpawn.ClearGreenHotbarGlow();
                    }
                    //Debug.Log("General Spell Destroy 4");
                    Transform coolDownSquare = parent.transform.Find("CoolDownSquare");
                    if(coolDownSquare != null)
                    {
                        //Debug.Log("General Spell Destroy 5");
                        CoolDownScript coolDownScript = coolDownSquare.GetComponent<CoolDownScript>();
                        if(coolDownScript != null)
                        {
                            //Debug.Log("General Spell Destroy 6");
                            coolDownScript.StartTimer();
                        }
                    }
                }
                
            }
            
        }  
    }
    

    //private DateTime timeToEnd = new DateTime();
    // Start is called before the first frame update
    void Awake()
    {
        timeSpanInMS = new TimeSpan(0, 0, timeIntervalInMS);
        if(onSpawnEffectPrefab != null)
        {
            Debug.Log("onSpawnEffectPrefab");
            Instantiate(onSpawnEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(manaOverTime > 0)
        {
            if (nextManaReduction < DateTime.Now)
            {
                ManaBar manaBar = GameObject.Find("Manabar").GetComponent<ManaBar>();
                if (manaBar.CheckAndReduceMana(manaOverTime) == false)
                {
                    Destroy(gameObject);
                }
                else 
                {
                    nextManaReduction = DateTime.Now + timeBetweenManaReduction;
                }
                
            }

        }
        
    }
}
*/