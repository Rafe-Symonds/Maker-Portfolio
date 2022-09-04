using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingNew : MonoBehaviour
{
    
    //This is the thing I am blocking with the amount I am blocking with
    public Dictionary<GameObject, float> enemiesBlockedObjectsWithAmounts = new Dictionary<GameObject, float>();
    public float totalBlockingAmount;       // "Initial Blocking Amount"
    public float CurrentBlockedAmount;     //My amount that is currently blocked
    private float orginalTotalBlockingAmount;


    public bool doesNotBlock;



    //Following functions are for blocking
    public float HowMuchBlockingIHaveLeft()
    {
        return totalBlockingAmount - CurrentBlockedAmount;
    }
    public bool AreWeBlockingAnything()
    {
        //Debug.Log(gameObject + "  enemiesBlockedObjectsWithAmounts + " + enemiesBlockedObjectsWithAmounts.Count);
        return enemiesBlockedObjectsWithAmounts.Count > 0;

    }
    public bool AreWeBlocked()
    {
        if (CurrentBlockedAmount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseCurrentBlockingAmount(float amount)
    {
        GeneralMovementNew generalMovement = gameObject.GetComponent<GeneralMovementNew>();
        CurrentBlockedAmount += amount;
        if (CurrentBlockedAmount > 0)
        {
            if(generalMovement != null)
            {
                generalMovement.moving = false; //Need some code to start moving if blocker dies
            }
        }
    }
    public void DecreaseCurrentBlockingAmount(float amount)
    {
        GeneralMovementNew generalMovement = gameObject.GetComponent<GeneralMovementNew>();
        CurrentBlockedAmount -= amount;
        if (CurrentBlockedAmount == 0)
        {
            if (generalMovement != null)
            {
                generalMovement.moving = true; 
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
    public float GetAmountWeAreBlockingEnemyWith(GameObject enemy)
    {
        try
        {
            float blockingAmount = enemiesBlockedObjectsWithAmounts[enemy];
            if (blockingAmount != null)
            {
                return blockingAmount;
            }
            else
            {
                return 0;
            }
        }
        catch(Exception e)
        {
            return 0;
        }
    }
    public void CapTotalBlockingAmount()
    {
        totalBlockingAmount = 0f;
    }
    public void ResetTotalBlockingAmount()
    {
        totalBlockingAmount = orginalTotalBlockingAmount;
    }
    public void RemoveBlocking()
    {
        
        foreach(KeyValuePair<GameObject, float> keyValuePair in enemiesBlockedObjectsWithAmounts)
        {
            BlockingNew enemyBlockingNew = keyValuePair.Key.GetComponent<BlockingNew>();
            //enemyBlockingNew.RemoveToEnemiesBlockedDictionary(gameObject);
            enemyBlockingNew.DecreaseCurrentBlockingAmount(GetAmountWeAreBlockingEnemyWith(keyValuePair.Key));
            //RemoveToEnemiesBlockedDictionary(keyValuePair.Key);
        }
        enemiesBlockedObjectsWithAmounts.Clear(); //no longer blocking anything. Blocking = 0
        ResetTotalBlockingAmount();
        if(CurrentBlockedAmount - orginalTotalBlockingAmount >= 0)
        {
            DecreaseCurrentBlockingAmount(orginalTotalBlockingAmount);
        }
    }   

    // Start is called before the first frame update
    void Start()
    {
        orginalTotalBlockingAmount = totalBlockingAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
