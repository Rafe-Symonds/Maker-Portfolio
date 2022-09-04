using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : DamageNew
{
    public int freezeAmount;
    
    public class FreezeEffect : AllEffectsOnTroop.Effect
    {
        public FreezeEffect(DateTime duration, GameObject troop) : base(duration, troop, AllEffectsOnTroop.Effect.EffectType.replace)
        {
            name = "Paralyze";
        }

        public override bool EffectCalledEveryFixedUpdate()
        {
            if (GetDuration() < TimeManagement.CurrentTime())
            {
                if(troop.GetComponentInChildren<GeneralMovementNew>() == null)
                {
                    return true;
                }
                GeneralMovementNew generalMovement = troop.GetComponentInChildren<GeneralMovementNew>();
                generalMovement.frozen = false;
                troop.GetComponent<BlockingNew>().ResetTotalBlockingAmount();
                return true;
            }
            return false;
        }

        public override void StartEffect()
        {
            
        }
        /*
        public override GameObject GetImage()
        {
           IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
           return iconManager.frozen;
        }
        */
    }




    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber > 80)
        {
            TimeSpan timeSpan;
            DateTime experationDate;
            timeSpan = new TimeSpan(0, 0, freezeAmount);
            experationDate = TimeManagement.CurrentTime() + timeSpan;

            GeneralMovementNew generalMovement = gameobject.GetComponentInChildren<GeneralMovementNew>();
            if(generalMovement == null)
            {
                return;
            }


            generalMovement.frozen = true;
            gameobject.GetComponent<AllEffectsOnTroop>().AddToList(new FreezeEffect(experationDate, gameobject));

            gameobject.GetComponent<BlockingNew>().CapTotalBlockingAmount();
        }
    }
}
