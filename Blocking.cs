using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    
    //This is the thing I am blocking with the amount I am blocking with
    private Dictionary<GameObject, float> enemiesBlockedObjectsWithAmounts = new Dictionary<GameObject, float>();
    public float totalBlockingAmount;
    private float CurrentBlockedAmount;
    public bool doesNotBlock;



    //Following functions are for blocking
    public float GetOutStandingBlockingAmount()
    {
        return totalBlockingAmount - CurrentBlockedAmount;
    }

    public void IncreaseCurrentBlockingAmount(float amount)
    {
        GeneralMovement generalMovement = gameObject.GetComponent<GeneralMovement>();
        CurrentBlockedAmount += amount;
        if (CurrentBlockedAmount > totalBlockingAmount)
        {
            if(generalMovement != null)
            {
                generalMovement.moving = false; //Need some code to start moving if blocker dies
            }
        }
    }
    public float TotalValueBeingUsedForBlocking()
    {
        float total = 0f;
        foreach (KeyValuePair<GameObject, float> item in enemiesBlockedObjectsWithAmounts)
        {
            total += item.Value;
        }
        return total;
    }
    public void AddToEnemiesBlockedDictionary(GameObject enemy, float amount)
    {
        if (enemiesBlockedObjectsWithAmounts.ContainsKey(enemy))
        {
            float currentAmount = enemiesBlockedObjectsWithAmounts[enemy];
            enemiesBlockedObjectsWithAmounts[enemy] = currentAmount + amount;

        }
        else
        {
            enemiesBlockedObjectsWithAmounts[enemy] = amount;
        }
    }
    public void RemoveToEnemiesBlockedDictionary(GameObject enemy)
    {
        enemiesBlockedObjectsWithAmounts.Remove(enemy);
    }

    public bool AreWeBlockingAnything()
    {
        //Debug.Log(gameObject + "  enemiesBlockedObjectsWithAmounts + " + enemiesBlockedObjectsWithAmounts.Count);
        return enemiesBlockedObjectsWithAmounts.Count > 0;

    }


    public bool AreWeBlocked()
    {
        if (CurrentBlockedAmount >= totalBlockingAmount)
        {
            return true;
        }
        else
        {
            return false;
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
