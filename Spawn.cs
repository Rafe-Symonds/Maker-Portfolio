using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject spawnPoint;
    public GameObject prefab;
    public bool summoner;
    private ManaBar manabar;
    public int manaUsage;

    void OnMouseDown()
    {
        if (summoner == true)
        {
            if (GameObject.FindGameObjectWithTag("team1Summoner") == null)
            {
                if (manabar.CheckAndReduceMana(manaUsage) == true)
                {
                    Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
                    UnityEngine.Debug.Log("summoner");
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        manabar = GameObject.Find("ManaBarNew").GetComponent<ManaBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}