using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{ }
/*
    GeneralMovement generalMovement;
    TriggersAndColliders triggersAndColliders;
    public GameObject projectile;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base"))
        { 
            generalMovement.moving = false;
            triggersAndColliders.RangedAttack(collider);

        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base"))
        {
            generalMovement.moving = false;
            triggersAndColliders.RangedAttack(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base"))
        {
            generalMovement.moving = true;
            Debug.Log(gameObject + "   RangedAttack  Moving = true");
        }
    }









    // Start is called before the first frame update
    void Start()
    {
        generalMovement = transform.parent.GetComponent<GeneralMovement>();
        triggersAndColliders = transform.parent.GetComponent<TriggersAndColliders>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/