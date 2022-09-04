using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEffect : AllEffectsOnTroop.Effect
{
    bool startedWithCharm = false;
    public FearEffect(DateTime expiration, GameObject troop) : base(expiration, troop, AllEffectsOnTroop.Effect.EffectType.ignore)
    {
        name = "Fear";
    }
    public FearEffect(int seconds, GameObject troop) : base(seconds, troop, AllEffectsOnTroop.Effect.EffectType.ignore)
    {
        name = "Fear";
    }


    public override bool EffectCalledEveryFixedUpdate()
    {

        if (expirationTime < TimeManagement.CurrentTime())
        {
            if (troop == null)
            {
                return true;
            }
            //reverse effect
            GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
            if(contactCollider == null || contactCollider.GetComponent<GeneralMovementNew>() == null)
            {
                return true;
            }
            

            if(startedWithCharm == true)
            {
                return true;
            }




            if (contactCollider.GetComponent<GeneralMovementNew>().direction == GeneralMovementNew.Direction.Left)
            {
                contactCollider.GetComponent<GeneralMovementNew>().direction = GeneralMovementNew.Direction.Right;
            }
            else
            {
                contactCollider.GetComponent<GeneralMovementNew>().direction = GeneralMovementNew.Direction.Left;
            }
            contactCollider.GetComponent<GeneralMovementNew>().speed *= 0.5f;
            Animator animator = troop.GetComponent<Animator>();
            animator.speed *= 0.667f;
            //contactCollider.GetComponent<BlockingNew>().ResetTotalBlockingAmount();
            troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;
            contactCollider.transform.localPosition = new Vector3(0, 0, 0);
            Transform rangedCollider = troop.transform.Find("RangedCollider");
            if (rangedCollider != null)
            {
                rangedCollider.transform.localPosition = new Vector3(0, 0, 0);
            }
            return true;
        }
        return false;
    }

    public override void StartEffect()
    {
        if(troop == null)
        {
            return;
        }
        GameObject contactCollider = troop.transform.Find("ContactCollider").gameObject;
        if(contactCollider == null || contactCollider.GetComponent<GeneralMovementNew>() == null)
        {
            return;
        }


        ListWithExpirations<AllEffectsOnTroop.Effect> effects = contactCollider.GetComponent<AllEffectsOnTroop>().effects;
        for (int i = 0; i < effects.GetList().Count; i++)
        {
            if (effects.GetList()[i] is CharmEffect)
            {
                startedWithCharm = true;
                return;
            }
        }


        if (contactCollider.GetComponent<GeneralMovementNew>().direction == GeneralMovementNew.Direction.Left)
        {
            contactCollider.GetComponent<GeneralMovementNew>().direction = GeneralMovementNew.Direction.Right;
        }
        else
        {
            contactCollider.GetComponent<GeneralMovementNew>().direction = GeneralMovementNew.Direction.Left;
        }
        contactCollider.GetComponent<GeneralMovementNew>().speed *= 2;
        Animator animator = troop.GetComponent<Animator>();
        animator.speed *= 1.5f;
        //contactCollider.GetComponent<BlockingNew>().RemoveBlocking();
        troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;
        contactCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
        Transform rangedCollider = troop.transform.Find("RangedCollider");
        if(rangedCollider != null)
        {
            rangedCollider.transform.position = new Vector3(UnityEngine.Random.Range(10, 1000), UnityEngine.Random.Range(10, 1000), 0);
            rangedCollider.GetComponent<RangedAttackNew>().target = null;
        }
    }
}
