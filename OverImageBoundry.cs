using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverImageBoundry : MonoBehaviour
{
    public static bool overImage = false;


    void OnMouseOver()
    {
        overImage = true;
    }
    void OnMouseExit()
    {
        overImage = false;
    }
}
