using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolumeOnStart : MonoBehaviour
{
    public GameObject settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.GetComponent<SettingsMenu>().SetVolume(PlayerPrefs.GetFloat("volume"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
