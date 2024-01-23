using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraManager : MonoBehaviour
{
    enum VirtualCameras
    {
        NoSelection = -1,
        CockpitCamera = 0,
        FollowCamera = 1,
        EnemyFollowCamera = 2,
    }
    [SerializeField] List<GameObject> _virtualCam;

    public Transform ActiveCamera { get; private set; }
    public UnityEvent ActiveCameraChanged;
    VirtualCameras CameraKeyPress
    {
        get
        {
            for (int i = 0; i < _virtualCam.Count; ++i)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    return (VirtualCameras)i;
            }
            return VirtualCameras.NoSelection;
        }
       
    }
    void Awake()
    {
        ActiveCameraChanged = new UnityEvent();
    }
    void Start()
    {
        SetActiveCamera(VirtualCameras.CockpitCamera);
    }

  
    void Update()
    {
        SetActiveCamera(CameraKeyPress);
    }
     void SetActiveCamera(VirtualCameras selectedCamera)
    {
        if (selectedCamera == VirtualCameras.NoSelection)
        {
            
            return;
        }

        VirtualCameras camIndex = VirtualCameras.CockpitCamera;
        foreach (var cam in _virtualCam)
        {
            if (camIndex++ == selectedCamera)
            {
                cam.gameObject.SetActive(true);
                ActiveCamera = cam.transform;
                ActiveCameraChanged.Invoke();
            }
            else
            {
                cam.gameObject.SetActive(false);
            }
        }
    }    
}
