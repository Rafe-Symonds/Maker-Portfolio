using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechTreeToolTipScript : MonoBehaviour
{
    private static TechTreeToolTipScript techTreeToolTipScript;
    private int costTolearn;
    private GameObject buttonClicked = null;
    public static bool mouseOverToolTip = false;
    public GameObject learnButtonGameObject;
    public GameObject unlearnButtonGameObject;
    private void TurnLearnButtonGray()
    {
        learnButtonGameObject.SetActive(true);
        Button learnButton = gameObject.transform.Find("LearnButton").GetComponent<Button>();
        unlearnButtonGameObject.SetActive(false);
        learnButton.interactable = false;
        ColorBlock colors = learnButton.colors;
        colors.normalColor = new Color32(75, 75, 75, 255);
        colors.highlightedColor = new Color32(75, 75, 75, 255);
        colors.pressedColor = new Color32(75, 75, 75, 255);
        learnButton.colors = colors;
    }
    private void TurnLearnButtonGray(GameObject gameobject)
    {
        //maybe how to fix the relearning issue
    }


    public static void Static_TurnLearnButtonGray()
    {
        techTreeToolTipScript.TurnLearnButtonGray();
    }

    private void TurnLearnButtonNormal()
    {
        learnButtonGameObject.SetActive(true);
        unlearnButtonGameObject.SetActive(false);
        Button learnButton = learnButtonGameObject.GetComponent<Button>();
        learnButton.interactable = true;
        ColorBlock colors = learnButton.colors;
        colors.normalColor = new Color32(255, 255, 255, 255);
        colors.highlightedColor = new Color32(255, 255, 255, 255);
        colors.pressedColor = new Color32(255, 255, 255, 255);
        learnButton.colors = colors;
    }
    private void ShowUnlearnButton()
    {
        learnButtonGameObject.SetActive(false);
        unlearnButtonGameObject.SetActive(true);
    }
    private void ShowToolTip(GameObject buttonClicked)
    {
        this.buttonClicked = buttonClicked;
        costTolearn = buttonClicked.GetComponent<GeneralTechTree>().costToLearn;
        gameObject.SetActive(true);
        try
        {
            string buttonName = buttonClicked.name;
            if (buttonName.Contains("Clone"))
            {
                buttonName = buttonName.Substring(0, buttonName.Length - 7);
            }
            StreamReader stream = new StreamReader(Application.streamingAssetsPath + "/Text/TechTreeText/" + buttonName + ".txt");
            TextMeshProUGUI titleText = gameObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI bodyText = gameObject.transform.Find("BodyText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI details = gameObject.transform.Find("Details").GetComponent<TextMeshProUGUI>();
            titleText.text = stream.ReadLine();
            bodyText.text = stream.ReadLine();
            details.text = stream.ReadToEnd();
        }
        
        catch(Exception e)
        {
            Debug.Log("File not found");
        }
        string name = buttonClicked.name;
        name = name.Substring(name.Length - 7);
        //Debug.Log(name);

        if (SaveAndLoad.Get().GettechTreePoints() < costTolearn || SaveAndLoad.Get().GetskillsCollected().Contains(buttonClicked.name)
            || name == "(Clone)" || buttonClicked.GetComponent<GeneralTechTree>().ParentsHaveBeenLearned() == false)
        {
            TurnLearnButtonGray();
        }
        else
        {
            TurnLearnButtonNormal();
        }

        if (SaveAndLoad.Get().GetskillsCollected().Contains(buttonClicked.name))
        {
            //turn the learn to unlearn
            ShowUnlearnButton();
        }
    }
    public static void Static_ShowToolTip(GameObject buttonClicked)
    {
        techTreeToolTipScript.ShowToolTip(buttonClicked);
    }
    private void HideToolTip()
    {
        gameObject.SetActive(false);
    }
    public static void Static_HideToolTip()
    {
        techTreeToolTipScript.HideToolTip();
    }

    public void LearnButton()
    {
        if (SaveAndLoad.Get().GettechTreePoints() >= costTolearn)
        {
            TechTreeEquipPointerScript.ShowTechTreePointer();

            TurnLearnButtonGray();
            learnButtonGameObject.SetActive(false);
            unlearnButtonGameObject.SetActive(true);
            buttonClicked.GetComponent<GeneralTechTree>().HidePoints();
            buttonClicked.GetComponent<GeneralTechTree>().ChangeMaterialToLearned();
            SaveAndLoad.Get().UpdateTechTreePoints(-costTolearn);
            SaveAndLoad.Get().UpdateSkillsCollected(buttonClicked);
            
            SaveAndLoad.SaveData();
            foreach(GameObject child in buttonClicked.GetComponent<GeneralTechTree>().childrenSkills)
            {
                GeneralTechTree childTechTree = child.GetComponent<GeneralTechTree>();
                if (childTechTree.ParentsHaveBeenLearned() == true)
                {
                    childTechTree.ChangeMaterialToParentsLearned();
                }
            }
        }

    }
    public void UnlearnButton()
    {
        TurnLearnButtonNormal();
        UnlearnSkills(buttonClicked);
    }
    public void UnlearnSkills(GameObject skill)
    {
        int index = SaveAndLoad.Get().FindHotbarIndexForSkill(skill);
        Debug.Log("index of skill " + index);
        if (index != -1)
        {
            GameObject hotbar = GameObject.Find("Hotbar" + index);
            Destroy(hotbar.transform.GetChild(0).gameObject);
            SaveAndLoad.Get().UpdateHotbarSkills(index, null);
            SaveAndLoad.Get().UpdateHotbarSkillImages(index, null);
        }

        SaveAndLoad.Get().UpdateTechTreePoints(skill.GetComponent<GeneralTechTree>().costToLearn);
        SaveAndLoad.Get().RemoveSkillCollected(skill);
        if(skill.GetComponent<GeneralTechTree>().ParentsHaveBeenLearned() == true)
        {
            skill.GetComponent<GeneralTechTree>().ChangeMaterialToParentsLearned();
        }
        
        skill.transform.Find("CostText").GetComponent<TextMeshPro>().enabled = true;
        foreach(GameObject child in skill.GetComponent<GeneralTechTree>().childrenSkills)
        {
            child.GetComponent<GeneralTechTree>().ChangeMaterialToParentsNotLearned();
            if (SaveAndLoad.Get().GetskillsCollected().Contains(child.name)){
                UnlearnSkills(child);
            } 
        }
        SaveAndLoad.SaveData();
    }
    public void CloseButton()
    {
        mouseOverToolTip = false;
        TechTreeToolTipScript.Static_HideToolTip();
    }



    // Start is called before the first frame update
    void Awake()
    {
        techTreeToolTipScript = this;
        TechTreeToolTipScript.Static_HideToolTip();
        //gameObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().geometrySortingOrder = 15;
        //gameObject.transform.Find("BodyText").GetComponent<TextMeshProUGUI>()
        //gameObject.transform.Find("Details").GetComponent<TextMeshProUGUI>()
        //gameObject.transform.Find("Details").GetComponent<Button>()
    }

    void OnMouseOver()
    {
        mouseOverToolTip = true;
        //Debug.Log("Over the tool tip");
    }
    void OnMouseExit()
    {
        mouseOverToolTip = false;
        //Debug.Log("leaving the tool tip");
    }

    
}
//old tech tree code
/*
     private static TechTreeToolTipScript techTreeToolTipScript;
private int costTolearn;
private GameObject buttonClicked = null;
public static bool mouseOverToolTip = false;

private void TurnLearnButtonGray()
{
    Button learnButton = gameObject.transform.Find("LearnButton").GetComponent<Button>();
    learnButton.interactable = false;
    ColorBlock colors = learnButton.colors;
    colors.normalColor = new Color32(75, 75, 75, 255);
    colors.highlightedColor = new Color32(75, 75, 75, 255);
    colors.pressedColor = new Color32(75, 75, 75, 255);
    learnButton.colors = colors;
}
private void TurnLearnButtonGray(GameObject gameobject)
{
    //maybe how to fix the relearning issue
}


public static void Static_TurnLearnButtonGray()
{
    techTreeToolTipScript.TurnLearnButtonGray();
}

private void TurnLearnButtonNormal()
{
    Button learnButton = gameObject.transform.Find("LearnButton").GetComponent<Button>();
    learnButton.interactable = true;
    ColorBlock colors = learnButton.colors;
    colors.normalColor = new Color32(255, 255, 255, 255);
    colors.highlightedColor = new Color32(255, 255, 255, 255);
    colors.pressedColor = new Color32(255, 255, 255, 255);
    learnButton.colors = colors;
}

private void ShowToolTip(GameObject buttonClicked)
{
    this.buttonClicked = buttonClicked;
    costTolearn = buttonClicked.GetComponent<GeneralTechTree>().costToLearn;
    gameObject.SetActive(true);
    try
    {
        string buttonName = buttonClicked.name;
        if (buttonName.Contains("Clone"))
        {
            buttonName = buttonName.Substring(0, buttonName.Length - 7);
        }
        StreamReader stream = new StreamReader(Application.streamingAssetsPath + "/Text/TechTreeText/" + buttonName + ".txt");
        TextMeshProUGUI titleText = gameObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bodyText = gameObject.transform.Find("BodyText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI details = gameObject.transform.Find("Details").GetComponent<TextMeshProUGUI>();
        titleText.text = stream.ReadLine();
        bodyText.text = stream.ReadLine();
        details.text = stream.ReadToEnd();
    }

    catch(Exception e)
    {
        Debug.Log("File not found");
    }
    string name = buttonClicked.name;
    name = name.Substring(name.Length - 7);
    //Debug.Log(name);

    if (SaveAndLoad.Get().GettechTreePoints() < costTolearn || SaveAndLoad.Get().GetskillsCollected().Contains(buttonClicked.name)
        || name == "(Clone)")
    {
        TurnLearnButtonGray();
    }
    else
    {
        TurnLearnButtonNormal();
    }


}
public static void Static_ShowToolTip(GameObject buttonClicked)
{
    techTreeToolTipScript.ShowToolTip(buttonClicked);
}
private void HideToolTip()
{
    gameObject.SetActive(false);
}
public static void Static_HideToolTip()
{
    techTreeToolTipScript.HideToolTip();
}

public void LearnButton()
{
    if (SaveAndLoad.Get().GettechTreePoints() >= costTolearn)
    {
        TurnLearnButtonGray();

        buttonClicked.GetComponent<GeneralTechTree>().HidePoints();
        SaveAndLoad.Get().UpdateTechTreePoints(-costTolearn);
        SaveAndLoad.Get().UpdateSkillsCollected(buttonClicked);

        SaveAndLoad.SaveData();
    }

}
public void CloseButton()
{
    mouseOverToolTip = false;
    TechTreeToolTipScript.Static_HideToolTip();
}



// Start is called before the first frame update
void Awake()
{
    techTreeToolTipScript = this;
    TechTreeToolTipScript.Static_HideToolTip();
}

void OnMouseOver()
{
    mouseOverToolTip = true;
    //Debug.Log("Over the tool tip");
}
void OnMouseExit()
{
    mouseOverToolTip = false;
    //Debug.Log("leaving the tool tip");
}


*/
