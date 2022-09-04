using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowArcher : DamageNew
{
    public GameObject projectilePrefab;
    private GameObject troopTarget;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber > 85)
        {
            if (gameobject.tag == "team1" && gameobject.name == "ContactCollider")
            {
                Animator animator = gameObject.GetComponent<Animator>();
                animator.SetBool("attack", true);
                troopTarget = gameobject;
                Invoke("CreateArrow", 0.5f);
            }
        }
    }
    public void CreateArrow()
    {
        if(troopTarget != null && troopTarget.transform != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity, transform);
            newProjectile.tag = "team2";
            newProjectile.GetComponent<Projectile>().MoveProjectile(troopTarget);
            newProjectile.GetComponent<SpriteRenderer>().flipX = true;
            troopTarget = null;
        }
        
    }
}
