using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmSupportingScript : MonoBehaviour
{
    GameObject parentTroop = null;
    public void StartToMoveCollidersBack(GameObject parentTroop)
    {
        this.parentTroop = parentTroop;
        Invoke("MoveCollidersBack", 0.5f);
    }
    public void MoveCollidersBack()
    {
        
        GameObject contactCollider = parentTroop.transform.Find("ContactCollider").gameObject;
        contactCollider.transform.localPosition = new Vector3(0, 0, 0);
        Transform rangedCollider = parentTroop.transform.Find("RangedCollider");
        if (rangedCollider != null)
        {
            rangedCollider.transform.localPosition = new Vector3(0, 0, 0);
        }
        
    }
}
