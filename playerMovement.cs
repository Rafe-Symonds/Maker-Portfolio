using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public int health;
    public Animator animator;

    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        health = health - 1;
        

    }



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        if(health < 1)
        {
            Destroy(gameObject);
        }
        
    }

    void FixedUpdate()

    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.transform.Translate(movement * speed);
        

    }
}
