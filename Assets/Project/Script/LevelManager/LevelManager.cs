using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    int levelUnlocked;

    public Button[] button;

    // Start is called before the first frame update
    void Start()
    {
        levelUnlocked = PlayerPrefs.GetInt("levelUnlocked", 1);
        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
        }
        for (int i = 0; i < levelUnlocked; i++)
        {
            button[i].interactable = true;
        }
    }
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
