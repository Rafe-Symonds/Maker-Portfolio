using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{ }
/*
    //public GameObject[] gameObjects;
    //public Transform[] target;
    //public float speed;
    //private int current = 0;
    //public Animator animator;
    //public int health = 10;
    //public AudioClip clip;
    //private bool dying = false;


    void OnTriggerEnter2D(Collider2D collider)
    {

        FireballAreaAffectScript fb = collider.gameObject.GetComponent<FireballAreaAffectScript>();
        if(fb == null)
        {
            return;
        }
        Health health = GetComponent<Health>();
        Damage damage = collider.gameObject.GetComponent<Damage>();
        //int fireballDamage = fb.damage;
        health.TakeDamage(damage);
        
        
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {

        //UnityEngine.Debug.Log("test");
        if (collision.gameObject.tag.Equals("team1") || collision.gameObject.tag.Equals("background"))
        {
            //UnityEngine.Debug.Log("Collision" + collision);
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
        
    }
    
    void OnParticleCollision()
    {

        UnityEngine.Debug.Log("Explosion");
        /*
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if( ps == null)
        {
            return;
        }
        

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            UnityEngine.Debug.Log("Skeleton Explosion");
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        
        
    }

    // Start is called before the first frame update
    
    void Start()
    {
        /*
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("target");
        target = new Transform[gameObjects.Length];
        Array.Sort(gameObjects, delegate (GameObject o1, GameObject o2)
        {
            return o1.name.CompareTo(o2.name);
        });
        for ( int i = 0; i < gameObjects.Length; i++)
        {
            target[i] = gameObjects[i].transform;
            
        }
        
    }



    // Update is called once per frame
    void Update()
    {
        /*
        if (dying != true)
        {
            if (transform.position != target[current].position)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                GetComponent<Rigidbody2D>().MovePosition(pos);
            }
            else
            {
                current = current + 1;
            }

            if (transform.position != target[target.Length - 1].position)
            {
                animator.SetBool("Moving", true);
            }
        }
        
        if( health <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
            
            GetComponent<GroundMovement>().SetDying(true);
            
            Destroy(gameObject, 1.4f);
            //add dying annimation
        }
        
    }

    void FixedUpdate()
    {
        //OnParticleTrigger();
    }
}
*/