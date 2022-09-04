using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    public enum Properties { WildernessWalk, Webable, Living};
    public Properties[] properties;


    public GameObject spirit;
    private GameObject spiritCreated;
    public GameObject bloodPrefab;
    public static int deathPosition = 100;
    public void TakenDamage()
    {
        if(bloodPrefab != null)
        {
            Vector3 pos = new Vector3(transform.position.x - 5, transform.position.y + 5, 0);
            GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb2D = blood.GetComponent<Rigidbody2D>();
            rb2D.velocity = new Vector2(-.5f, 1);
            Destroy(blood, 1f);
        }
        
        
    }

    public virtual void Death()
    {
        if (spiritCreated == null)
        {
            spiritCreated = Instantiate(spirit, transform.position, Quaternion.identity);
            gameObject.transform.position = new Vector3(deathPosition, 0, 0);
            deathPosition += 10;
            Destroy(gameObject, .1f);
        }
        

    }

    public virtual void Touch(GameObject other)
    {
        
    }
    public virtual void Leave(GameObject other)
    {
        

    }
    public bool CheckHasProperty(Properties properties2)
    {
        for(int i = 0; i <= properties.Length - 1; i++)
        {
            if(properties[i] == properties2)
            {
                return true;
            }
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
