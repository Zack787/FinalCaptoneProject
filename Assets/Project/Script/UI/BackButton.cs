using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BackButton : MonoBehaviour
{
    public void OnclickBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }    
    public void OnNextButton()
    {
        SceneManager.LoadScene("ChooseRound");
    }
}
