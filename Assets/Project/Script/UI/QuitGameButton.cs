using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
        // Ghi log để kiểm tra xem phương thức có được gọi không
        Debug.Log("Quitting the game...");

        // Thoát trò chơi
        Application.Quit();

        // Lưu ý: Nếu bạn đang chạy trong Unity Editor, Application.Quit() sẽ không hoạt động
        // Bạn có thể sử dụng dòng dưới đây để kiểm tra trong Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}