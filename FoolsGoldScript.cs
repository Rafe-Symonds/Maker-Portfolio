using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoolsGoldScript : MonoBehaviour
{
    public int goldAmount;
    public GameObject sparkle;
    // Start is called before the first frame update
    void Start()
    {
        GameObject money = GameObject.Find("Money");
        money.GetComponent<GeneralMoney>().AddMoney(goldAmount);
        GameObject sparkleObject = Instantiate(sparkle, money.transform.position, Quaternion.identity);
        sparkleObject.transform.localScale *= 2;
        Destroy(gameObject, 2f);
        Destroy(sparkleObject, 2f);
    }

}
