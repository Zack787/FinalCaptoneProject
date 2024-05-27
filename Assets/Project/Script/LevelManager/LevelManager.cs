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

        // Đặt tất cả các nút không thể tương tác
        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
        }

        // Đảm bảo levelUnlocked không vượt quá số lượng các nút
        levelUnlocked = Mathf.Clamp(levelUnlocked, 0, button.Length);

        // Chỉ đặt các nút có chỉ số nhỏ hơn levelUnlocked có thể tương tác
        for (int i = 0; i < levelUnlocked; i++)
        {
            button[i].interactable = true;
        }
    }

    /*public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }*/

    // Update is called once per frame
    void Update()
    {
    }
}
