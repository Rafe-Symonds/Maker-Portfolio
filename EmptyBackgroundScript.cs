using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EmptyBackgroundScript : MonoBehaviour
{
    public AudioSource ambientSound0;
    public int ambientSound0IntervalMillis;
    private DateTime nextAmbientSound0;

    public AudioSource ambientSound1;
    public int ambientSound1IntervalMillis;
    private DateTime nextAmbientSound1;

    public AudioSource ambientSound2;
    public int ambientSound2IntervalMillis;
    private DateTime nextAmbientSound2;
    
    public AudioSource ambientSound3;
    public int ambientSound3IntervalMillis;
    private DateTime nextAmbientSound3;


    public enum OverridePosition { Normalxy, Top, Bottom, Left, Right, Ground};
    private OverridePosition overridePosition;

    public Action<Vector3> CallBackFunction = null;
    public Action<GameObject> CallBackFunctionOnTroop = null;
    //private Action ClearHotbarGlow = null;
    //private Action rightClickClearHotbarGlow = null;
    private bool twoClicks = false;

    private bool mouseOverToolTip = false;

    public List<ToolTipNew.RowDataNew> rowDataList = null;

    private GameObject currentHotBar = null;
    
    public void OnMouseDown()
    {
        //Debug.Log("Click on the emptyBackground");
        
        
        if (mouseOverToolTip)
        {
            return;
        }



        if (CallBackFunctionOnTroop == null)
        {
            ToolTipNew.HideToolTip_Static();
        }

        //Debug.Log("Mouse pos = " + Input.mousePosition);
        //Debug.Log(Camera.main);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        //0 is needed as it will move in 3D
        pos.z = 0;
        //Debug.Log("converted pos =" + pos);
        if (CallBackFunction != null)
        {
            if( overridePosition == OverridePosition.Top)
            {
                pos.y = 6;
            }
            else if(overridePosition == OverridePosition.Bottom)
            {
                pos.y = -6;
            }
            else if(overridePosition == OverridePosition.Left)
            {
                pos.x = -10;
            }
            else if(overridePosition == OverridePosition.Right)
            {
                pos.x = 10;
            }
            else if(overridePosition == OverridePosition.Ground)
            {
                pos = RayCastToGround(pos);
            }
            //Debug.Log("Spawning");
            CallBackFunction(pos);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, .01f);
        }
    }
    public static Vector3 RayCastToGround(Vector3 pos)
    {
        bool found = false;
        RaycastHit2D[] hitsUp = Physics2D.RaycastAll(pos, Vector2.up, 10);
        //Debug.DrawRay(pos, Vector3.up, UnityEngine.Color.green, 10);
        for (int i = 0; i <= hitsUp.Length - 1; i++)
        {
            //Debug.Log(hit[i].collider.name);
            if (hitsUp[i].collider != null && hitsUp[i].collider.name == "Ground")
            {
                Vector2 point = hitsUp[i].point;
                pos.y = point.y;
                found = true;
                break;
            }

        }

        if (found == false)
        {
            RaycastHit2D[] hitsDown = Physics2D.RaycastAll(pos, Vector2.down, 10);
            //Debug.DrawRay(pos, Vector3.down, UnityEngine.Color.red, 10);
            for (int i = 0; i <= hitsDown.Length - 1; i++)
            {
                //Debug.Log(hit[i].collider.name);
                if (hitsDown[i].collider != null && hitsDown[i].collider.name == "Ground")
                {
                    Vector2 point = hitsDown[i].point;
                    pos.y = point.y;
                    break;
                }
            }
        }
        //Debug.Log(found);
        return pos;
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


    public void SetCallBack(Action<Vector3> CallBack, OverridePosition overridePosition, bool twoClicks, GameObject newHotBar)
    {
        if (twoClicks == false)
        {
            this.overridePosition = overridePosition;
            CallBackFunction = CallBack;
        }
        else if(twoClicks == true)
        {
            this.overridePosition = overridePosition;
            this.twoClicks = twoClicks;
            CallBackFunction = CallBack;
        }

        
        if (currentHotBar != null)
        {
            GeneralSpawnNew generalSpawn = currentHotBar.GetComponent<GeneralSpawnNew>();
            generalSpawn.CancelSpell();
        }
        else if(CallBackFunctionOnTroop != null)
        {
            GeneralSpawnNew generalSpawn = currentHotBar.GetComponent<GeneralSpawnNew>();
            generalSpawn.CancelSpell();
            CallBackFunctionOnTroop = null;
            currentHotBar = null;
        }
        currentHotBar = newHotBar;
    }

    public void ClearCallBack()
    {
        CallBackFunction = null;
    }

    public void ClickOnTroop(Vector3 pos, GameObject troop)
    {
        //A spell can need a troop for an effect  If there is one troop do the spell
        //If there is multiple troops in the same spot use a tooltip to select troop
        //If there is not a spell in progress then always pop up tooltip

        //Debug.Log("Click On Troop");
        //ToolTip.ShowToolTip_Static("Some text"); 


        //Physics2D.queriesHitTriggers = false;
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero);

        //Debug.Log(hits);
        //Debug.Log(pos);
        //Includes background
        //Debug.Log(hits.Length);

        //This code below is to make sure only one of each troop is in the list. RayCast may hit ground and contact collider.
        HashSet<GameObject> parents = new HashSet<GameObject>();
        List<RaycastHit2D> uniqueHits = new List<RaycastHit2D>();
        for (int i = 0; i < hits.Length; i++) 
        {
            if (hits[i].collider.gameObject.name == "ContactCollider" && hits[i].collider.gameObject.transform.parent.name != "treasureChest")
            {
                if (!parents.Contains(hits[i].collider.gameObject.transform.parent.gameObject))
                {
                    parents.Add(hits[i].collider.gameObject.transform.parent.gameObject);
                    uniqueHits.Add(hits[i]);
                }
            }
        }



        if(uniqueHits.Count == 1 && CallBackFunctionOnTroop != null)
        {
            //Debug.Log("About to call callback on troop");
            CallBackFunctionOnTroop(troop); //Only time a tooltip is not created
            return;
        }

        
        rowDataList = new List<ToolTipNew.RowDataNew>();

        for(int i = 0; i < uniqueHits.Count; i++)
        {
            //Debug.Log(uniqueHits[i].collider.gameObject);
            if(uniqueHits[i].collider.gameObject.transform.parent.Find("IconController") != null)
            {
                rowDataList.Add(new ToolTipNew.RowDataNew(uniqueHits[i].collider.gameObject.transform.parent.Find("GroundCollider").gameObject));
            }
            
            
            
        }
        if(rowDataList.Count > 0)
        {
            ToolTipNew.ShowToolTip_Static(rowDataList);
        }
    }

    public void SelectTroopFromToolTip(GameObject troop)
    {
        if(CallBackFunctionOnTroop != null)
        {
            CallBackFunctionOnTroop(troop);
        }
    }

    public void SetCallBackTroop(Action<GameObject> CallBack, GameObject newHotBar)
    {
        CallBackFunctionOnTroop = CallBack;
        if (currentHotBar != null)
        {
            GeneralSpawnNew generalSpawn = currentHotBar.GetComponent<GeneralSpawnNew>();
            generalSpawn.CancelSpell();
            CallBackFunction = null;
        }
        currentHotBar = newHotBar;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, .01f);
    }

    public void ClearCallBackTroop()
    {
        CallBackFunctionOnTroop = null;
    }


    // Start is called before the first frame update
    void Start()
    {
        nextAmbientSound0 = DateTime.Now;
        nextAmbientSound1 = DateTime.Now;
        nextAmbientSound2 = DateTime.Now;
        nextAmbientSound3 = DateTime.Now;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsMouseOverUI())
        {
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -5 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 5)
            {
                mouseOverToolTip = true;
            }
            else
            {
                mouseOverToolTip = false;
            }
            //Debug.Log("Mouse is not over");
        }
        else
        {
            mouseOverToolTip = false;
        }


        if (Input.GetMouseButtonDown(1))  //This is checking for right click
        {
            Debug.Log("Right Click - " + currentHotBar);
            //moves emptyBackground back when undo spell with right click
            if (currentHotBar != null)
            {
                GeneralSpawnNew generalSpawn = currentHotBar.GetComponent<GeneralSpawnNew>();
                generalSpawn.CancelSpell();
            }
            currentHotBar = null;

            CallBackFunction = null;
            CallBackFunctionOnTroop = null;
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0.1f);
        }
        
        if(ambientSound0.clip != null && nextAmbientSound0 < DateTime.Now)
        {
            int random = UnityEngine.Random.Range((int)(-0.1 * ambientSound0IntervalMillis), (int)(0.1f * ambientSound0IntervalMillis));
            nextAmbientSound0 = DateTime.Now + new TimeSpan(0, 0, 0, 0, ambientSound0IntervalMillis + random);
            ambientSound0.PlayOneShot(ambientSound0.clip);
        }
        if (ambientSound1.clip != null && nextAmbientSound0 < DateTime.Now)
        {
            int random = UnityEngine.Random.Range((int)(-0.1 * ambientSound1IntervalMillis), (int)(0.1f * ambientSound1IntervalMillis));
            nextAmbientSound1 = DateTime.Now + new TimeSpan(0, 0, 0, 0, ambientSound1IntervalMillis + random);
            ambientSound1.PlayOneShot(ambientSound1.clip);
        }
        if (ambientSound2.clip != null && nextAmbientSound2 < DateTime.Now)
        {
            int random = UnityEngine.Random.Range((int)(-0.1 * ambientSound2IntervalMillis), (int)(0.1f * ambientSound2IntervalMillis));
            nextAmbientSound2 = DateTime.Now + new TimeSpan(0, 0, 0, 0, ambientSound2IntervalMillis + random);
            ambientSound2.PlayOneShot(ambientSound2.clip);
        }
        if (ambientSound3.clip != null && nextAmbientSound3 < DateTime.Now)
        {
            int random = UnityEngine.Random.Range((int)(-0.1 * ambientSound3IntervalMillis), (int)(0.1f * ambientSound3IntervalMillis));
            nextAmbientSound3 = DateTime.Now + new TimeSpan(0, 0, 0, 0, ambientSound3IntervalMillis + random);
            ambientSound3.PlayOneShot(ambientSound3.clip);
        }
        
    }
}
