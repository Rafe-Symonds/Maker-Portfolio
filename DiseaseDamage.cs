using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseDamage : DamageNew
{
    public override void specialDamageEffectOnEnemy(GameObject gameobject)
    {
        bool living = false;

        DamageType[] enemyCharacteristics = gameobject.GetComponent<HealthNew>().creatureCharacteristics;

        foreach (DamageType enemyDamageType in enemyCharacteristics)
        {
            if (enemyDamageType == DamageType.living)
            {
                Debug.Log("Checking fear true " + gameobject);
                living = true;
                break;
            }
        }
        if (living == false)
        {
            return;
        }
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber > 90)
        {
            gameobject.GetComponent<AllEffectsOnTroop>().AddToList(new DiseaseEffect(8, gameobject.transform.parent.gameObject));
        }
    }
}
