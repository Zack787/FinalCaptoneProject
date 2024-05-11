using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskBoard : MonoBehaviour
{
    public GameObject taskBoardLevel1;
    public GameObject taskBoardLevel2;
    public GameObject taskBoardLevel3;
    public GameObject taskBoardLevel4;

    public void ShowTasksLevel1()
    {
        taskBoardLevel1.SetActive(true);
    }

    public void ShowTasksLevel2()
    {
        taskBoardLevel2.SetActive(true);
    }

    public void ShowTasksLevel3()
    {
        taskBoardLevel3.SetActive(true);
    }

    public void ShowTasksLevel4()
    {
        taskBoardLevel4.SetActive(true);
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene("SampleScene"); // Load cảnh màn 1
        Time.timeScale = 1;
    }

    // Đây là hàm Play cho màn 2 (lev2)
    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2"); // Load cảnh màn 2
        Time.timeScale = 1;
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3"); // Load cảnh màn 3
        Time.timeScale = 1;
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level4"); // Load cảnh màn 4
        Time.timeScale = 1;
    }
    public void OnClickBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
