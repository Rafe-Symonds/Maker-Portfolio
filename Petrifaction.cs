using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrifaction : DamageNew
{
    public int freezeAmount;
    public int freezeChancePercent;


    public class PetrifactionEffect : AllEffectsOnTroop.Effect
    {
        public PetrifactionEffect(DateTime duration, GameObject troop) : base(duration, troop, AllEffectsOnTroop.Effect.EffectType.replace)
        {
            name = "Petrifacation";
        }

        public override bool EffectCalledEveryFixedUpdate()
        {
            if (GetDuration() < TimeManagement.CurrentTime())
            {
                GeneralMovementNew generalMovement = troop.GetComponentInChildren<GeneralMovementNew>();
                generalMovement.frozen = false;
                SpriteRenderer spriteRenderer = troop.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                }   
                    
                troop.GetComponentInChildren<BlockingNew>().ResetTotalBlockingAmount();
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
        if (randomNumber > (100 - freezeChancePercent))
        {
            Debug.Log("Freezing time");
            TimeSpan timeSpan;
            DateTime experationDate;
            timeSpan = new TimeSpan(0, 0, freezeAmount);
            experationDate = TimeManagement.CurrentTime() + timeSpan;

            GeneralMovementNew enemyGeneralMovement = gameobject.GetComponentInChildren<GeneralMovementNew>();
            if (enemyGeneralMovement != null)
            {
                enemyGeneralMovement.frozen = true;
                BlockingNew blockingNew = gameobject.GetComponentInChildren<BlockingNew>();
                blockingNew.RemoveBlocking();
            }
            GeneralMovementNew generalMovement = gameObject.GetComponentInChildren<GeneralMovementNew>();
            if(generalMovement != null)
            {
                generalMovement.moving = true;
            }
            

            SpriteRenderer spriteRenderer = gameobject.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null)
            {
                spriteRenderer.color = new Color(0.29f, 0.26f, 0.25f);
            }
            


            gameobject.GetComponentInChildren<AllEffectsOnTroop>().AddToList(new PetrifactionEffect(experationDate, gameobject));

            gameobject.GetComponentInChildren<BlockingNew>().CapTotalBlockingAmount();
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
