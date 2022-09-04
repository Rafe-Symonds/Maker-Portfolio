using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : MonoBehaviour
{
    //public GameObject ghost;
    public DateTime nextGhost;
    GeneralMovementNew generalMovement;
    public void RaiseCrature()
    {
        Animator animator = gameObject.GetComponentInParent<Animator>();
        animator.SetBool("special", true);

        //animator.SetBool("moving", false);
        
        //ghost is spawned in the statemachine script which is found on the special block in the animator.
        
    }




    // Start is called before the first frame update
    void Start()
    {
        generalMovement = gameObject.GetComponent<GeneralMovementNew>();
        nextGhost = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 15);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (nextGhost < TimeManagement.CurrentTime() && generalMovement.moving == true)
        {
            nextGhost = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 10);
            RaiseCrature();
        }
    }
}
