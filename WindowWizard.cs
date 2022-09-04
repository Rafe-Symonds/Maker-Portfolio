using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowWizard : DamageNew
{
    public GameObject projectilePrefab;
    private GameObject troopTarget;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        if (gameobject.tag == "team1" && gameobject.name == "ContactCollider")
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetBool("attack", true);
            troopTarget = gameobject;
            Invoke("CreateFireBall", 0.5f);
        }
    }
    public void CreateFireBall()
    {
        if (troopTarget != null && troopTarget.transform != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity, transform);
            newProjectile.tag = "treasureChest";
            newProjectile.GetComponent<Projectile>().MoveProjectile(troopTarget);
            newProjectile.GetComponent<ParticleSystem>().time = 10;
            troopTarget = null;
        }

    }
}
