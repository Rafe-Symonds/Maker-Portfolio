using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHeal : AllEffectsOnTroop.Effect
{
    private int healAmount;
    DateTime endEffect;
    public InstantHeal(GameObject troop, int healAmount) : base(2,troop, AllEffectsOnTroop.Effect.EffectType.duplicate)
    {
        name = "InstantHeal";
        this.healAmount = healAmount;
        endEffect = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 3);
    }


    public override bool EffectCalledEveryFixedUpdate()
    {
        if(endEffect < TimeManagement.CurrentTime())
        {
            return true;
        }
        return false;
    }

    public override void StartEffect()
    {
        HealthNew healthNew = troop.transform.Find("ContactCollider").GetComponent<HealthNew>();
        healthNew.Heal(healAmount);
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
