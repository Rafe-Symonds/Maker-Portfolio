using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManagerScript : MonoBehaviour
{
    public GameObject lightning;
    public GameObject mainMenu;
    public List<GameObject> troops = new List<GameObject>();
    private List<GameObject> activeTroopsGoingRight = new List<GameObject>();
    private List<GameObject> activeTroopsGoingLeft = new List<GameObject>();
    private int lightningNumber = 0;
    private int updateCounter = 0;
    private bool spawnTroops = true;
    public void Lightning()
    {
        //Debug.Log("Lightning");
        int xPos = 0;
        if(lightningNumber == 0)
        {
            xPos = Random.Range(-650, -300);
        }
        else if (lightningNumber == 1)
        {
            xPos = Random.Range(300, 650);
        }
        GameObject lightningObject = Instantiate(lightning, new Vector3(xPos, 0, -1), Quaternion.identity);
        lightningObject.transform.localScale = new Vector3(300, 720, 0);
        lightningNumber++;
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void SpawnTroops()
    {
        int num = Random.Range(0, troops.Count);
        int spawnSide = Random.Range(0, 2);
        if(spawnSide == 0)
        {
            spawnSide = -1;
        }
        int yPos = Random.Range(-220, 380);
        GameObject troop = Instantiate(troops[num], new Vector3(spawnSide * 1000, yPos, 0), Quaternion.identity);
        Destroy(troop.GetComponent<TriggersAndCollidersNew>().spawnSound);
        Destroy(troop.GetComponent<TriggersAndCollidersNew>());
        Destroy(troop, 60);
        troop.transform.localScale = new Vector3(200, 200);
        if(spawnSide > 0)
        {
            activeTroopsGoingLeft.Add(troop);
            Rigidbody2D rigidbody2D = troop.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(-50, 0);
            rigidbody2D.angularDrag = 0;
            rigidbody2D.drag = 0;
            rigidbody2D.mass = 0;
            troop.GetComponent<SpriteRenderer>().flipX = !troop.GetComponent<SpriteRenderer>().flipX;
        }
        else
        {
            activeTroopsGoingRight.Add(troop);
            Rigidbody2D rigidbody2D = troop.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity  = new Vector2(50, 0);
            rigidbody2D.angularDrag = 0;
            rigidbody2D.drag = 0;
            rigidbody2D.mass = 0;
        }
        
        Destroy(troop.transform.Find("ContactCollider").gameObject);
        if(troop.transform.Find("RangedCollider") != null)
        {
            Destroy(troop.transform.Find("RangedCollider").gameObject);
        }
    }
    public void DestroyAllTroops()
    {
        spawnTroops = false;
        for(int i = 0; i < activeTroopsGoingRight.Count; i++)
        {
            if(activeTroopsGoingRight[i] != null)
            {
                GameObject troop = activeTroopsGoingRight[i];
                activeTroopsGoingRight.Remove(troop);
                Destroy(troop);
                i--;
            }
        }
        for (int i = 0; i < activeTroopsGoingLeft.Count; i++)
        {
            if (activeTroopsGoingLeft[i] != null)
            {
                GameObject troop = activeTroopsGoingLeft[i];
                activeTroopsGoingLeft.Remove(troop);
                Destroy(troop);
                i--;
            }
        }
        activeTroopsGoingLeft.Clear();
        activeTroopsGoingRight.Clear();
    }
    public void TurnOnTroopSpawns()
    {
        spawnTroops = true;
    }
    void Start()
    {

        Invoke("Lightning", 0.2f);
        Invoke("Lightning", 0.5f);
        Invoke("Lightning", 0.8f);
        Invoke("ShowMainMenu", 1.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTroops == true && updateCounter <= 0)
        {
            updateCounter = 225;
            SpawnTroops();
        }
        updateCounter--;
        foreach(GameObject troop in activeTroopsGoingLeft)
        {
            if(troop != null)
            {
                troop.GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 0);
            }
        }
        foreach (GameObject troop in activeTroopsGoingRight)
        {
            if (troop != null)
            {
                troop.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 0);
            }
        }
    }
}
