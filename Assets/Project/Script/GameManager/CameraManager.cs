using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    enum VirtualCameras
    {
        NoCamera = -1,
        PhongdieukhienCam = 0,
        FollowCam,
    }
    [SerializeField] List<GameObject> _virtualCam;
    VirtualCameras CameraKeyPress
    {
        get
        {
            for (int i = 0; i < _virtualCam.Count; ++i)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    return (VirtualCameras)i;
            }
            return VirtualCameras.NoCamera;
        }
       
    }    
    // Start is called before the first frame update
    void Start()
    {
        SetActiveCamera(VirtualCameras.PhongdieukhienCam);
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveCamera(CameraKeyPress);
    }
     void SetActiveCamera(VirtualCameras activeCamera)
    {
        if (activeCamera == VirtualCameras.NoCamera)
        {
            Debug.Log("No Camera");
            return;
        }
        Debug.Log($"${activeCamera.ToString()}");
        foreach (GameObject cam in _virtualCam)
        {
            cam.SetActive(cam.tag.Equals(activeCamera.ToString()));
        }
    }    
}
