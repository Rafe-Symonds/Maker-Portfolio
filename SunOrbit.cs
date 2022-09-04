using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    public GameObject center;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(center.transform.position, Vector3.forward, 1.15f * Time.deltaTime);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
