using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    [System.Serializable]
    public class CameraInfo
    {
        public GameObject cameraObject;
        public bool isActive;
    }

    enum VirtualCameras
    {
        NoSelection = -1,
        CockpitCamera = 0,
        FollowCamera = 1,
        EnemyFollowCamera = 2,
    }

    [SerializeField] List<CameraInfo> _virtualCam;

    public Transform ActiveCamera { get; private set; }
    public UnityEvent ActiveCameraChanged;

    private VirtualCameras _activeCameraIndex = VirtualCameras.NoSelection; // Lưu trạng thái camera hiện tại

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ActiveCameraChanged = new UnityEvent();

        // Đăng ký hàm để lắng nghe sự kiện load lại scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Hủy đăng ký hàm khi đối tượng bị hủy
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        SetActiveCamera(VirtualCameras.CockpitCamera);
    }

    void Update()
    {
        SetActiveCamera(CameraKeyPress);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Khôi phục trạng thái của CameraManager sau khi scene được load lại
        SetActiveCamera(VirtualCameras.CockpitCamera);
    }

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

    void SetActiveCamera(VirtualCameras selectedCamera)
    {
        if (selectedCamera == VirtualCameras.NoSelection)
        {
            return;
        }

        foreach (var camInfo in _virtualCam)
        {
            if (_virtualCam[(int)selectedCamera] == camInfo)
            {
                if (camInfo.cameraObject != null)
                {
                    camInfo.cameraObject.SetActive(true);
                    ActiveCamera = camInfo.cameraObject.transform;
                    camInfo.isActive = true;
                    ActiveCameraChanged.Invoke();
                }
            }
            else
            {
                if (camInfo.cameraObject != null)
                {
                    camInfo.cameraObject.SetActive(false);
                    camInfo.isActive = false;
                }
            }
        }

        _activeCameraIndex = selectedCamera; // Lưu trạng thái camera hiện tại
    }
}
