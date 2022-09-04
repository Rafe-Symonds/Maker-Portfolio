using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceStrength : DamageNew
{
    public float reduceStrengthAmount;
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber > 80)
        {
            DamageNew damageEnemy = gameobject.GetComponent<DamageNew>();
            Debug.Log(damageEnemy);
            damageEnemy.AddDamageEffect(new DamageEffect(reduceStrengthAmount, 10));
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
