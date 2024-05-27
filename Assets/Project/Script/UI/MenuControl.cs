using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuControl : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Setting;
    
    // Start is called before the first frame update
    public void Start()
    {
        Cursor.visible = true;

    }
   /* public void Play1v1()
    {
        Application.LoadLevel("Play1v1");
        Time.timeScale = 1;
    }*/
    public void Play()
    {
        Application.LoadLevel("Guide");
        Time.timeScale = 1;
    }
    public void OnClickAbout()
    {
        SceneManager.LoadScene("About");
    }
    public void OnClickSetting()
    {
        MainMenu.SetActive(false);
        Setting.SetActive(true);
        
    }
    public void OnBackMenu()
    {
        Setting.SetActive(false);
        MainMenu.SetActive(true);
      

    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
