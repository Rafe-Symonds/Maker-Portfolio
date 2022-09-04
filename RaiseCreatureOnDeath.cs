using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseCreatureOnDeath : DamageNew
{
    public GameObject creatureToSummon;
    private GameObject creatureSpawned;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        HealthNew health = gameobject.GetComponentInChildren<HealthNew>();
        if(health.health <= 0)
        {
            if (gameObject.CompareTag("team1"))
            {
                GameObject troop = Instantiate(creatureToSummon, new Vector3(gameobject.transform.position.x - 0.2f,
                   gameObject.transform.position.y + 1, 0), Quaternion.identity);
                creatureSpawned = troop;
                Invoke("MoveSpawnedTroop", 0.05f);
            }
            else
            {
                GameObject troop = Instantiate(creatureToSummon, new Vector3(gameobject.transform.position.x + 0.2f,
                   gameObject.transform.position.y + 1, 0), Quaternion.identity);
                troop.GetComponentInChildren<GeneralMovementNew>().ChangeTeams();
                troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;
                creatureSpawned = troop;
                Invoke("MoveSpawnedTroop", 0.05f);
            }
        }
    }

    //method so the spawned troop is not affected by the structures
    public void MoveSpawnedTroop()
    {
        if(gameObject != null)
        {
            creatureSpawned.transform.position = gameObject.transform.position;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
