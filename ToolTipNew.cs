using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipNew : MonoBehaviour
{
    private static ToolTipNew toolTip;

    private Text toolTipText;
    private RectTransform background;
    //public Button buttonPrefab;
    public Dictionary<GameObject, ToolTipRowData> toolTipRows = new Dictionary<GameObject, ToolTipRowData>();  //troopGroundCollider to toolTipRow
    public HashSet<GameObject> groundCollidersInList = new HashSet<GameObject>();



    public class RowDataNew
    {
        public GameObject troopGroundCollider;
        public string name;
        public bool dying = false;
        public int dyingCountDown = 50;
        public List<GameObject> effects = new List<GameObject>();
        //public List<GameObject> icons = new List<GameObject>();

        public RowDataNew(GameObject troop)
        {
            this.troopGroundCollider = troop;
            this.name = troop.transform.parent.name;
            if(troop.transform.parent.Find("IconController") == null)
            {
                return;
            }
            effects = troop.transform.parent.Find("IconController").GetComponent<IconControllerScript>().GetEffectIcons();

            if (name.ToLower().Contains("clone"))
            {
                name = name.Substring(0, name.Length - 7);
                name = name.Replace("_", " ");
            }

        }
    }

    public class ToolTipRowData
    {
        public GameObject toolTipRow;
        public bool dying = false;
        public int dyingCountDown = 50;

        public ToolTipRowData(GameObject toolTipRow)
        {
            this.toolTipRow = toolTipRow;
        }
    }

    public void ShowToolTip(List<ToolTipNew.RowDataNew> rowDataList)
    {
        gameObject.SetActive(true);

        //float posy = (background.rect.height / 2);
        //int buttonNumber = 0;
        foreach (RowDataNew rowData in rowDataList)
        {
            if (groundCollidersInList.Contains(rowData.troopGroundCollider))
            {
                continue;
            }

            groundCollidersInList.Add(rowData.troopGroundCollider);

            GameObject toolTipRow = Instantiate(Resources.Load("Prefabs/General/ToolTipRow") as GameObject,
                                        transform.position, Quaternion.identity, transform.Find("ScrollView").Find("Background"));
            toolTipRows[rowData.troopGroundCollider] = new ToolTipRowData(toolTipRow);

            toolTipRow.transform.Find("Button").GetComponent<ToolTipButton>().troopOnToolTipRow = rowData.troopGroundCollider.transform.parent.gameObject;


            TextMeshProUGUI textMeshPro = toolTipRow.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            textMeshPro.text = rowData.name;
            TextMeshProUGUI healthText = toolTipRow.transform.Find("Health").GetComponent<TextMeshProUGUI>();
            UpdateHealthText(healthText, rowData.troopGroundCollider);

            UpdateEffects(toolTipRow, rowData.troopGroundCollider);



        }

    }

    public void DamageUpdate(GameObject troopContactCollider)
    {
        GameObject groundCollider = troopContactCollider.transform.parent.Find("GroundCollider").gameObject;
        if (groundCollidersInList.Contains(groundCollider))
        {
            GameObject toolTipRow = toolTipRows[groundCollider].toolTipRow;
            UpdateHealthText(toolTipRow.transform.Find("Health").GetComponent<TextMeshProUGUI>(), groundCollider);
        }
    }

    private void UpdateHealthText(TextMeshProUGUI healthText, GameObject groundCollider)
    {
        healthText.text = groundCollider.transform.parent.Find("ContactCollider").GetComponent<HealthNew>().health + "/"
                + groundCollider.transform.parent.Find("ContactCollider").GetComponent<HealthNew>().GetTotalHealth();
    }

    private void UpdateEffects(GameObject toolTipRow, GameObject groundCollider)
    {
        for(int i = 0; i < 6; i++)
        {
            if(toolTipRow.transform.Find("IconContainer").Find("Image" + i).childCount > 0)
            {
                Transform iconTransform = toolTipRow.transform.Find("IconContainer").Find("Image" + i).GetChild(0);
                if (iconTransform != null)
                {
                    iconTransform.GetComponent<IconScript>().stop = true;
                    Destroy(iconTransform.gameObject);
                }
            }
        }
        
        
        List<GameObject> effects = groundCollider.transform.parent.Find("IconController").GetComponent<IconControllerScript>().GetEffectIcons();

        for (int i = 0; i < effects.Count; i++)
        {
            GameObject imageInToolTip = toolTipRow.transform.Find("IconContainer").Find("Image" + i).gameObject;
            GameObject icon = Instantiate(effects[i], imageInToolTip.transform.position, Quaternion.identity, imageInToolTip.transform);
            //rowData.icons.Add(icon);

            icon.GetComponent<IconScript>().SetTimer(effects[i].GetComponent<IconScript>().timer);

            icon.name = icon.name.Substring(0, icon.name.Length - 7);
            icon.transform.localScale = new Vector3(100, 100, 0);
            MeshRenderer meshRenderer = icon.transform.Find("Canvas").Find("Name").GetComponent<MeshRenderer>();
            meshRenderer.sortingOrder = 10;
            icon.transform.Find("Canvas").Find("Name").transform.position += new Vector3(-0.4f, 0.20f, 0);
        }
            

        
    }

    public void EffectsHaveChanged(GameObject groundCollider)
    {
        if (toolTipRows.ContainsKey(groundCollider))
        {
            GameObject toolTipRow = toolTipRows[groundCollider].toolTipRow;
            if (toolTipRow != null)
            {
                UpdateEffects(toolTipRow, groundCollider);
            }
        }
        
    }


    public void ButtonClickCallBack(string arg)
    {

    }

    
    public void HideToolTip()
    {
        foreach (KeyValuePair<GameObject, ToolTipRowData> keyValuePair in toolTipRows)
        {
            Destroy(keyValuePair.Value.toolTipRow);
        }
        toolTipRows.Clear();
        groundCollidersInList.Clear();
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(List<ToolTipNew.RowDataNew> rowDataList)
    {
        toolTip.ShowToolTip(rowDataList);
    }

    public static void HideToolTip_Static()
    {
        toolTip.HideToolTip();
    }


    public void AddRow(GameObject troop)
    {

    }



    void OnMouseDown()
    {
        Debug.Log("ToolTip" + Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }






    // Start is called before the first frame update
    void Awake()
    {
        toolTip = this;
        //background = transform.Find("Background").GetComponent<RectTransform>();
    }
    void Start()
    {
        HideToolTip();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        EmptyBackgroundScript emptyBackgroundScript = GameObject.Find("EmptyBackground").GetComponent<EmptyBackgroundScript>();

        DateTime changeToolTip = TimeManagement.CurrentTime();
        TimeSpan timeBetweenChanges = new TimeSpan(0, 0, 1);
        bool toolTipIsEmpty = false;
        foreach (KeyValuePair<GameObject, ToolTipRowData> keyValuePair in toolTipRows)
        {
            if(keyValuePair.Key.gameObject == null)
            {
                

                keyValuePair.Value.dying = true;
                keyValuePair.Value.dyingCountDown -= 1;
                if (keyValuePair.Value.toolTipRow != null)
                {
                    keyValuePair.Value.toolTipRow.GetComponent<Image>().color = new Color(188, 0, 0, 255);
                }  
                if (keyValuePair.Value.dyingCountDown <= 0)
                {
                    Debug.Log("Change ToolTip");
                    Destroy(keyValuePair.Value.toolTipRow);
                    if(toolTipRows.Count <= 1)
                    {
                        toolTipIsEmpty = true;
                    }
                    //ShowToolTip_Static(emptyBackgroundScript.rowDataList);
                }
            }
        }
        if(toolTipIsEmpty == true)
        {
            HideToolTip();
        }


        /*
            for (int i = 0; i <= emptyBackgroundScript.rowDataList.Count - 1; i++)
        {
            if (emptyBackgroundScript.rowDataList[i].troopGroundCollider.gameObject == null)
            {
                emptyBackgroundScript.rowDataList[i].dying = true;
                emptyBackgroundScript.rowDataList[i].dyingCountDown -= 1;
                TextMeshProUGUI buttonText = ;
                    //buttons[i].transform.Find("Text").GetComponent<TextMeshProUGUI>();
                buttonText.color = Color.red;

                if (emptyBackgroundScript.rowDataList[i].dyingCountDown <= 0)
                {
                    Debug.Log("Change ToolTip");
                    emptyBackgroundScript.rowDataList.Remove(emptyBackgroundScript.rowDataList[i]);
                    ShowToolTip_Static(emptyBackgroundScript.rowDataList);
                }
            }

        }
        */
    }
}
