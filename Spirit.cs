using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vector3 = new Vector3(transform.position.x, 100, 0);
        Vector3 pos = Vector3.MoveTowards(transform.position, vector3, 10 * Time.deltaTime);
        rb2D.MovePosition(pos);
    }
}
