using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private static ToolTip toolTip;

    private Text toolTipText;
    private RectTransform background;
    public Button buttonPrefab;
    public List<Button> buttons = new List<Button>();
    public List<GameObject> images = new List<GameObject>();

    


    public class RowData
    {
        public GameObject troop;
        public string name;
        public bool dying = false;
        public int dyingCountDown = 50;
        public List<GameObject> effects = new List<GameObject>();

        public RowData(GameObject troop)
        {
            this.troop = troop;
            this.name = troop.transform.parent.name;
            effects = troop.transform.parent.Find("IconController").GetComponent<IconControllerScript>().GetEffectIcons();
            
            if(name.ToLower().Contains("clone"))
            {
                name = name.Substring(0, name.Length - 7);
                name = name.Replace("_", " ");
            }
            
        }
    }

    public void ShowToolTip(List<ToolTip.RowData> rowDataList)
    {
        gameObject.SetActive(true);

        //float maxWidth = 0;
        //float maxHeight = 0;
        /*

        foreach(Button button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();

        foreach(GameObject image in images)
        {
            Destroy(image.gameObject);
        }
        images.Clear();


        
        TextMeshProUGUI prefabText = buttonPrefab.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

        
        maxWidth = 300;
        maxHeight += 30 * rowDataList.Count;

        Vector2 backgroundSize = new Vector2(maxWidth, maxHeight);
        background.sizeDelta = backgroundSize;
        */

        float posy = (background.rect.height / 2);
        int buttonNumber = 0;
        foreach (RowData rowData in rowDataList)
        {
            GameObject toolTipRow = Instantiate(Resources.Load("Prefabs/General/ToolTipRow") as GameObject, 
                                        transform.position, Quaternion.identity, transform.Find("Background"));
            
            






            /*
            List<BaseEffect> baseEffects = new List<BaseEffect>();

            HealthNew health = rowData.troop.gameObject.GetComponent<HealthNew>();
            DamageNew damage = rowData.troop.gameObject.GetComponent<DamageNew>();
            GeneralEffect generalEffect = rowData.troop.gameObject.GetComponent<GeneralEffect>();
            if (health != null)
            {
                foreach(HealthNew.Protection protection in health.protections.GetList())
                {
                    baseEffects.Add(protection);
                }
                
            }
            if(damage != null)
            {
                foreach (DamageNew.DamageEffect damageEffect in damage.damageEffects.GetList())
                {
                    baseEffects.Add(damageEffect);
                }
            }
            if (generalEffect != null)
            {
                foreach( GeneralEffect.Effect effect in generalEffect.effects.GetList())
                {
                    baseEffects.Add(effect);
                }
            }


            //Debug.Log(background.rect.width);
            

            Button button = Instantiate(buttonPrefab, new Vector3(0,0,0), Quaternion.identity, gameObject.transform.Find("Background"));
            buttons.Add(button);
            ToolTipButton toolTipButton = button.GetComponent<ToolTipButton>();
            toolTipButton.buttonNumber = buttonNumber++;

            //button.transform.position = pos;
            TextMeshProUGUI buttonText = button.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

            Vector3 pos = new Vector3(transform.Find("Background").position.x, posy = (posy - (buttonText.preferredHeight)), 0);
            button.transform.localPosition = pos;

            float posx = 120;
            
            
            foreach (BaseEffect baseEffect in baseEffects)
            {
                //To do - change this to new icons


                
                Vector3 imagePos = new Vector3(posx, transform.position.y, 0);
                GameObject image = Instantiate(baseEffect.GetImage(), new Vector3(0,0,0), Quaternion.identity, gameObject.transform.Find("Background"));
                images.Add(image);
                
                image.transform.localPosition = imagePos;
                
                image.transform.localScale = new Vector3(20f, 20f, 0);
                posx += 20;
                
            }
            


            buttonText.text = rowData.name;




            //Debug.Log(buttonText.preferredHeight);
            
            
            posy -= buttonText.preferredHeight;
            */
        }
        


        
        
        
        
        
    }



    public void ButtonClickCallBack(string arg)
    {



    }




    public void HideToolTip()
    {
        
        gameObject.SetActive(false);



    }

    public static void ShowToolTip_Static(List<ToolTip.RowData> rowDataList)
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
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    




    // Start is called before the first frame update
    void Awake()
    {
        toolTip = this;
        background = transform.Find("Background").GetComponent<RectTransform>();
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

        for (int i = 0; i <= emptyBackgroundScript.rowDataList.Count - 1; i++)
        {
            if (emptyBackgroundScript.rowDataList[i].troopGroundCollider.gameObject == null)
            {
                emptyBackgroundScript.rowDataList[i].dying = true;
                emptyBackgroundScript.rowDataList[i].dyingCountDown -= 1;
                TextMeshProUGUI buttonText = buttons[i].transform.Find("Text").GetComponent<TextMeshProUGUI>();
                buttonText.color = Color.red;


                if(emptyBackgroundScript.rowDataList[i].dyingCountDown <= 0)
                {
                    Debug.Log("Change ToolTip");
                    emptyBackgroundScript.rowDataList.Remove(emptyBackgroundScript.rowDataList[i]);
                    //ShowToolTip_Static(emptyBackgroundScript.rowDataList);

                }

            }

        }
    }

}
