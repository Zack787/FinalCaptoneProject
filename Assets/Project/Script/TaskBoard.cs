using UnityEngine;
using UnityEngine.UI;

public class TaskBoard : MonoBehaviour
{
    public GameObject taskBoard;
    /*public Text taskText;*/

    public void ShowTasks()
    {
       
        taskBoard.SetActive(true);
    }
    public void Play()
    {
        Application.LoadLevel("SampleScene");
        Time.timeScale = 1;
    }
}
