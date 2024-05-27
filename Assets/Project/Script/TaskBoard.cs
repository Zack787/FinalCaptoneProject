using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskBoard : MonoBehaviour
{
    public GameObject taskBoardLevel1;
    public GameObject taskBoardLevel2;
    public GameObject taskBoardLevel3;
    public GameObject taskBoardLevel4;

    // Các hàm này sẽ chỉ hiển thị bảng nhiệm vụ tương ứng
    public void ShowTasksLevel1()
    {
        DeactivateAllTaskBoards(); // Ẩn tất cả các bảng nhiệm vụ khác
        taskBoardLevel1.SetActive(true);
    }

    public void ShowTasksLevel2()
    {
        DeactivateAllTaskBoards(); // Ẩn tất cả các bảng nhiệm vụ khác
        taskBoardLevel2.SetActive(true);
    }

    public void ShowTasksLevel3()
    {
        DeactivateAllTaskBoards(); // Ẩn tất cả các bảng nhiệm vụ khác
        taskBoardLevel3.SetActive(true);
    }

    public void ShowTasksLevel4()
    {
        DeactivateAllTaskBoards(); // Ẩn tất cả các bảng nhiệm vụ khác
        taskBoardLevel4.SetActive(true);
    }

    // Các hàm này sẽ chuyển scene tương ứng
    public void PlayLevel1()
    {
        SceneManager.LoadScene("SampleScene"); // Load cảnh màn 1
        Time.timeScale = 1;
    }

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

    // Hàm này sẽ ẩn tất cả các bảng nhiệm vụ
    private void DeactivateAllTaskBoards()
    {
        taskBoardLevel1.SetActive(false);
        taskBoardLevel2.SetActive(false);
        taskBoardLevel3.SetActive(false);
        taskBoardLevel4.SetActive(false);
    }
}
