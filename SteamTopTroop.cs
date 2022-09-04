using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteamTopTroop : MonoBehaviour
{
    bool alreadyUsed = false;


    
    // Start is called before the first frame update
    void Start()
    {
        if(alreadyUsed == false && SceneManager.GetActiveScene().name != "MainMenu" && gameObject.CompareTag("team1"))
        {
            SteamConnection.UnlockAchievement("ACH_TOPTROOP");
            alreadyUsed = true;
            Debug.Log("Unlocking top level spell");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
