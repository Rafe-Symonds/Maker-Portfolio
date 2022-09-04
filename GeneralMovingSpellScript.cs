using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMovingSpellScript : MonoBehaviour
{
    private Vector3 target = Vector3.zero;
    public float speed;
    public GameObject explosion;
    

    public void Target(Vector3 to)
    {
        //Debug.Log("target =" + target);
        target = to;
    }


    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (target == Vector3.zero)
        {
            return;
        }

        if (transform.position != target)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(pos);
            Vector3 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
        }
        if (transform.position == target)
        {

            Instantiate(explosion, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
            
        }
    }
}