using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestScript : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public GameObject parent;
    private bool pickedUp = false;
    public AudioSource chestSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickedUp == false)
        {
            if (collision.gameObject != parent)
            {
                if (collision.gameObject.GetComponentInChildren<GeneralMovementNew>() != null)
                {
                    Animator animator = gameObject.GetComponentInParent<Animator>();
                    if (collision.gameObject.tag == "team1")
                    {
                        if(chestSound.clip != null)
                        {
                            chestSound.PlayOneShot(chestSound.clip);
                        }
                        animator.SetBool("team1Contact", true);
                        GeneralMoney money = GameObject.Find("Money").GetComponent<GeneralMoney>();
                        money.AddMoney(50);
                        Destroy(gameObject.GetComponent<BoxCollider2D>());
                    }

                    if (collision.gameObject.tag == "team2")
                    {
                        animator.SetBool("team2Contact", true);
                        Destroy(gameObject.GetComponent<BoxCollider2D>());
                    }
                    pickedUp = true;
                }

            }
            
        }   
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
