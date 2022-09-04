using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamConnection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(2069220);
            Debug.Log(Steamworks.SteamClient.Name);
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }

    void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
        Debug.Log("Shutting down steam connection");
    }

    public static void UnlockAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        achievement.Trigger();

    }

    // Update is called once per frame
    void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }
}
