using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconControllerScript : MonoBehaviour
{
    public ListWithExpirations<BaseEffect> effects = new ListWithExpirations<BaseEffect>();
    List<GameObject> icons = new List<GameObject>();

    public float verticalOffSetPosition;

    public void ChangeEffect() //add all the effects from the three types into one. Sorts the list automatically
    {
        //Debug.Log( gameObject.transform.parent + " ChangeEffect Method in IconControllerScript");
        int numEffectsAtStart = effects.GetList().Count;


        effects.GetList().Clear();
        GameObject contactCollider = gameObject.transform.parent.Find("ContactCollider").gameObject;
        AllEffectsOnTroop allEffectsOnTroop = contactCollider.GetComponent<AllEffectsOnTroop>();
        //Debug.Log("$$$$$ " + generalEffect.effects.GetList().Count);
        DamageNew damageNew = contactCollider.GetComponent<DamageNew>();
        //Debug.Log("$$$$$ " + damageNew.damageEffects.GetList().Count);
        HealthNew healthNew = contactCollider.GetComponent<HealthNew>();

        for (int i = 0; i < allEffectsOnTroop.effects.GetList().Count; i++)
        {
            effects.AddEffect(allEffectsOnTroop.effects.GetList()[i]);
        }
        for (int i = 0; i < damageNew.damageEffects.GetList().Count; i++)
        {
            effects.AddEffect(damageNew.damageEffects.GetList()[i]);
        }
        for (int i = 0; i < healthNew.protections.GetList().Count; i++)
        {
            effects.AddEffect(healthNew.protections.GetList()[i]);
        }

        //Debug.Log("****************** effects.GetList().Count = " + effects.GetList().Count + " effects list " + effects.GetList());
        DisplayEffects();
        GameObject toolTip = GameObject.Find("ToolTip");
        if(toolTip != null && toolTip.activeSelf == true)
        {
            if(gameObject.transform.parent != null && gameObject.transform.parent.Find("GroundCollider") != null)
            {
                if(toolTip.GetComponent<ToolTipNew>() != null)
                {
                    toolTip.GetComponent<ToolTipNew>().EffectsHaveChanged(gameObject.transform.parent.Find("GroundCollider").gameObject);
                }
            } 
        }

        int numEffectsAtEnd = effects.GetList().Count;
        if(numEffectsAtEnd - numEffectsAtStart > 0)
        {
            //new effect was added - do animation
            GameObject sparkleEffectPrefab =  Resources.Load("Prefabs/General/EffectSpawnGlow") as GameObject;

            GameObject sparkle = Instantiate(sparkleEffectPrefab, gameObject.transform.position, Quaternion.identity);
            sparkle.transform.parent = gameObject.transform;
            sparkle.transform.localScale = new Vector3(sparkle.transform.localScale.x * gameObject.transform.parent.localScale.x, 
                                sparkle.transform.localScale.y * gameObject.transform.parent.localScale.x, 0);
        }

    }
    

    public void DisplayEffects()
    {
        //Debug.Log("Vertical OffSet position "  + verticalOffSetPosition);
        for (int i = 0; i < icons.Count; i++)
        {
            if(icons[i] != null)
            {
                Destroy(icons[i]);
            }
        }
        icons.Clear();

        BoxCollider2D boxCollider = gameObject.transform.parent.Find("ContactCollider").GetComponent<BoxCollider2D>();
        float verticalOffset = 0;
        if(verticalOffSetPosition == 0)
        {
            verticalOffSetPosition = 0.2f;
        }
        if (boxCollider != null)
        {
            verticalOffset = boxCollider.bounds.size.y / 2 + verticalOffSetPosition;
        }
        
        for(int i = 0; i < effects.GetList().Count; i++)
        {
            //Debug.Log(effects.GetList()[i].GetName());
            GameObject effect = Resources.Load("Prefabs/Effects/" + effects.GetList()[i].GetName()) as GameObject;
            Vector3 spawnPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + verticalOffset, 0);
            //Debug.Log("gameObject.transform.position.y = " + gameObject.transform.position.y);
            GameObject icon = Instantiate(effect, spawnPoint, Quaternion.identity);
            icon.transform.parent = gameObject.transform;
            double time = (effects.GetList()[i].GetDuration().Ticks - TimeManagement.CurrentTime().Ticks) / 10000000;
            icon.GetComponent<IconScript>().SetTimer(time);
            
            icons.Add(icon);
            //Debug.Log("verticalOffset = " + verticalOffset);
            verticalOffset += 0.25f;
        }
    }

    public List<GameObject> GetEffectIcons()
    {
        return icons;
    }











    // Start is called before the first frame update
    void Start()
    {
        ChangeEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
