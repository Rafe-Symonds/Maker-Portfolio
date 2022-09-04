using System;
using UnityEngine;

public class GrowSpell : SpecificSpellNew
{
    public class GrowEffect : AllEffectsOnTroop.Effect
    {
        public GrowEffect(DateTime expiration, GameObject troop) : base(expiration, troop, AllEffectsOnTroop.Effect.EffectType.replace)
        {
            name = "Grow";
        }

        public override bool EffectCalledEveryFixedUpdate()
        {
            if(GetDuration() < TimeManagement.CurrentTime())
            {
                Debug.Log("Shrink");
                Vector3 scale = troop.transform.localScale;
                troop.transform.localScale = scale / 2;
                //Debug.Log("Vertical OffSet position before " + troop.transform.Find("IconController").
                                //GetComponent<IconControllerScript>().verticalOffSetPosition);
                

                troop.transform.Find("IconController").GetComponent<IconControllerScript>().verticalOffSetPosition = .2f;

                //Debug.Log("Vertical OffSet position after " + troop.transform.Find("IconController").
                                //GetComponent<IconControllerScript>().verticalOffSetPosition);


                troop.transform.Find("IconController").GetComponent<IconControllerScript>().DisplayEffects();
                return true;
            }
            return false;

        }

        public override void StartEffect()
        {


            Vector3 scale = troop.transform.localScale;

            troop.transform.localScale = new Vector3(scale.x * 2, scale.y * 2, 0);
            DamageNew damage = troop.transform.Find("ContactCollider").GetComponent<DamageNew>();

            int durationForDamageEffect = (int)((expirationTime.Ticks - TimeManagement.CurrentTime().Ticks)/10_000_000);
            damage.AddDamageEffect(new DamageNew.DamageEffect(2, durationForDamageEffect));
            troop.transform.Find("IconController").GetComponent<IconControllerScript>().verticalOffSetPosition = .7f;
            troop.transform.Find("IconController").GetComponent<IconControllerScript>().DisplayEffects();
        }

        /*
        public override GameObject GetImage()
        {
            IconManager iconManager = GameObject.Find("EmptyBackground").GetComponent<IconManager>();
            return iconManager.growth;
        }
        */
    }
    private GameObject troop;
    public override void onSpellCleanUpPolymorphic()
    {
        base.onSpellCleanUpPolymorphic();
    }

    public override void spellAffectOnSpecificTroopPolymorphic()
    {
        //Debug.Log("grow");
        GeneralSpellNew generalSpell = gameObject.GetComponent<GeneralSpellNew>();
        troop = generalSpell.Gettroop();
        

        //Debug.Log(troop);
        troop.transform.Find("ContactCollider").GetComponent<AllEffectsOnTroop>().AddToList(new GrowEffect(expiration, troop));
        
        Destroy(gameObject);
    }

    public override void spellAffectOverTimeEndPolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectOverTimePolymorphic(GameObject gameobject)
    {
        
    }

    public override void spellAffectPolymorphic(GameObject gameobject)
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
