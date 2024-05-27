using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TargetIndicator _targetIndicatorPrefab;
    [SerializeField] Canvas _mainCanvas;

    public GameObject pauseMenu;
    public GameObject GameOverScreen;
    public TextMeshProUGUI timer;
    public GameObject WinGameScreen;

    private float timeRemaining = 60f;
    public static UIManager Instance;
    private bool _isPaused = false;

    // Biến để lưu trữ Prefab của AiShipMovementControls
    public GameObject aiShipMovementPrefab;

    // Biến để lưu trữ thể hiện của AiShipMovementControls
    private AiShipMovementControls aiShipMovementControlsInstance;

    private List<TargetIndicator> _targetIndicators;

    void Start()
    {
        UpdateTime(timeRemaining);
        GameOverScreen.SetActive(false);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _targetIndicators = new List<TargetIndicator>();
    }

    public void AddTarget(Transform target)
    {
        var targetIndicator = Instantiate(_targetIndicatorPrefab, _mainCanvas.transform);
        targetIndicator.Init(target, _mainCanvas);
        _targetIndicators.Add(targetIndicator);
    }

    public void RemoveTarget(Transform target)
    {
        var key = target.GetInstanceID();
        var indicator = _targetIndicators.FirstOrDefault(i => i.Key == key);
        if (indicator)
        {
            _targetIndicators.Remove(indicator);
            Destroy(indicator.gameObject);
        }
    }

    void Update()
    {
        if (timeRemaining > 0 && !_isPaused)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTime(timeRemaining);
            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    public void UpdateTargetIndicators(List<Transform> targets, int lockedOnTarget)
    {
        foreach (var targetIndicator in _targetIndicators)
        {
            targetIndicator.gameObject.SetActive(targets.Any(target => target.GetInstanceID() == targetIndicator.Key));
            targetIndicator.LockedOn = targetIndicator.Key == lockedOnTarget;
        }
    }

    public void UpdateTime(float timeleft)
    {
        timer.text = " Time Remaining: " + timeleft;
    }

    public void OnClickResume()
    {
        if (_isPaused)
        {
            _isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            // Kích hoạt lại tính năng AiShipMovementControls
            if (aiShipMovementControlsInstance != null)
            {
                aiShipMovementControlsInstance.SetEnabled(true);
            }
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Đảm bảo thời gian được thiết lập lại khi load lại scene
        timeRemaining = 60f; // Đặt lại thời gian đếm ngược
        UpdateTime(timeRemaining); // Cập nhật hiển thị thời gian đếm ngược
        GameOverScreen.SetActive(false); // Ẩn màn hình Game Over nếu đang hiển thị
        pauseMenu.SetActive(false); // Tắt màn hình PauseGame khi chuyển scene
        _isPaused = false; // Đảm bảo trạng thái Pause được thiết lập lại 
    }
    public void OnClickNextLevel()
    {
        SceneManager.LoadScene("ChooseRound");
    }    
    public void OnClickQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverScreen.SetActive(true);
    }
    public void WinGame()
    {
        Time.timeScale = 0;
        WinGameScreen.SetActive(true);
    }


    public void TogglePauseMenu(bool isPaused)
    {
        _isPaused = isPaused;
        Time.timeScale = _isPaused ? 0f : 1f;
        pauseMenu.SetActive(_isPaused);

        // Tạm dừng hoặc tiếp tục tính năng AiShipMovementControls
        if (aiShipMovementControlsInstance != null)
        {
            aiShipMovementControlsInstance.SetEnabled(!_isPaused);
        }
    }
    public void ResetUIState()
    {
        pauseMenu.SetActive(false);
        GameOverScreen.SetActive(false);
        WinGameScreen.SetActive(false);
        _isPaused = false; // Đảm bảo rằng trạng thái pause được reset
    }

}
