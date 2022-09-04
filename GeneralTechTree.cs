using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralTechTree : MonoBehaviour
{
    /*

        This script is on each skill on the tech tree that can be dragged and their duplicates (being dragged and the one on the hotbar


    */

    public List<GameObject> parentSkills = new List<GameObject>();
    public List<GameObject> childrenSkills = new List<GameObject>();
    public Material learned;
    public Material parentsNotLearned;
    public Material parentsLearned;
    private GameObject scroller;
    private GameObject techTreePrefab;
    private GameObject spellOrTroopPrefab;
    //private Vector3 endPosition;
    public static GameObject skillBeingDragged = null;
    private static GameObject button = null;
    // mappedButtons is the array of prefab skills that will be mapped to game hotbar
    //public static GameObject[] mappedButtons = null;
    //public static GameObject[] mappedButtonsImages = null;

    private ScrollRect scroll;
    public GameObject copyOfSkillOnButton = null;
    private GameObject draggedSkill = null;
    private Vector3 pos;

    public int costToLearn;
    private bool abillityIsTroop;


    private string troopName;

    public void OnMouseDrag()
    {
        if(TechTreeToolTipScript.mouseOverToolTip == true || OverImageBoundry.overImage == true)
        {
            return;
        }
        List<string> skillsCollected = SaveAndLoad.Get().GetskillsCollected();
        if(skillsCollected.Contains(gameObject.name))
        {
            //scroll.horizontal = false;
            scroll.vertical = false;
            //UnityEngine.Debug.Log("Dragging");
            skillBeingDragged = gameObject;
            
            if (draggedSkill == null) //If you are not dragging and start dragging
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 1;
                draggedSkill = Instantiate(techTreePrefab, pos, Quaternion.identity, GameObject.Find("Canvas").transform);
                GameObject draggedSkillText = draggedSkill.transform.Find("CostText").gameObject;
                Debug.Log("setting dragged skill to gold");
                draggedSkill.GetComponent<MeshRenderer>().material = learned;
                Transform techTreeArrow = draggedSkill.transform.Find("TechTreeArrow");
                if(techTreeArrow != null)
                {
                    Destroy(techTreeArrow.gameObject);
                }
                Destroy(draggedSkillText);
            }
        }
    }

    public void OnMouseUp()
    {
        Destroy(draggedSkill);
        if (skillBeingDragged != null && button != null)
        {
            int index = (int)button.name[6] - '0';
            
            
            //Clearing out the prefab so there are not multiples of the same one
            for (int i = 0; i <= SaveAndLoad.Get().GetHotbarSkills().Length - 1; i++)
            {
                if (SaveAndLoad.Get().GetHotbarSkills()[i] != null)
                {
                    //Checking if any button has the dragged skill already
                    if (SaveAndLoad.Get().GetHotbarSkillImages()[i]  == techTreePrefab.name)
                    {
                        Destroy(copyOfSkillOnButton);
                        SaveAndLoad.Get().UpdateHotbarSkillImages(i, null);
                        SaveAndLoad.Get().UpdateHotbarSkills(i, null);
                    }
                }
            }
            copyOfSkillOnButton = Instantiate(techTreePrefab, button.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            Transform techTreeArrow = copyOfSkillOnButton.transform.Find("TechTreeArrow");
            if (techTreeArrow != null)
            {
                Destroy(techTreeArrow.gameObject);
            }
            //Changing the rendering order so the hotbar images are not behind the background
            GameObject spriteImage = copyOfSkillOnButton.transform.Find("Sprite").gameObject;
            spriteImage.GetComponent<SpriteRenderer>().sortingOrder = 2; 


            copyOfSkillOnButton.transform.SetParent(button.transform);
            Destroy(copyOfSkillOnButton.GetComponent<GeneralTechTree>());
            Destroy(copyOfSkillOnButton.GetComponent<MeshRenderer>());  // These destroy lines are to make the image on the hotbar in the tech tree do what it is supposed to       
            
            if (SaveAndLoad.Get().GetHotbarSkills()[index] != null)
            {
                Destroy(button.transform.GetChild(0).gameObject);
            }

            //Debug.Log(techTreePrefab.name);

            SaveAndLoad.Get().UpdateHotbarSkillImages(index, techTreePrefab.name);
            string troopName = gameObject.name;
            troopName = troopName.Remove(troopName.Length - 6);
            //Debug.Log(troopName);
            spellOrTroopPrefab = Resources.Load("Prefabs/TroopsNew/" + troopName) as GameObject;
            if (spellOrTroopPrefab == null)
            {
                spellOrTroopPrefab = Resources.Load("Prefabs/SpellsNew/" + troopName) as GameObject;
            }
            //Debug.Log(spellOrTroopPrefab);
            SaveAndLoad.Get().UpdateHotbarSkills(index, spellOrTroopPrefab.name);
            SaveAndLoad.SaveData();

            TechTreeEquipPointerScript.HideTechTreePointer();
        }
        skillBeingDragged = null;
        //scroll.horizontal = true;
        scroll.vertical = true;
    }

    public void OnMouseDown()
    {
        if (TechTreeToolTipScript.mouseOverToolTip == true || OverImageBoundry.overImage == true)
        {
            return;
        }
        TechTreeToolTipScript.Static_ShowToolTip(gameObject);
    }
    public static void MouseOverButton(GameObject buttonover)
    {
        button = buttonover;
    }

    public void HidePoints()
    {
        TextMeshPro costText = gameObject.transform.Find("CostText").GetComponent<TextMeshPro>();
        costText.enabled = false;
    }
    public bool ParentsHaveBeenLearned()
    {
        if(parentSkills.Count == 0)
        {
            return true;
        }
        List<string> skillsCollected = SaveAndLoad.Get().GetskillsCollected();
        int numParentsLearned = 0;
        foreach (GameObject parent in parentSkills)
        {
            if (skillsCollected.Contains(parent.name))
            {
                numParentsLearned++;
            }
        }
        if(parentSkills.Count == numParentsLearned)
        {
            return true;
        }
        return false;
    }
    public void ChangeMaterialToLearned()
    {
        gameObject.GetComponent<MeshRenderer>().material = learned;
        Transform techTreeArrow = gameObject.transform.Find("TechTreeArrow");
        if (techTreeArrow != null)
        {
            techTreeArrow.GetComponent<SpriteRenderer>().color = new Color(0.718f, 0.631f, 0.180f,1);
        }

    }
    public void ChangeMaterialToParentsLearned()
    {
        gameObject.GetComponent<MeshRenderer>().material = parentsLearned;
        Transform techTreeArrow = gameObject.transform.Find("TechTreeArrow");
        if (techTreeArrow != null)
        {
            techTreeArrow.GetComponent<SpriteRenderer>().color = new Color(0.753f, 0.753f, 0.753f, 1);
        }
    }
    public void ChangeMaterialToParentsNotLearned()
    {
        gameObject.GetComponent<MeshRenderer>().material = parentsNotLearned;
        Transform techTreeArrow = gameObject.transform.Find("TechTreeArrow");
        if (techTreeArrow != null)
        {
            techTreeArrow.GetComponent<SpriteRenderer>().color = new Color(0.690f, 0.553f, 0.341f, 1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        techTreePrefab = gameObject;
        //Debug.Log(gameObject);
        GameObject sprite = gameObject.transform.Find("Sprite").gameObject;
        SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();

        troopName = gameObject.name;
        if (troopName.Substring(troopName.Length-7) == "(Clone)")
        {
            troopName = troopName.Remove(troopName.Length - 13);
        }
        else
        {
            troopName = troopName.Remove(troopName.Length - 6);
        }

        SpriteRenderer troopSpriteRenderer = null;

        //Debug.Log(troopName);
        GameObject troopPrefab = Resources.Load("Prefabs/TroopsNew/" + troopName) as GameObject;
        abillityIsTroop = true;
        if (troopPrefab == null)
        {
            troopPrefab = Resources.Load("Prefabs/SpellsNew/" + troopName) as GameObject;
            abillityIsTroop = false;
        }
        troopSpriteRenderer = troopPrefab.GetComponent<SpriteRenderer>();
        
        
        if(troopSpriteRenderer != null)
        {
            if(spriteRenderer.sprite == null)
            {
                spriteRenderer.sprite = troopSpriteRenderer.sprite;
            }
        }
        else
        {
            ParticleSystem spellParticleSystem = troopPrefab.GetComponent<ParticleSystem>();
            if (spellParticleSystem != null)
            {
                if (spriteRenderer.sprite == null)
                {
                    spriteRenderer.sprite = spellParticleSystem.textureSheetAnimation.GetSprite(0);
                }
            }
        }
        
        //Debug.Log("GeneralTechTreeTest");
        scroller = GameObject.Find("Scroller");
        if(scroller != null)
        {
            scroll = scroller.GetComponent<ScrollRect>();
            //Debug.Log("Scroll checking " + scroll);
        }
        //Debug.Log(gameObject.name);

        Transform costText = gameObject.transform.Find("CostText");
        //Debug.Log(costText);
        if(costText != null)
        {
            //Debug.Log(SaveAndLoad.Get().GetskillsCollected());

            TextMeshPro costTextMesh = costText.GetComponent<TextMeshPro>();
            //Debug.Log(costTextMesh);
            costTextMesh.text = costToLearn.ToString();
            costText.GetComponent<MeshRenderer>().sortingOrder = -1;
        }
        if(costText != null && SaveAndLoad.Get().GetskillsCollected().Contains(gameObject.name))
        {
            TextMeshPro costTextMesh = costText.GetComponent<TextMeshPro>();

            costTextMesh.enabled = false;
        }
        

        if(SceneManager.GetActiveScene().name == "TechTree")
        {
            TextMeshPro goldOrManaCostText = gameObject.transform.Find("GoldOrManaCostText").GetComponent<TextMeshPro>();
            //Destroy(goldOrManaCostText);
        }
        else
        {
            TextMeshPro costTextForDeleting = gameObject.transform.Find("CostText").GetComponent<TextMeshPro>();
            costTextForDeleting.enabled = false;
        }
        
        if(SceneManager.GetActiveScene().name != "TechTree" && abillityIsTroop == true)
        {
            TextMeshPro goldOrManaCostText = gameObject.transform.Find("GoldOrManaCostText").GetComponent<TextMeshPro>();
            goldOrManaCostText.color = new Color(1, 0.71f, 0, 1);
            GameObject contactCollider = troopPrefab.transform.Find("ContactCollider").gameObject;
            GeneralMovementNew troopGeneralMovement = contactCollider.GetComponent<GeneralMovementNew>();
            goldOrManaCostText.text = troopGeneralMovement.moneyCost.ToString();
        }
        if(SceneManager.GetActiveScene().name != "TechTree" && abillityIsTroop == false)
        {
            TextMeshPro goldOrManaCostText = gameObject.transform.Find("GoldOrManaCostText").GetComponent<TextMeshPro>();
            goldOrManaCostText.color = new Color(0.01f, 0.64f, 0.28f, 1);
            GeneralSpellNew generalSpell = troopPrefab.GetComponent<GeneralSpellNew>();

            goldOrManaCostText.text = generalSpell.manaUsage.ToString();
        }


        //Code to fix the image sizes
        sprite.transform.localScale = new Vector3(1, 1, 0);
        if(spriteRenderer.sprite != null)
        {
            Vector2 spriteSize = spriteRenderer.sprite.rect.size;
            Vector3 backgroundSize = gameObject.GetComponent<BoxCollider>().bounds.size;
            float scale = backgroundSize.x / spriteSize.x * 40;
            if (SceneManager.GetActiveScene().name.Contains("BattleScene"))
            {
                scale /= 80; //This if statement is to resize the sprites on the battle scene because they are too large. This is because on the 
                              //tech tree the skills are in a canvas; therefore, they are 100 times too small. 
            }
            //Debug.Log(gameObject + " orginal sprite size " + sprite.transform.localScale);
            sprite.transform.localScale = sprite.transform.localScale * scale;
            //Debug.Log(gameObject + " sprite size " + spriteSize);
            //Debug.Log(gameObject + " background size " + backgroundSize);
        }
        string name = gameObject.name;
        if (name.Contains("Clone"))
        {
            name = name.Substring(0, name.Length - 7);
        }
        if (SaveAndLoad.Get().GetskillsCollected().Contains(name))
        {
            ChangeMaterialToLearned();
        }
        else if(ParentsHaveBeenLearned() == true)
        {
            //set button to gray
            ChangeMaterialToParentsLearned();
        }
        else
        {
            ChangeMaterialToParentsNotLearned();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (draggedSkill != null)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 1;
            if (draggedSkill.transform.position != pos)
            {
                draggedSkill.transform.position = pos;
            }
        }
    }
}
//old tech tree code
/*
 private GameObject scroller;
    private GameObject techTreePrefab;
    private GameObject spellOrTroopPrefab;
    //private Vector3 endPosition;
    public static GameObject skillBeingDragged = null;
    private static GameObject button = null;
    // mappedButtons is the array of prefab skills that will be mapped to game hotbar
    //public static GameObject[] mappedButtons = null;
    //public static GameObject[] mappedButtonsImages = null;

    private ScrollRect scroll;
    public GameObject copyOfSkillOnButton = null;
    private GameObject draggedSkill = null;
    private Vector3 pos;

    public int costToLearn;
    private bool abillityIsTroop;


    private string troopName;

    public void OnMouseDrag()
    {
        if(TechTreeToolTipScript.mouseOverToolTip == true)
        {
            return;
        }
        List<string> skillsCollected = SaveAndLoad.Get().GetskillsCollected();
        if(skillsCollected.Contains(gameObject.name))
        {
            //scroll.horizontal = false;
            scroll.vertical = false;
            //UnityEngine.Debug.Log("Dragging");
            skillBeingDragged = gameObject;

            if (draggedSkill == null) //If you are not dragging and start dragging
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 1;
                draggedSkill = Instantiate(techTreePrefab, pos, Quaternion.identity, GameObject.Find("Canvas").transform);
                GameObject draggedSkillText = draggedSkill.transform.Find("CostText").gameObject;
                Destroy(draggedSkillText);
            }
        }
    }

    public void OnMouseUp()
    {
        Destroy(draggedSkill);
        if (skillBeingDragged != null && button != null)
        {
            int index = (int)button.name[6] - '0';
            
            
            //Clearing out the prefab so there are not multiples of the same one
            for (int i = 0; i <= SaveAndLoad.Get().GetHotbarSkills().Length - 1; i++)
            {
                if (SaveAndLoad.Get().GetHotbarSkills()[i] != null)
                {
                    //Checking if any button has the dragged skill already
                    if (SaveAndLoad.Get().GetHotbarSkillImages()[i]  == techTreePrefab.name)
                    {
                        Destroy(copyOfSkillOnButton);
                        SaveAndLoad.Get().UpdateHotbarSkillImages(i, null);
                        SaveAndLoad.Get().UpdateHotbarSkills(i, null);
                    }
                }
            }
            copyOfSkillOnButton = Instantiate(techTreePrefab, button.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);

            //Changing the rendering order so the hotbar images are not behind the background
            GameObject spriteImage = copyOfSkillOnButton.transform.Find("Sprite").gameObject;
            spriteImage.GetComponent<SpriteRenderer>().sortingOrder = 2; 


            copyOfSkillOnButton.transform.SetParent(button.transform);
            Destroy(copyOfSkillOnButton.GetComponent<GeneralTechTree>());
            Destroy(copyOfSkillOnButton.GetComponent<MeshRenderer>());  // These destroy lines are to make the image on the hotbar in the tech tree do what it is supposed to       
            
            if (SaveAndLoad.Get().GetHotbarSkills()[index] != null)
            {
                Destroy(button.transform.GetChild(0).gameObject);
            }

            //Debug.Log(techTreePrefab.name);

            SaveAndLoad.Get().UpdateHotbarSkillImages(index, techTreePrefab.name);
            string troopName = gameObject.name;
            troopName = troopName.Remove(troopName.Length - 6);
            //Debug.Log(troopName);
            spellOrTroopPrefab = Resources.Load("Prefabs/TroopsNew/" + troopName) as GameObject;
            if (spellOrTroopPrefab == null)
            {
                spellOrTroopPrefab = Resources.Load("Prefabs/SpellsNew/" + troopName) as GameObject;
            }
            //Debug.Log(spellOrTroopPrefab);
            SaveAndLoad.Get().UpdateHotbarSkills(index, spellOrTroopPrefab.name);
            SaveAndLoad.SaveData();    
        }
        skillBeingDragged = null;
        //scroll.horizontal = true;
        scroll.vertical = true;
    }

    public void OnMouseDown()
    {
        if (TechTreeToolTipScript.mouseOverToolTip == true)
        {
            return;
        }
        TechTreeToolTipScript.Static_ShowToolTip(gameObject);
    }
    public static void MouseOverButton(GameObject buttonover)
    {
        button = buttonover;
    }

    public void HidePoints()
    {
        TextMeshPro costText = gameObject.transform.Find("CostText").GetComponent<TextMeshPro>();
        costText.text = "";

    }

    // Start is called before the first frame update
    void Start()
    {
        techTreePrefab = gameObject;
        //Debug.Log(gameObject);
        GameObject sprite = gameObject.transform.Find("Sprite").gameObject;
        SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();

        troopName = gameObject.name;
        if (troopName.Substring(troopName.Length-7) == "(Clone)")
        {
            troopName = troopName.Remove(troopName.Length - 13);
        }
        else
        {
            troopName = troopName.Remove(troopName.Length - 6);
        }

        SpriteRenderer troopSpriteRenderer = null;

        //Debug.Log(troopName);
        GameObject troopPrefab = Resources.Load("Prefabs/TroopsNew/" + troopName) as GameObject;
        abillityIsTroop = true;
        if (troopPrefab == null)
        {
            troopPrefab = Resources.Load("Prefabs/SpellsNew/" + troopName) as GameObject;
            abillityIsTroop = false;
        }
        troopSpriteRenderer = troopPrefab.GetComponent<SpriteRenderer>();
        
        
        if(troopSpriteRenderer != null)
        {
            if(spriteRenderer.sprite == null)
            {
                spriteRenderer.sprite = troopSpriteRenderer.sprite;
            }
        }
        else
        {
            ParticleSystem spellParticleSystem = troopPrefab.GetComponent<ParticleSystem>();
            if (spellParticleSystem != null)
            {
                if (spriteRenderer.sprite == null)
                {
                    spriteRenderer.sprite = spellParticleSystem.textureSheetAnimation.GetSprite(0);
                }
            }
        }
        
        //Debug.Log("GeneralTechTreeTest");
        scroller = GameObject.Find("Scroller");
        if(scroller != null)
        {
            scroll = scroller.GetComponent<ScrollRect>();
            //Debug.Log("Scroll checking " + scroll);
        }
        //Debug.Log(gameObject.name);

        Transform costText = gameObject.transform.Find("CostText");
        //Debug.Log(costText);
        if(costText != null && !SaveAndLoad.Get().GetskillsCollected().Contains(gameObject.name))
        {
            //Debug.Log(SaveAndLoad.Get().GetskillsCollected());

            TextMeshPro costTextMesh = costText.GetComponent<TextMeshPro>();
            //Debug.Log(costTextMesh);
            costTextMesh.text = costToLearn.ToString();
            costText.GetComponent<MeshRenderer>().sortingOrder = -1;
        }
        else if(costText != null)
        {
            TextMeshPro costTextMesh = costText.GetComponent<TextMeshPro>();

            costTextMesh.text = "";
        }
        

        if(SceneManager.GetActiveScene().name == "TechTree")
        {
            TextMeshPro goldOrManaCostText = gameObject.transform.Find("GoldOrManaCostText").GetComponent<TextMeshPro>();
            //Destroy(goldOrManaCostText);
        }
        else
        {
            TextMeshPro costTextForDeleting = gameObject.transform.Find("CostText").GetComponent<TextMeshPro>();
            Destroy(costTextForDeleting);
        }
        
        if(SceneManager.GetActiveScene().name != "TechTree" && abillityIsTroop == true)
        {
            TextMeshPro goldOrManaCostText = gameObject.transform.Find("GoldOrManaCostText").GetComponent<TextMeshPro>();
            goldOrManaCostText.color = new Color(1, 0.71f, 0, 1);
            GameObject contactCollider = troopPrefab.transform.Find("ContactCollider").gameObject;
            GeneralMovementNew troopGeneralMovement = contactCollider.GetComponent<GeneralMovementNew>();
            goldOrManaCostText.text = troopGeneralMovement.moneyCost.ToString();
        }
        if(SceneManager.GetActiveScene().name != "TechTree" && abillityIsTroop == false)
        {
            TextMeshPro goldOrManaCostText = gameObject.transform.Find("GoldOrManaCostText").GetComponent<TextMeshPro>();
            goldOrManaCostText.color = new Color(0.01f, 0.64f, 0.28f, 1);
            GeneralSpellNew generalSpell = troopPrefab.GetComponent<GeneralSpellNew>();

            goldOrManaCostText.text = generalSpell.manaUsage.ToString();
        }


        //Code to fix the image sizes
        sprite.transform.localScale = new Vector3(1, 1, 0);
        if(spriteRenderer.sprite != null)
        {
            Vector2 spriteSize = spriteRenderer.sprite.rect.size;
            Vector3 backgroundSize = gameObject.GetComponent<BoxCollider>().bounds.size;
            float scale = backgroundSize.x / spriteSize.x * 40;
            if (SceneManager.GetActiveScene().name.Contains("BattleScene"))
            {
                scale /= 100; //This if statement is to resize the sprites on the battle scene because they are too large. This is because on the 
                              //tech tree the skills are in a canvas; therefore, they are 100 times too small. 
            }
            //Debug.Log(gameObject + " orginal sprite size " + sprite.transform.localScale);
            sprite.transform.localScale = sprite.transform.localScale * scale;
            //Debug.Log(gameObject + " sprite size " + spriteSize);
            //Debug.Log(gameObject + " background size " + backgroundSize);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (draggedSkill != null)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 1;
            if (draggedSkill.transform.position != pos)
            {
                draggedSkill.transform.position = pos;
            }
        }
    }
 */
