using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMana : MonoBehaviour
{

    
    public ManaBar manabar;
    

    
    // Start is called before the first frame update
    void Start()
    {
        manabar = GameObject.Find("Manabar").GetComponent<ManaBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
