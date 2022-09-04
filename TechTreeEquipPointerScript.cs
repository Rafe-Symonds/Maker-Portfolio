using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTreeEquipPointerScript : MonoBehaviour
{
    public static GameObject techTreePointer;
    // Start is called before the first frame update
    void Start()
    {
        techTreePointer = GameObject.Find("TechTreePointer");
        HideTechTreePointer();
    }

    public static void HideTechTreePointer()
    {
        techTreePointer.SetActive(false);
    }

    public static void ShowTechTreePointer()
    {
        techTreePointer.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
