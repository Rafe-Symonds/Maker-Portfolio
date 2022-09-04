using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SaveAndLoad
{

    public static UnityEvent techTreePointsEvents = new UnityEvent();

    
    
    private static SaveAndLoad saveAndLoad = null;
    private Save[] saves = new Save[4];
    private int currentSave = 0;
    private float volume = 0;
    private int resolutionHeight = 0;
    private int resolutionWidth = 0;
    private bool fullScreen;

    [System.Serializable]
    public class Save
    {
        public int[] levelsCompleted = new int[40];
        //private bool[] itemsCollected = new bool[40];
        public List<string> skillsCollected = new List<string>();
        public int techTreePoints = 8;
        public string[] hotbarSkills = new string[10];
        public string[] hotbarSkillImages = new string[10];
        public int gameDifficulty = 1;  // 1 - easy   2 - medium   3 - hard
    }
    public float GetVolume()
    {
        return volume;
    }
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }
    public int GetResolutionHeight()
    {
        return resolutionHeight;
    }
    public int GetResolutionWidth()
    {
        return resolutionWidth;
    }
    public void SetResolution(Resolution newResolution)
    {
        resolutionHeight = newResolution.height;
        resolutionWidth = newResolution.width;
    }
    public bool GetFullScreen()
    {
        return fullScreen;
    }
    public void SetFullScreen(bool newFullScreen)
    {
        fullScreen = newFullScreen;
    }
    public void ChangeCurrentSave(int newSave)
    {
        currentSave = newSave;
        if (saveAndLoad.saves[saveAndLoad.currentSave] == null)
        {
            saveAndLoad.saves[saveAndLoad.currentSave] = new Save();
            for (int i = 0; i <= saveAndLoad.GetHotbarSkills().Length - 1; i++)
            {
                saveAndLoad.GetHotbarSkills()[i] = null;
                saveAndLoad.GetHotbarSkills()[i] = null;
            }

            for (int i = 0; i <= saveAndLoad.GetLevelsCompleted().Length - 1; i++)
            {
                saveAndLoad.UpdateLevelCompleted(i, -1);
                //Debug.Log(saveAndLoad.GetLevelsCompleted()[i]);
            }
            saveAndLoad.UpdateLevelCompleted(0, 0);
            saveAndLoad.GetskillsCollected().Add("Soldier1_Skill");
            saveAndLoad.GetskillsCollected().Add("Soldier2_Skill");
            saveAndLoad.GetskillsCollected().Add("HealSingleSpell_Skill");
            saveAndLoad.GetskillsCollected().Add("LesserFireBall_Skill");
            saveAndLoad.GetHotbarSkills()[0] = "Soldier1";
            saveAndLoad.GetHotbarSkills()[1] = "Soldier2";
            saveAndLoad.GetHotbarSkills()[2] = "HealSingleSpell";
            saveAndLoad.GetHotbarSkills()[3] = "LesserFireBall";
            saveAndLoad.GetHotbarSkillImages()[0] = "Soldier1_Skill";
            saveAndLoad.GetHotbarSkillImages()[1] = "Soldier2_Skill";
            saveAndLoad.GetHotbarSkillImages()[2] = "HealSingleSpell_Skill";
            saveAndLoad.GetHotbarSkillImages()[3] = "LesserFireBall_Skill";
        }
    }
    public int GetGameDifficulty()
    {
        return saves[currentSave].gameDifficulty;
    }
    public int GetGameDifficulty(int save)
    {
        if (saves[save] == null)
        {
            return 0;
        }
        return saves[save].gameDifficulty;
    }
    public void SetGameDifficulty(int difficulty)
    {
        saves[currentSave].gameDifficulty = difficulty;
    }
    public int CurrentLevel(int save)
    {
        if(saves[save] == null)
        {
            return 0;
        }
        for(int i = 0; i < saves[save].levelsCompleted.Length; i++)
        {
            if(saves[save].levelsCompleted[i] < 1)
            {
                return i + 1;
            }
        }
        return 10;
    }
    public void DeleteSave(int save)
    {
        saves[save] = null;
    }
    public static SaveAndLoad Get()
    {
        if(saveAndLoad == null)
        {
            saveAndLoad = LoadData();
        }
        return saveAndLoad;
    }
    public int[] GetLevelsCompleted()
    {
        return saves[currentSave].levelsCompleted;
    }
    /*
    public bool[] GetitemsCollected()
    {
        return itemsCollected;

    }
    */
    public List<string> GetskillsCollected()
    {
        return saves[currentSave].skillsCollected;
    }
    public int GettechTreePoints()
    {
        return saves[currentSave].techTreePoints;
    }
    public string[] GetHotbarSkills()
    {
        return saves[currentSave].hotbarSkills;
    }
    public string[] GetHotbarSkillImages()
    {
        return saves[currentSave].hotbarSkillImages;
    }
    public void UpdateLevelCompleted(int index, int starsEarned)
    {
        saves[currentSave].levelsCompleted[index] = starsEarned;
    }
    /*
    public void UpdateItemCollected(int index)
    {
        itemsCollected[index] = true;
    }
    */
    public void UpdateSkillsCollected(GameObject skill)
    {
        saves[currentSave].skillsCollected.Add(skill.name);
    }
    public void RemoveSkillCollected(GameObject skill)
    {
        saves[currentSave].skillsCollected.Remove(skill.name);
    }
    public void UpdateTechTreePoints(int pointsToAdjust)
    {
        saves[currentSave].techTreePoints += pointsToAdjust;
        techTreePointsEvents.Invoke();
    }
    /*
    public void UpdateHotbarSkills(string[] newHotbar)
    {
        saves[currentSave].hotbarSkills = newHotbar;
    }
    public void UpdatehotbarSkillImages(string[] newHotbarImages)
    {
        saves[currentSave].hotbarSkillImages = newHotbarImages;
    }
    */
    public void UpdateHotbarSkills(int i, string value)
    {
        saves[currentSave].hotbarSkills[i] = value;
    }
    public void UpdateHotbarSkillImages(int i, string value)
    {
        saves[currentSave].hotbarSkillImages[i] = value;
    }

    public static void SaveData()
    {
        Debug.Log("Save Data");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saved_data";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveAndLoad);
        stream.Close();
    }

    private static SaveAndLoad LoadData()
    {
        string path = Application.persistentDataPath + "/saved_data";
        Debug.Log(path);
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveAndLoad data = formatter.Deserialize(stream) as SaveAndLoad;
            stream.Close();
            saveAndLoad = data;
            
            
            //Debug.Log("Load Data");

            return data;
        }
        else
        {
            Debug.Log("Creating new save and load file");
            saveAndLoad = new SaveAndLoad();
            saveAndLoad.saves[saveAndLoad.currentSave] = new Save();
            for (int i = 0; i <= saveAndLoad.GetHotbarSkills().Length - 1; i++)
            {
                saveAndLoad.GetHotbarSkills()[i] = null;
                saveAndLoad.GetHotbarSkills()[i] = null;
            }
            
            for (int i = 0; i <= saveAndLoad.GetLevelsCompleted().Length - 1; i++)
            {
                saveAndLoad.UpdateLevelCompleted(i, -1);
                //Debug.Log(saveAndLoad.GetLevelsCompleted()[i]);
            }
            saveAndLoad.UpdateLevelCompleted(0, 0);
            saveAndLoad.GetskillsCollected().Add("Soldier1_Skill");
            saveAndLoad.GetskillsCollected().Add("Soldier2_Skill");
            saveAndLoad.GetskillsCollected().Add("HealSingleSpell_Skill");
            saveAndLoad.GetskillsCollected().Add("LesserFireBall_Skill");
            saveAndLoad.GetHotbarSkills()[0] = "Soldier1";
            saveAndLoad.GetHotbarSkills()[1] = "Soldier2";
            saveAndLoad.GetHotbarSkills()[2] = "HealSingleSpell";
            saveAndLoad.GetHotbarSkills()[3] = "LesserFireBall";
            saveAndLoad.GetHotbarSkillImages()[0] = "Soldier1_Skill";
            saveAndLoad.GetHotbarSkillImages()[1] = "Soldier2_Skill";
            saveAndLoad.GetHotbarSkillImages()[2] = "HealSingleSpell_Skill";
            saveAndLoad.GetHotbarSkillImages()[3] = "LesserFireBall_Skill";
            //Debug.Log(saveAndLoad.GetLevelsCompleted()[0]);
            //Debug.Log(saveAndLoad.GetLevelsCompleted()[1]);
            //Debug.Log("=================== " + saveAndLoad.levelsCompleted.Length);

            return saveAndLoad;
        }

    }

    public int FindHotbarIndexForSkill(GameObject skill)
    {
        string stringNameWithoutClone;
        if (skill.name.Contains("Clone"))
        {
            stringNameWithoutClone = skill.name.Substring(0, skill.name.Length - 7);
        }
        else if (skill.name.Contains("Skill"))
        {
            stringNameWithoutClone = skill.name.Substring(0, skill.name.Length - 6);
        }
        else
        {
            stringNameWithoutClone = skill.name;
        }
        
        
        for(int i = 0; i <= GetHotbarSkills().Length - 1; i++)
        {
            
            if(GetHotbarSkills()[i] != null && GetHotbarSkills()[i].Equals(stringNameWithoutClone))
            {
                //Debug.Log(GetHotbarSkills()[i]);
                return i;
            }
        }
        return -1;
    }

}
