using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNoStructures : Projectile
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Contains("Base"))
        {
            return;
        }
        base.OnTriggerEnter2D(collider);
    }
}
