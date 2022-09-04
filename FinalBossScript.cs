using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{
    HealthNew health;
    bool transformed = false;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponentInChildren<HealthNew>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transformed == false && health.health < 100)
        {
            transformed = true;
            Animator animator = gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = gameObject.transform.Find("SecondForm").GetComponent<Animator>().runtimeAnimatorController;
        }
    }
}
