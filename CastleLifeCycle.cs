using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleLifeCycle : LifeCycleNew
{
    private Animator runanimator;
    

    public override void Death(Animator animator)
    {
        if(dead == false)
        {
            gameObject.transform.parent.Find("OnDeathCollider").gameObject.SetActive(true);
            if (gameObject.CompareTag("team1Base"))
            {
                Debug.Log("Wizard falling");
                if(GameObject.Find("Base1").transform.childCount > 0)
                {
                    GameObject wizard = GameObject.Find("Base1").transform.GetChild(0).gameObject;
                    wizard.GetComponent<Rigidbody2D>().gravityScale = .25f;
                }
                
            }
            if (gameObject.CompareTag("team2Base"))
            {
                GameObject enemyBossParent = GameObject.Find("EnemyBoss");
                if (enemyBossParent.transform.childCount > 0)
                {
                    GameObject boss = enemyBossParent.transform.GetChild(0).gameObject;
                    boss.GetComponent<Rigidbody2D>().gravityScale = .25f;
                }
                
            }
            base.Death(animator);
            dead = true;
        }
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        runanimator = gameObject.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
