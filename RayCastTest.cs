using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{

    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + .1f);
        Color rayColor = Color.red;

        for( int i = 0; i <= hit.Length - 1; i++)
        {
            Debug.Log(hit[i].collider.name);
            if (hit[i].collider != null && hit[i].collider.name == "Background")
            {
                rayColor = Color.green;
                Debug.Log("collider hit");
                Debug.Log(hit[i].normal);
                //Debug.DrawLine(boxCollider2D.bounds.center, boxCollider2D.bounds.center + hit[i].normal, Color.yellow, 10f);
                break;
            }
        }


        
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + .1f), rayColor);

        /*
        if(h > 1)
        {
            Debug.Log("Hit =" + hits[1]);
            Debug.Log("normal =" + hits[1].normal);
        }
        */
    }
}
