using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonTechTree : MonoBehaviour
{
    /*

        This script is on each hotbar button on the techtree


    */

    public void OnMouseUp()
    {
        //UnityEngine.Debug.Log("ButtonMouseUp");
    }

    public void OnMouseOver()
    {
        GeneralTechTree.MouseOverButton(gameObject);
        //UnityEngine.Debug.Log("ButtonMouseOver");
    }

    public void OnMouseExit()
    {
        GeneralTechTree.MouseOverButton(null);
    }


    // Start is called before the first frame update
    void Start()
    {
        //Start is finding the correct image in the saved data for this button
        //


        int index = (int)gameObject.name[6] - '0';
        
        if (SaveAndLoad.Get().GetHotbarSkillImages()[index] != null)
        {
            GameObject techTreeSkill = Instantiate(Resources.Load("Prefabs/TechTree/" + SaveAndLoad.Get().GetHotbarSkillImages()[index]) as GameObject,
                                            transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            techTreeSkill.transform.SetParent(gameObject.transform);
            techTreeSkill.transform.localScale = new Vector3(1.3f, 1.3f, 0);
            Destroy(techTreeSkill.GetComponent<MeshRenderer>()); //This code sets up the hotbar that is saved during the tech tree scene
            Destroy(techTreeSkill.transform.Find("CostText").GetComponent<TextMeshPro>());
            
            GameObject skillOnTechTree = GameObject.Find(SaveAndLoad.Get().GetHotbarSkillImages()[index]);
            
            GeneralTechTree generalTechTree = skillOnTechTree.GetComponent<GeneralTechTree>();
            generalTechTree.copyOfSkillOnButton = techTreeSkill;

            //Moving the sprite forwards so it is visible
            GameObject sprite = techTreeSkill.transform.Find("Sprite").gameObject;
            sprite.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        
    }
}

//old tech tree code
/*
   public void OnMouseUp()
    {
        //UnityEngine.Debug.Log("ButtonMouseUp");
    }

    public void OnMouseOver()
    {
        GeneralTechTree.MouseOverButton(gameObject);
        //UnityEngine.Debug.Log("ButtonMouseOver");
    }

    public void OnMouseExit()
    {
        GeneralTechTree.MouseOverButton(null);
    }


    // Start is called before the first frame update
    void Start()
    {
        //Start is finding the correct image in the saved data for this button
        //


        int index = (int)gameObject.name[6] - '0';
        
        if (SaveAndLoad.Get().GetHotbarSkillImages()[index] != null)
        {
            GameObject techTreeSkill = Instantiate(Resources.Load("Prefabs/TechTree/" + SaveAndLoad.Get().GetHotbarSkillImages()[index]) as GameObject,
                                            transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            techTreeSkill.transform.SetParent(gameObject.transform);
            techTreeSkill.transform.localScale = new Vector3(1.3f, 1.3f, 0);
            Destroy(techTreeSkill.GetComponent<MeshRenderer>()); //This code sets up the hotbar that is saved during the tech tree scene
            Destroy(techTreeSkill.transform.Find("CostText").GetComponent<TextMeshPro>());
            
            GameObject skillOnTechTree = GameObject.Find(SaveAndLoad.Get().GetHotbarSkillImages()[index]);
            
            GeneralTechTree generalTechTree = skillOnTechTree.GetComponent<GeneralTechTree>();
            generalTechTree.copyOfSkillOnButton = techTreeSkill;

            //Moving the sprite forwards so it is visible
            GameObject sprite = techTreeSkill.transform.Find("Sprite").gameObject;
            sprite.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        
    }
*/
