using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMapStar : MonoBehaviour
{
    public GameObject starExplosionSoundGameObject;
    public AudioSource destroySound;
    public GameObject shield;
    public void MoveStarToShield(GameObject shield)
    {
        this.shield = shield;
        float groundDirectionx = shield.transform.position.x - gameObject.transform.position.x;
        float groundDirectiony = shield.transform.position.y - gameObject.transform.position.y;
        Vector2 groundDirection = new Vector2(groundDirectionx, groundDirectiony);
        double distanceBetweenCreatures = Math.Sqrt((double)(groundDirectionx * groundDirectionx + groundDirectiony * groundDirectiony));

        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = 10 * groundDirection / (float)distanceBetweenCreatures;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        /*
        Debug.Log("GameObject = " + gameObject + "  Collider = " + collider.name);
        Debug.Log("Target shield = " + shield);
        if (collider.name.Equals(shield.name))
        {
            shield.GetComponent<MapShieldScript>().ShowStars();
            shield.GetComponent<MapShieldScript>().StarExplosionAnimation();
            Debug.Log("Destroying Stars");

            
        }
        */
        if (collider.name.Equals(shield.name))
        {
            Destroy(gameObject);
        }
            
    }
    void OnDestroy()
    {
        if (starExplosionSoundGameObject != null)
        {
            GameObject soundGameObject = Instantiate(starExplosionSoundGameObject, new Vector3(15, 15, 0), Quaternion.identity);
            soundGameObject.AddComponent<AudioSource>();
            soundGameObject.GetComponent<AudioSource>().clip = destroySound.clip;
            soundGameObject.GetComponent<AudioSource>().PlayOneShot(soundGameObject.GetComponent<AudioSource>().clip);
            Destroy(soundGameObject, 2);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

}
