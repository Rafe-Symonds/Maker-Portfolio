using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralMoney : MonoBehaviour
{
    private int totalMoney;
    public int startMoney;
    private int moneyTimeCounter = 0;
    public AudioSource notEnoughMoney;
    public bool CheckAndReduceMoney(int moneyUsage)
    {
        if (totalMoney < moneyUsage)
        {
            //flash money
            if (notEnoughMoney.clip != null)
            {
                notEnoughMoney.PlayOneShot(notEnoughMoney.clip);
            }
            return false;
        }
        else
        {
            totalMoney -= moneyUsage;
            return true;
        }
    }

    public void SetMoney(int money)
    {
        startMoney = money;
    }

    public void AddMoney(int money)
    {
        totalMoney += money;
    }


    // Start is called before the first frame update
    void Start()
    {
        totalMoney = startMoney;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TextMeshPro moneyText = gameObject.transform.Find("MoneyText").GetComponent<TextMeshPro>();
        moneyText.text = totalMoney.ToString();
        moneyTimeCounter++;
        if(moneyTimeCounter >= 45)
        {
            moneyTimeCounter = 0;
            totalMoney += 1;
        }
    }
}
