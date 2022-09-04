using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorDamage : DamageNew
{
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        if (gameobject.name == "ContactCollider" && gameobject.CompareTag("team1"))
        {
            if (gameobject.GetComponent<GeneralMovementNew>().levitate == true)
            {
                return;
            }
            int num = UnityEngine.Random.Range(0, 100);
            if (num > 95)
            {
                Animator animator = gameObject.GetComponentInParent<Animator>();
                if(animator.GetBool("special") == false)
                {
                    gameobject.GetComponent<HealthNew>().TakeDamage(1000, DamageType.physical);
                    animator.SetBool("special", true);
                }
            }
        }
    }
}
