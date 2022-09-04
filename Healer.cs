using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public int healAmount;

    DateTime nextHeal = TimeManagement.CurrentTime();

    CircleCollider2D circleCollider;
    private List<GameObject> creaturesInArea = new List<GameObject>();
    GeneralMovementNew generalMovement;

    public void AddCreatureToHeal(GameObject contactCollider)
    {
        if (!creaturesInArea.Contains(contactCollider))
        {
            creaturesInArea.Add(contactCollider);
        }
    }


    public void Heal()
    {
        Animator animator = gameObject.GetComponentInParent<Animator>();
        animator.SetBool("special", true);
        
        //animator.SetBool("moving", false);
        foreach(GameObject contactCollider in creaturesInArea)
        {
            contactCollider.GetComponent<AllEffectsOnTroop>().AddToList(new InstantHeal(contactCollider.transform.parent.gameObject, healAmount));
        }
        
    }

    public void RemoveCreatureFromHeal(GameObject contactCollider)
    {
        creaturesInArea.Remove(contactCollider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name.Equals("ContactCollider") && collider.tag.Equals(gameObject.tag))
        {
            
            AddCreatureToHeal(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name.Equals("ContactCollider"))
        {
            RemoveCreatureFromHeal(collider.gameObject);
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        generalMovement = gameObject.transform.parent.GetComponentInChildren<GeneralMovementNew>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Animator animator = gameObject.GetComponentInParent<Animator>();
        //animator.SetBool("special", false);
        if (nextHeal < TimeManagement.CurrentTime() && generalMovement.moving == true)
        {
            nextHeal = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 10);
            Heal();
        }
    }
}
