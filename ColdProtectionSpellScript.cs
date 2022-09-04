using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdProtectionSpellScript : ProtectionSpellScript
{
    public class ColdProtection : HealthNew.Protection
    {
        GameObject contactCollider;
        public ColdProtection(DamageNew.DamageType type, float amount, int duration, GameObject contactCollider) : base(type, amount, duration)
        {
            this.contactCollider = contactCollider;
        }
        public override void OnEnd()
        {
            if(contactCollider != null)
            {
                GeneralMovementNew generalMovementNew = contactCollider.transform.Find("ContactCollider").GetComponent<GeneralMovementNew>();
                if (generalMovementNew != null)
                {
                    generalMovementNew.immuneToBlizzard = false;
                }
            }
        }
    }



    public override void spellAffectPolymorphic(GameObject gameobject)
    {
        if (!gameObjectsAlreadyHit.Contains(gameobject) && gameObject.CompareTag(gameobject.tag))
        {
            HealthNew health = gameobject.transform.Find("ContactCollider").GetComponent<HealthNew>();
            if (health != null)
            {
                //Debug.Log("Protected");
                health.AddProtection(new ColdProtection(type, amount, duration, gameobject.transform.Find("ContactCollider").gameObject));
            }
            GeneralMovementNew generalMovementNew = gameobject.transform.Find("ContactCollider").GetComponent<GeneralMovementNew>();
            if(generalMovementNew != null)
            {
                generalMovementNew.immuneToBlizzard = true;
            }
            gameObjectsAlreadyHit.Add(gameobject);
        }
    }
}
