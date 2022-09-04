using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMovementNew : MonoBehaviour
{

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
        public MovementMultiplier(float amount, int durationInSeconds) : base(durationInSeconds)
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
        public DirectionChange(int durationInSeconds) : base(durationInSeconds)
        {

        }
        public override Vector2 adjustMovement(Vector2 velocity)
        {
            return new Vector2(-velocity.x, velocity.y);
        }
    }


    private Animator runanimator;
    //public AudioClip clip;

    public bool flying;
    public bool frozen = false;
    public float speed;
    public bool moving = true;
    public enum Direction { Left, Right };
    public Direction direction;
    public enum SpawnLocation {Normal, Random}
    public SpawnLocation spawnLocation;
    public enum Size { Small = 1, Medium = 2, Large = 3, ExtraLarge = 4, Giant = 5};
    public Size size;
    public bool rotateWithGround;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;
    TriggersAndCollidersNew triggersAndColliders;
    public int coolDownInSeconds;
    public int moneyCost;
    //private DateTime nextAttackSound;
    //public int attackSoundIntervalMillis;
    //private DateTime nextMoveSound;
    public int moveSoundIntervalMillis;
    public bool levitate;
    public bool immuneToBlizzard;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
        boxCollider2D = gameObject.transform.parent.Find("GroundCollider").GetComponent<BoxCollider2D>();
        runanimator = gameObject.transform.parent.GetComponent<Animator>();
        triggersAndColliders = gameObject.GetComponentInParent<TriggersAndCollidersNew>();
        //nextAttackSound = TimeManagement.CurrentTime();
        //nextMoveSound = TimeManagement.CurrentTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(frozen == false)
        {
            runanimator.enabled = true;
            AnimatorClipInfo[] clipInfo = runanimator.GetCurrentAnimatorClipInfo(0);
            if(clipInfo.Length == 0)
            {
                return;
            }
            if (clipInfo[0].clip.name.ToLower().Contains("spawning"))
            {
                return;
            }
            foreach(AnimatorClipInfo clip in clipInfo)
            {
                //Debug.Log(clip.clip.name);
            }

            if(rb2D != null)
            {
                rb2D.gravityScale = .5f;
            }
            
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



            if (rotateWithGround == true)
            {
                //SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                //gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0,-30* groundDirection.y));
            }







            if (moving == true)
            {
                Rigidbody2D rigidbody2D = gameObject.transform.parent.GetComponent<Rigidbody2D>();
                if(rigidbody2D != null)
                {
                    if(gameObject.GetComponent<LifeCycleNew>().GetDead() == false)
                    {
                        rigidbody2D.gravityScale = 1;
                    }
                }
                
                if (runanimator != null)
                {
                    if (!(flying == true && triggersAndColliders.inMelee == true))
                    {
                        /*
                        if (!runanimator.GetBool("special"))
                        {
                            runanimator.SetBool("moving", true);
                        }
                        */
                        runanimator.SetBool("moving", true);
                        runanimator.SetBool("inMelee", false);
                    }
                }
                /*
                if(moveSound.clip != null && nextMoveSound < TimeManagement.CurrentTime())
                {
                    int random = UnityEngine.Random.Range(-250, 250);
                    nextMoveSound = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 0, 0, moveSoundIntervalMillis + random);
                    moveSound.PlayOneShot(moveSound.clip);
                }
                */

                if (groundDirection != Vector2.zero)
                {
                    if (direction == Direction.Right)
                    {
                        groundDirection.x = Math.Abs(groundDirection.x);
                        groundDirection.y = groundDirection.y * -1;
                        if (rb2D != null)
                        {
                            rb2D.velocity = (speed / 20) * new Vector2(groundDirection.x, groundDirection.y);
                        }
                            
                        //Debug.Log("new velocity" + rb2D.velocity);
                    }
                    else
                    {
                        groundDirection.x = groundDirection.x * -1;
                        if (rb2D != null)
                        {
                            rb2D.velocity = (speed / 20) * new Vector2(groundDirection.x, groundDirection.y);
                        }
                            
                        //gameObject.GetComponentInParent<SpriteRenderer>().flipX = false;
                        //Debug.Log(rb2D.velocity);
                    }

                }
                for (int i = 0; i <= adjustments.Count - 1; i++)
                {
                    if (rb2D != null)
                    {
                        rb2D.velocity = adjustments[i].adjustMovement(rb2D.velocity);
                    }   
                }
            }

            else if (moving == false)
            {
                if (runanimator != null)
                {
                    runanimator.SetBool("moving", false);
                }
                /*
                if(attackSound.clip != null && nextMoveSound < TimeManagement.CurrentTime())
                {
                    int random = UnityEngine.Random.Range(-250, 250);
                    nextAttackSound = TimeManagement.CurrentTime() + new TimeSpan(0, 0, 0, 0, attackSoundIntervalMillis + random);
                    attackSound.PlayOneShot(attackSound.clip);
                }
                */
                Rigidbody2D rigidbody2D = gameObject.transform.parent.GetComponent<Rigidbody2D>();
                if(rigidbody2D != null)
                {
                    //To stop creature moving for fighting
                    rigidbody2D.gravityScale = 0;
                    rigidbody2D.velocity = new Vector3(0, 0, 0);
                }
                
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
        else
        {
            runanimator.enabled = false;
        }
    }

    private void OnDestroy()
    {
        /*
        if(dieSound.clip != null)
        {
            dieSound.PlayOneShot(dieSound.clip);
        }
        */
    }

    public void ChangeTeams()
    {
        try {
            if (gameObject.tag == "team1")
            {
                gameObject.tag = "team2";
                gameObject.transform.parent.tag = "team2";
                gameObject.transform.parent.Find("GroundCollider").tag = "team2";
                direction = Direction.Left;
                if (gameObject.transform.parent.Find("RangedCollider") != null)
                {
                    gameObject.transform.parent.Find("RangedCollider").tag = "team2";
                    gameObject.transform.parent.Find("RangedCollider").GetComponent<CapsuleCollider2D>().offset *= -1;
                }
                if (gameObject.transform.parent.Find("SpecialCollider") != null)
                {
                    gameObject.transform.parent.Find("SpecialCollider").tag = "team2";
                }
            }
            else
            {
                gameObject.tag = "team1";
                gameObject.transform.parent.tag = "team1";
                gameObject.transform.parent.Find("GroundCollider").tag = "team1";
                direction = Direction.Right;
                if (gameObject.transform.parent.Find("RangedCollider") != null)
                {
                    gameObject.transform.parent.Find("RangedCollider").tag = "team1";
                    gameObject.transform.parent.Find("RangedCollider").GetComponent<CapsuleCollider2D>().offset *= -1;
                }
                if (gameObject.transform.parent.Find("SpecialCollider") != null)
                {
                    gameObject.transform.parent.Find("SpecialCollider").tag = "team1";
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("general Movement New change teams " + e);
        }
        
        
        
    }
}
