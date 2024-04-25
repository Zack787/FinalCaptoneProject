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

    private float timeRemaining = 10f ;
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
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTime(timeRemaining);
        }
        else
        {
            UpdateTime(0);
            GameOver();
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

    public void OnClickPauseMenu()
    {
        _isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        // Tạo thể hiện mới của AiShipMovementControls nếu không tồn tại
        if (aiShipMovementControlsInstance == null && aiShipMovementPrefab != null)
        {
            aiShipMovementControlsInstance = Instantiate(aiShipMovementPrefab).GetComponent<AiShipMovementControls>();
        }
        // Tạm dừng tính năng AiShipMovementControls
        if (aiShipMovementControlsInstance != null)
        {
            aiShipMovementControlsInstance.SetEnabled(false);
        }
    }
    
    public void OnClickResume()
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
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}

