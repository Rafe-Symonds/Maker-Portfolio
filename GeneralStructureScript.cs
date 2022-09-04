using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStructureScript : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        DamageNew damage = gameObject.GetComponent<DamageNew>();
        damage.specialDamageEffectOnEnemy(collider.gameObject);
    }
}
