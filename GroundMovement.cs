using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GroundMovement : MonoBehaviour
{ }
/*
    public List<Transform> targets = new List<Transform>();
    public float speed;
    private int current = 0;
    public bool moving = true;
    public Animator runanimator;
    public AudioClip clip;
    public enum Direction{ Left, Right};
    public Direction direction;


    public void SetDying(bool dying)
    {
        this.moving = !dying;
        runanimator.SetBool("Moving", false);
    }


    

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("target");
        Array.Sort(gameObjects, delegate (GameObject o1, GameObject o2)
        {
            if( direction == Direction.Right)
            {
                return o1.name.CompareTo(o2.name);
            }
            else
            {
                return o1.name.CompareTo(o2.name) * -1;
            }
            

        });
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (direction == Direction.Right && gameObjects[i].transform.position.x > transform.position.x)
            {
                targets.Add(gameObjects[i].transform);
            }
            else if (direction == Direction.Left && gameObjects[i].transform.position.x < transform.position.x)
            {
                targets.Add(gameObjects[i].transform);
            }
            

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            
                if (transform.position != targets[current].position)
                {
                    Vector3 pos = Vector3.MoveTowards(transform.position, targets[current].position, speed * Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(pos);
                }
                else if (current < targets.Count - 1)
                {
                    current++;
                }
                else
                {
                    runanimator.SetBool("Moving", false);
                }

                if (transform.position != targets[targets.Count - 2].position)
                {
                    runanimator.SetBool("Moving", true);
                }
            
        }
        Health healthcomponent = GetComponent<Health>();
        int health = healthcomponent.health;
        if (health <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);

            SetDying(true);
            //add dying annimation
            //maybe add a delay on dying for an annimation
        }
    }
}
*/