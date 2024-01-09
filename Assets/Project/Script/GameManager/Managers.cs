using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Managers : MonoBehaviour
{
    static Managers instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);

        }else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
    }
   
}
