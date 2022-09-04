using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEffectsOnTroop : MonoBehaviour
{
    public ListWithExpirations<Effect> effects = new ListWithExpirations<Effect>();

    public abstract class Effect : BaseEffect
    {
        //return if finished
        public enum EffectType {replace, duplicate, ignore};
        public abstract bool EffectCalledEveryFixedUpdate();
        public abstract void StartEffect();
        public bool done = false;
        protected GameObject troop;
        public EffectType effectType;

        public Effect(int seconds, GameObject troop, EffectType effectType)
        {
            this.effectType = effectType;
            expirationTime = TimeManagement.CurrentTime() + new TimeSpan(0,0, seconds);
            this.troop = troop;
        }
        public Effect(DateTime expiration, GameObject troop, EffectType effectType)
        {
            this.effectType = effectType;
            expirationTime = expiration;
            this.troop = troop;
        }
        public Effect(GameObject troop, EffectType effectType)
        {
            this.effectType = effectType;
            this.troop = troop;
        }
    }

    public void AddToList(Effect effect)
    {
        //gameobject.GetComponent<AllEffectsOnTroop>().RemoveEffectType(typeof(FreezeEffect));
        if(effect.effectType == Effect.EffectType.replace)
        {
            RemoveEffectType(effect.GetType());
        }
        if(effect.effectType == Effect.EffectType.ignore)
        {
            foreach(Effect existingEffect in effects.GetList())
            {
                if (effect.GetType().Equals(existingEffect.GetType()))
                {
                    return;
                }
            }
        }
        effects.AddEffect(effect);
        effect.StartEffect();
        GameObject iconController = gameObject.transform.parent.Find("IconController").gameObject;
        iconController.GetComponent<IconControllerScript>().ChangeEffect();
        //
        //TO DO: Check for existing effects for same type
        //
    }


    public void RemoveEffectType(Type effectType)
    {
        ListWithExpirations<Effect> effectsToDelete = new ListWithExpirations<Effect>();
        
        foreach (Effect effect in effects.GetList())
        {
            if(effectType.Equals(effect.GetType()))
            {
                effectsToDelete.AddEffect(effect);
            }
        }
        foreach (Effect effect in effectsToDelete.GetList())
        {
            effects.GetList().Remove(effect);
        }
    }

    public void CleanUp()
    {
        // Do nothing to remove base class clean up as update will do the clean up
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ListWithExpirations<Effect> effectsToDelete = new ListWithExpirations<Effect>();
        foreach(Effect effect in effects.GetList())
        {
            if (effect.EffectCalledEveryFixedUpdate() == true)
            {
                effectsToDelete.AddEffect(effect);
            }
        }
        foreach (Effect effect in effectsToDelete.GetList())
        {
            effects.GetList().Remove(effect);
            //Debug.Log("destroying effects");
        }
        if(effectsToDelete.GetList().Count >= 1)
        {
            gameObject.transform.parent.Find("IconController").GetComponent<IconControllerScript>().ChangeEffect();
            //Debug.Log("readjusting the current icons");
        }
            
    }
}
