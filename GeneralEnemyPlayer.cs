using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyPlayer : MonoBehaviour
{
    public TimeSpan timeBetweenSpawns;
    private DateTime nextSpawn = TimeManagement.CurrentTime();

    public float timeDelayFixedSpell;
    public int minSecondsBetweenSpawns;
    public int maxSecondsBetweenSpawns;
    public int secondsBetweenSpells;
    public TimeSpan timeBetweenSpells;
    private DateTime nextSpell = TimeManagement.CurrentTime();

    public int secondsFixedTimeBetweenSpells;
    private TimeSpan fixedTimeBetweenSpells;
    private DateTime fixedNextSpell = TimeManagement.CurrentTime();
    public GameObject fixedTarget = null;

    public GameObject spawnPoint;
    public GameObject troopSpawnPoint;
    public List<GameObject> troops = new List<GameObject>();
    public List<int> troopPercentages = new List<int>();
    public List<GameObject> spells = new List<GameObject>();
    public List<GameObject> fixedSpells = new List<GameObject>();
    public List<int> fixedSpellsPercentages = new List<int>();

    private GameObject boss;


    private List<GameObject> enemyParentTroops = new List<GameObject>();


    public AudioSource spellSound;


    //private GameObject enemyWizard;


    public bool attack;

    public GameObject specialTarget = null;
    //int count = 2;
    //int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        fixedTimeBetweenSpells = new TimeSpan(0, 0, secondsFixedTimeBetweenSpells);
        //enemyWizard = gameObject.transform.Find("EnemyWizard").gameObject;
        //timeBetweenSpawns = new TimeSpan(0, 0, secondsBetweenSpawns);
        nextSpawn = TimeManagement.CurrentTime() + timeBetweenSpawns;
        timeBetweenSpells = new TimeSpan(0, 0, secondsBetweenSpells);
        nextSpell = TimeManagement.CurrentTime() + timeBetweenSpells;
        boss = gameObject.transform.Find("EnemyBoss").transform.GetChild(0).gameObject;
        if(troopSpawnPoint == null)
        {
            troopSpawnPoint = spawnPoint;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (attack == true)
        {
            
            if (nextSpawn < TimeManagement.CurrentTime())
            {
                int randomNumber = UnityEngine.Random.Range(0, 100);
                //Debug.Log("Random number " + randomNumber);
                int sum = 0;
                int troopIndex = 0;
                for (int i = 0; i < troopPercentages.Count; i++)
                {
                    sum += troopPercentages[i];
                    if (sum >= randomNumber)
                    {
                        troopIndex = i;
                        break;
                    }
                }
                GameObject newTroop = Instantiate(troops[troopIndex], troopSpawnPoint.transform.position, Quaternion.identity);
                newTroop.GetComponent<SpriteRenderer>().flipX = !newTroop.GetComponent<SpriteRenderer>().flipX;
                GameObject contactCollider = newTroop.transform.Find("ContactCollider").gameObject;
                //GameObject groundCollider = newTroop.transform.Find("GroundCollider").gameObject;
                contactCollider.GetComponent<GeneralMovementNew>().ChangeTeams();
                float gameDifficultyMultiplier = 1 - 0.2f * (2 - SaveAndLoad.Get().GetGameDifficulty());
                contactCollider.GetComponent<HealthNew>().health = (int)(contactCollider.GetComponent<HealthNew>().health * gameDifficultyMultiplier);
                contactCollider.GetComponent<DamageNew>().damageAmount = (int)(contactCollider.GetComponent<DamageNew>().damageAmount * gameDifficultyMultiplier);
                //newTroop.tag = "team2";
                //contactCollider.tag = "team2";
                //groundCollider.tag = "team2";
                nextSpawn = TimeManagement.CurrentTime() + new TimeSpan(0,0, UnityEngine.Random.Range(minSecondsBetweenSpawns, maxSecondsBetweenSpawns));

                if(spells.Count > 0)
                {
                    enemyParentTroops.Add(newTroop);
                }

                //num--;
            }

            if (fixedNextSpell < TimeManagement.CurrentTime() && fixedTarget != null)
            {
                GameObject enemyBase = GameObject.Find("Base2");
                if (fixedTarget.tag.Equals("team1"))
                {
                    Animator animator = boss.GetComponent<Animator>();
                    animator.SetBool("inMelee", true);
                    animator.SetBool("idle", false);
                    Invoke("SpawnRandomFixedAttack", timeDelayFixedSpell);
                    //SpawnRandomFixedAttack(fixedTarget);

                }
                fixedNextSpell = TimeManagement.CurrentTime() + fixedTimeBetweenSpells;
            }

            if(nextSpell < TimeManagement.CurrentTime() && spells.Count > 0)
            {
                SpawnRandomSpell();
                nextSpell = TimeManagement.CurrentTime() + timeBetweenSpells;
            }
        }
        if(specialTarget != null && specialTarget != fixedTarget)
        {
            fixedTarget = specialTarget;
            if(boss != null)
            {
                Animator animator = boss.GetComponent<Animator>();
                animator.SetBool("inMelee", true);
                animator.SetBool("idle", false);
            }
            
        }
        /*
        else
        {
            if (boss != null)
            {
                Animator animator = boss.GetComponent<Animator>();
                animator.SetBool("inMelee", false);
                animator.SetBool("idle", true);
            }  
        }
        */
    }
    public void SpawnRandomFixedAttack()
    {
        //Debug.Log("Creating enemy spell");
        
        int num = UnityEngine.Random.Range(0, 100);
        //Debug.Log("**** Random number " + num);
        int sum = 0;
        int spellIndex = 0;
        if(fixedSpellsPercentages[0] < 0)
        {
            return;
        }
        for(int i = 0; i < fixedSpellsPercentages.Count; i++)
        {
            sum += fixedSpellsPercentages[i];
            if(sum >= num)
            {
                spellIndex = i;
                break;
            }
        }
        GameObject spell = Instantiate(fixedSpells[spellIndex], spawnPoint.transform.position, Quaternion.identity);
        spellSound.PlayOneShot(spellSound.clip);
        spell.tag = "team2";

        if (spell.GetComponent<GeneralEnemySpell>() != null)
        {
            spell.tag = "treasureChest";
        }

        if (spell.GetComponent<Projectile>() != null)
        {
            spell.GetComponent<Projectile>().MoveProjectile(fixedTarget);
        }
        else if (spell.GetComponent<GeneralSpellNew>() == null)
        {
            spell.transform.position = fixedTarget.transform.position;
        }

        if (spell.GetComponent<GeneralSpellNew>() != null && spell.GetComponent<GeneralSpellNew>().spellType == GeneralSpellNew.SpellType.Stationary)
        {
            spell.transform.position = fixedTarget.transform.position;
        }
        if (spell.GetComponent<GeneralSpellNew>() != null && spell.GetComponent<GeneralSpellNew>().overridePosition == EmptyBackgroundScript.OverridePosition.Top)
        {
            spell.transform.position = new Vector3(spell.transform.position.x, 6, 0);
        }
        if(spell.GetComponent<GeneralSpellNew>() != null && spell.GetComponent<GeneralSpellNew>().overridePosition == EmptyBackgroundScript.OverridePosition.Ground)
        {
            spell.transform.position = EmptyBackgroundScript.RayCastToGround(spell.transform.position);
        }

    }
    public void SpawnRandomSpell()
    {
        for(int i = 0; i < enemyParentTroops.Count; i++) //assume first non-null enemy is the first troop
        {
            if(enemyParentTroops[i] == null)
            {
                enemyParentTroops.RemoveAt(i);
                i--;
                Debug.Log("Troop is null so checking next one for spell spawn");
            }
            else
            {
                GameObject spell = Instantiate(spells[0], enemyParentTroops[i].transform.position, Quaternion.identity);
                spell.tag = "team2";
                return;
            }
        }
    }
}


