using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmEffect : AllEffectsOnTroop.Effect
{
    //private bool needToMoveTriggerBack;
    //private int triggerTimer = 0;
    public GameObject charmSupportingGameObject;
    bool startedWithFear = false;
    public CharmEffect(DateTime expiration, GameObject troop) : base(expiration, troop, AllEffectsOnTroop.Effect.EffectType.ignore)
    {
        name = "Charm";
    }
    public CharmEffect(int seconds, GameObject troop) : base(seconds, troop, AllEffectsOnTroop.Effect.EffectType.ignore)
    {
        name = "Charm";
    }
    public override bool EffectCalledEveryFixedUpdate()
    {
        if (expirationTime < TimeManagement.CurrentTime())
        {
            if(troop == null)
            {
                return true;
            }
            GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
            if (contactCollider == null)
            {
                return true;
            }

            if(startedWithFear == true)
            {
                return true;
            }

            troop.GetComponentInChildren<BlockingNew>().RemoveBlocking();
            troop.GetComponentInChildren<BlockingNew>().ResetTotalBlockingAmount();

            contactCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
            troop.GetComponentInChildren<GeneralMovementNew>().ChangeTeams();
            troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;

            troop.GetComponent<Animator>().SetBool("inMelee", false);

            Transform rangedCollider = troop.transform.Find("RangedCollider");
            if (rangedCollider != null)
            {
                rangedCollider.GetComponent<RangedAttackNew>().target = null;
                rangedCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
                troop.GetComponent<Animator>().SetBool("inRanged", false);
                troop.GetComponent<Animator>().SetBool("moving", true);
                troop.GetComponentInChildren<GeneralMovementNew>().moving = true;
            }


            charmSupportingGameObject.GetComponent<CharmSupportingScript>().StartToMoveCollidersBack(troop);

            return true;
        }
        return false;

        //old charm code
        /*
         //This code is used to set the position of the contactCollider back to the initial position after moving it so that blocking is not
            //messed up

            GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
            Transform rangedCollider = troop.transform.Find("RangedCollider");
            if (needToMoveTriggerBack == true && triggerTimer == 0)
            {
                //Debug.Log("Moving collider back to normal spot");
                troop.GetComponentInChildren<GeneralMovementNew>().ChangeTeams();
                //contactCollider.transform.localPosition = new Vector3(0, 0, 0);
                
                if (rangedCollider != null)
                {
                    rangedCollider.GetComponent<RangedAttackNew>().target = null;
                    //rangedCollider.transform.localPosition = new Vector3(0, 0, 0);
                }
                return true;
            }
            else
            {
                //Debug.Log("Moving collider to random spot");
                contactCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);

                troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;
                if (rangedCollider != null)
                {
                    rangedCollider.GetComponent<RangedAttackNew>().target = null;
                    rangedCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
                }
                //troop.GetComponentInChildren<BlockingNew>().RemoveBlocking();
                //troop.GetComponentInChildren<BlockingNew>().ResetTotalBlockingAmount();
                needToMoveTriggerBack = true;
                triggerTimer = 3;
            }
            triggerTimer--;
        }
        else if(needToMoveTriggerBack == true)
        {
            if(triggerTimer == 0)
            {
                GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
                //contactCollider.transform.localPosition = new Vector3(0, 0, 0);
                needToMoveTriggerBack = false;
                Transform rangedCollider = troop.transform.Find("RangedCollider");
                if (rangedCollider != null)
                {
                    rangedCollider.GetComponent<RangedAttackNew>().target = null;
                    //rangedCollider.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            triggerTimer--;
         
         
         
         */
    }

    public override void StartEffect()
    {
        if(troop == null)
        {
            return;
        }
        GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
        if (contactCollider == null)
        {
            return;
        }

        ListWithExpirations<AllEffectsOnTroop.Effect> effects = contactCollider.GetComponent<AllEffectsOnTroop>().effects;
        for(int i = 0; i < effects.GetList().Count; i++)
        {
            if (effects.GetList()[i] is FearEffect)
            {
                startedWithFear = true;
                return;
            }
        }
        
            
        

        troop.GetComponentInChildren<BlockingNew>().RemoveBlocking();
        troop.GetComponentInChildren<BlockingNew>().ResetTotalBlockingAmount();

        contactCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
        troop.GetComponentInChildren<GeneralMovementNew>().ChangeTeams();
        troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;

        troop.GetComponent<Animator>().SetBool("inMelee", false);

        Transform rangedCollider = troop.transform.Find("RangedCollider");
        if (rangedCollider != null)
        {
            rangedCollider.GetComponent<RangedAttackNew>().target = null;
            rangedCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
            troop.GetComponent<Animator>().SetBool("inRanged", false);
            troop.GetComponent<Animator>().SetBool("moving", true);
            troop.GetComponentInChildren<GeneralMovementNew>().moving = true;
        }

        //using a gameobject because you can't make a MonoBehaviour using  "= new Script()"
        charmSupportingGameObject = Resources.Load("Prefabs/General/CharmSupportingGameObject") as GameObject;
        charmSupportingGameObject.GetComponent<CharmSupportingScript>().StartToMoveCollidersBack(troop);


        //old charm code
        /*
        GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
        contactCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
        troop.GetComponentInChildren<GeneralMovementNew>().ChangeTeams();
        needToMoveTriggerBack = true;
        triggerTimer = 3;
        //troop.GetComponentInChildren<BlockingNew>().RemoveBlocking();
        //troop.GetComponentInChildren<BlockingNew>().ResetTotalBlockingAmount();
        troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;
        troop.GetComponent<Animator>().SetBool("inMelee", false);
        Transform rangedCollider = troop.transform.Find("RangedCollider");
        if (rangedCollider != null)
        {
            rangedCollider.GetComponent<RangedAttackNew>().target = null;
            rangedCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
        }
         */
    }
    /*
    public void MoveCollidersBack()
    {
        GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
        contactCollider.transform.localPosition = new Vector3(0, 0, 0);
        needToMoveTriggerBack = false;
        Transform rangedCollider = troop.transform.Find("RangedCollider");
        if (rangedCollider != null)
        {
            rangedCollider.GetComponent<RangedAttackNew>().target = null;
            rangedCollider.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    */
}
