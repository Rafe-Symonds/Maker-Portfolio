using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect
{
    protected DateTime expirationTime;
    protected string name = "ICON NAME NOT SET *****";
    public static int CompareProtections(BaseEffect a, BaseEffect b)
    {
        if (a == null)
        {
            if (b == null)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (b == null)
            {
                return 1;
            }
            else
            {
                if (a.expirationTime < b.expirationTime)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

    }//Same code as the GeneralMovement script

    public DateTime GetDuration()
    {
        return expirationTime;
    }
    public string GetName()
    {
        return name;
    }
    public virtual void OnEnd()
    {

    }

    //public abstract GameObject GetImage();




}
public class ListWithExpirations<T> where T: BaseEffect
{
    
    private List<T> effects = new List<T>();
    

    public List<T> GetList()
    {
        return effects;
    }

    public bool CleanUp()
    {
        if (effects.Count > 0)
        {
            if (effects[0].GetDuration() < TimeManagement.CurrentTime())
            {
                effects[0].OnEnd();
                effects.RemoveAt(0);
                return true;
                
            }
        }
        return false;
    }
    public void AddEffect(T effect)
    {
        effects.Add(effect);
        effects.Sort(BaseEffect.CompareProtections);
    }











}
