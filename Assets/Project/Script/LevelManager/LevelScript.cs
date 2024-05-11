using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public void Pass()
    {
        int currentlevel = SceneManager.GetActiveScene().buildIndex;

        if(currentlevel >= PlayerPrefs.GetInt("levelUnlocked"))
        {
            PlayerPrefs.SetInt("levelUnlocked", currentlevel + 1);
        }
        Debug.Log("Level" + PlayerPrefs.GetInt("levelUnlocked") + "Unlocked");
    }
}
