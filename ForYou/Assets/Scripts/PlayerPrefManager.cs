using UnityEngine;
using System.Collections;

// stores unlocked levels
public class PlayerPrefManager
{
    // store a key for the name of the current level to indicate it is unlocked
    public static void UnlockLevel()
    {
        PlayerPrefs.SetInt(Application.loadedLevelName, 1);
    }

    // determine if a levelname is currently unlocked (i.e., it has a key set)
    public static bool LevelIsUnlocked(string levelName)
    {
        return (PlayerPrefs.HasKey(levelName));
    }

}
