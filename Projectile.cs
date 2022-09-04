using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject destroySoundGameObject;
    public float destroyDelay;
    public AudioSource destroySound;
    private Vector3 projectileRotatePoint = Vector3.zero;
    private Vector3 target;

    public void MoveProjectile(GameObject target)
    {
        //RangedAttackNew rangedAttack = transform.parent.GetComponentInChildren<RangedAttackNew>();
        Vector2 groundDirection;
        float groundDirectionx = 0;
        float groundDirectiony = 0;

        if(target != null && gameObject != null)
        {
            groundDirectionx = target.transform.position.x - gameObject.transform.position.x;
            groundDirectiony = target.transform.position.y - gameObject.transform.position.y;
            groundDirection = new Vector2(groundDirectionx, groundDirectiony);
            double distanceBetweenCreatures = Math.Sqrt((double)(groundDirectionx * groundDirectionx + groundDirectiony * groundDirectiony));

            Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = (projectileSpeed) * groundDirection / (float)distanceBetweenCreatures;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MoveProjectile(Vector3 target)
    {
        //RangedAttackNew rangedAttack = transform.parent.GetComponentInChildren<RangedAttackNew>();
        Vector2 groundDirection;
        float groundDirectionx = 0;
        float groundDirectiony = 0;

        groundDirectionx = target.x - gameObject.transform.position.x;
        groundDirectiony = target.y - gameObject.transform.position.y;
        groundDirection = new Vector2(groundDirectionx, groundDirectiony);
        double distanceBetweenCreatures = Math.Sqrt((double)(groundDirectionx * groundDirectionx + groundDirectiony * groundDirectiony));



        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = (projectileSpeed) * groundDirection / (float)distanceBetweenCreatures;
    }
    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (!gameObject.tag.Equals(collider.tag) && !collider.tag.Equals(gameObject.tag + "Base") && !collider.tag.Equals("background") && !gameObject.tag.Equals(collider.tag + "Base") && !collider.tag.Equals("treasureChest") && collider.name == "ContactCollider")
        {
            if (destroySound.clip != null)
            {
                //Debug.Log("playing projectile sound");
                destroySound.PlayOneShot(destroySound.clip);
            }
            //Debug.Log("Projectile hitting  " + gameObject);
            DamageNew damage = gameObject.GetComponent<DamageNew>();
            HealthNew health = collider.GetComponentInChildren<HealthNew>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
            
            Destroy(gameObject, destroyDelay);
        }
        else if (collider.name == "Ground" || collider.name.Equals("Boundary"))
        {
            Destroy(gameObject);
        }
        
    }
    void OnDestroy()
    {

        if(destroySoundGameObject != null)
        {
            GameObject soundGameObject = Instantiate(destroySoundGameObject, new Vector3(15, 15, 0), Quaternion.identity);
            soundGameObject.AddComponent<AudioSource>();
            soundGameObject.GetComponent<AudioSource>().clip = destroySound.clip;
            soundGameObject.GetComponent<AudioSource>().PlayOneShot(soundGameObject.GetComponent<AudioSource>().clip);
            Destroy(soundGameObject, 2);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.time = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
