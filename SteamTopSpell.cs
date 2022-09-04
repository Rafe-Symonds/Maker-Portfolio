using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteamTopSpell : MonoBehaviour
{
    bool alreadyUsed = false;



    // Start is called before the first frame update
    void Awake()
    {
        if (alreadyUsed == false && SceneManager.GetActiveScene().name != "MainMenu" && gameObject.CompareTag("team1"))
        {
            SteamConnection.UnlockAchievement("ACH_TOPSPELL");
            alreadyUsed = true;
            Debug.Log("Unlocking top level spell");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
