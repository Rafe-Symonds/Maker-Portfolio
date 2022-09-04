using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLifeCycle : LifeCycle
{
    GeneralMovement.MovementMultiplier movementMultiplier = new GeneralMovement.MovementMultiplier(0.5f, 10000);

    public override void Touch(GameObject other)
    {
        GeneralMovement generalMovement = other.GetComponent<GeneralMovement>();
        LifeCycle lifeCycle = other.GetComponent<LifeCycle>();
        if (generalMovement != null && lifeCycle != null && !lifeCycle.CheckHasProperty(Properties.WildernessWalk))
        {
            generalMovement.AddAdjustment(movementMultiplier);
        }
    }
    public override void Leave(GameObject other)
    {
        GeneralMovement generalMovement = other.GetComponent<GeneralMovement>();
        LifeCycle lifeCycle = other.GetComponent<LifeCycle>();
        if (generalMovement != null && lifeCycle != null && !lifeCycle.CheckHasProperty(Properties.WildernessWalk))
        {
            generalMovement.RemoveAdjustment(movementMultiplier);


        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
