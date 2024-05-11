using UnityEngine;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool _isPaused = false;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        if (ShouldQuitGame())
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCursorVisibility();
        }
    }

    bool ShouldQuitGame()
    {
        return Input.GetKeyUp(KeyCode.Escape);
    }

    void TogglePause()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0f : 1f;

        UIManager.Instance.TogglePauseMenu(_isPaused);
    }

    void ToggleCursorVisibility()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Confined;
    }
    public void InCombat(bool inCombat)
    {
        if (inCombat)
        {
            MusicManager.Instance.PlayCombatMusic();
            return;
        }

        MusicManager.Instance.PlayPatrolMusic();
    }
   
    public void RestartGame()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian chạy bình thường
        UIManager.Instance.ResetUIState(); // Reset trạng thái của các UI
        SceneManager.LoadScene("SampleScene"); // Load lại scene hiện tại
    }
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
