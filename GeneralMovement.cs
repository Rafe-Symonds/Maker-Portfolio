using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMovement : MonoBehaviour
{

    public int coolDownInSeconds;

    private List<MovementAdjustment> adjustments = new List<MovementAdjustment>();

    public void AddAdjustment(MovementAdjustment adjustment)
    {
        adjustments.Add(adjustment);
        adjustments.Sort(MovementAdjustment.CompareMovementAdjustment);
        //Debug.Log(adjustments.Count);
    }

    public void RemoveAdjustment(MovementAdjustment adjustment)
    {
        adjustments.Remove(adjustment);
        //Debug.Log("Remove " + adjustments.Count);
    }



    public abstract class MovementAdjustment
    {

        private DateTime expirationTime;

        public MovementAdjustment(int durationInSeconds)
        {
            TimeSpan timeLeft = new TimeSpan(0, 0, durationInSeconds);
            this.expirationTime = TimeManagement.CurrentTime() + timeLeft;
        }


        public abstract Vector2 adjustMovement(Vector2 velocity);


        public static int CompareMovementAdjustment(MovementAdjustment a, MovementAdjustment b)
        {
            if (a == null)
            {
                if (b == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (b == null)
                {
                    return 1;
                }
                else
                {
                    if (a.expirationTime < b.expirationTime)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        } //Same code as the health script


        public DateTime GetDuration()
        {
            return expirationTime;
        }

    }





    public class MovementMultiplier : MovementAdjustment
    {
        private float amount;
        public MovementMultiplier(float amount, int durationInSeconds): base(durationInSeconds)
        {
            this.amount = amount;


        }

        public override Vector2 adjustMovement(Vector2 velocity)
        {
            return velocity * amount;
        }


    }

    public class DirectionChange : MovementAdjustment
    {
        public DirectionChange(int durationInSeconds): base(durationInSeconds)
        {

        }
        public override Vector2 adjustMovement(Vector2 velocity)
        {
            return new Vector2(-velocity.x, velocity.y);
        }
    }


    public Animator runanimator;
    public AudioClip clip;

    public float speed;
    public bool moving = true;
    public enum Direction { Left, Right };
    public Direction direction;
    public enum Size { Small = 1, Medium = 2, Large = 3, ExtraLarge = 4, Giant = 5};
    public Size size;
    public bool rotateWithGround;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;

    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        RaycastHit2D[] hit = Physics2D.BoxCastAll(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f);
        Color rayColor = Color.red;

        Vector2 groundDirection = Vector2.zero;

        for (int i = 0; i <= hit.Length - 1; i++)
        {
            //Debug.Log(hit[i].collider.name);
            if (hit[i].collider != null && hit[i].collider.name == "Ground")
            {
                rayColor = Color.green;
                //Debug.Log("collider hit");
                //Debug.Log(hit[i].normal);
                groundDirection = hit[i].normal;
                groundDirection = new Vector2(groundDirection.y, groundDirection.x);
                break;
            }
        }



        //Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x,0), Vector2.down * (boxCollider2D.bounds.extents.y + 1f), rayColor);
        //Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + 1f), rayColor);
        //Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.right * (boxCollider2D.bounds.extents.y + 1f), rayColor);



        if(rotateWithGround == true)
        {
            //SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            //gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0,-30* groundDirection.y));
        }







        if (moving == true)
        {
            if(runanimator != null)
            {
                runanimator.SetBool("Moving", true);
            }
            
            if (groundDirection != Vector2.zero)
            {
                if (direction == Direction.Right)
                {
                    groundDirection.x = Math.Abs(groundDirection.x);
                    groundDirection.y = groundDirection.y * -1;
                    rb2D.velocity = (speed / 20) * new Vector2(groundDirection.x, groundDirection.y);
                    //Debug.Log("new velocity" + rb2D.velocity);
                }
                else
                {
                    groundDirection.x = groundDirection.x * -1;
                    rb2D.velocity = (speed / 20) * new Vector2(groundDirection.x, groundDirection.y);
                    //Debug.Log(rb2D.velocity);
                }

            }
            for (int i = 0; i <= adjustments.Count - 1; i++)
            {
                rb2D.velocity = adjustments[i].adjustMovement(rb2D.velocity);
            }
        }
        else if(moving == false)
        {
            runanimator.SetBool("Moving", false);
        }
        if (adjustments.Count > 0)
        {
            if (adjustments[0].GetDuration() < TimeManagement.CurrentTime())
            {
                Debug.Log("Deleted MovementAdjustment");
                adjustments.RemoveAt(0);
            }
        }

    }
}
