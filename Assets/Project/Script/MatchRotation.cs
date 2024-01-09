using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MatchRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;

     void LateUpdate()
    {
        transform.rotation = target.rotation;
    }
}
