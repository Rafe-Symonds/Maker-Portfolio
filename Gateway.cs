using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway : SpecificSpellNew
{
    public override void spellAffectPolymorphic(GameObject gameobject)
    {

    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        GeneralMovement generalMovement = gameobject.GetComponent<GeneralMovement>();
        
        Gateway[] gateways = GameObject.FindObjectsOfType<Gateway>();
        if(gateways.Length != 2)
        {
            Debug.Log("not 2 Gateways");
            return;
        }

        Gateway[] newGateways = new Gateway[2];
        if(gateways[0].transform.position.x < gateways[1].transform.position.x)
        {
            newGateways = gateways;
        }
        else
        {
            //order the array so that creatures only go one direction 
            newGateways[0] = gateways[1];
            newGateways[1] = gateways[0];
        }
        


        for(int i = 0; i <= 1; i++)
        {
            Gateway gateway = newGateways[i];
            if(gateway.transform.position != transform.position)
            {
                if (generalMovement.direction == GeneralMovement.Direction.Left)
                {
                    if(i != 0)
                    {
                        //Gates are in order the other gate should be the first element
                        return;
                    }

                }
                if (generalMovement.direction == GeneralMovement.Direction.Right)
                {
                    if( i != 1)
                    {
                        return;
                    }

                }
                ManaBar manaBar = GameObject.Find("ManaBarNew").GetComponent<ManaBar>();
                int size = (int)generalMovement.size;
                float distance = Math.Abs(newGateways[0].transform.position.x - newGateways[1].transform.position.x);
                Debug.Log(size);
                Debug.Log(distance);



                if(manaBar.CheckAndReduceMana(size * (int)distance) == true)
                {
                    gameobject.transform.position = gateway.transform.position;
                    
                }
                else
                {
                    Debug.Log("destroy Gateways");
                    Destroy(gateways[0].gameObject);
                    Destroy(gateways[1].gameObject);
                    
                }

                break;

            }

        }
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        
    }

    public override void onSpellCleanUpPolymorphic()
    {
        base.onSpellCleanUpPolymorphic();
    }




    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        
    }

    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        throw new NotImplementedException();
    }

    public override void specialDamageEffectOnDeath(GameObject gameobject)
    {
        throw new NotImplementedException();
    }

    public override void spellAffectOverTimeNotNeedingTrigger()
    {
        throw new NotImplementedException();
    }
}
