using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _gameDuration = 60f;
    [SerializeField] private MonoBehaviour _targetSpawner;

    private int _score;
    private float _timeRemaining;
    private bool _isGameOver;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGame();
    }

    private void ResetGame()
    {
        _score = 0;
        _timeRemaining = _gameDuration;
        _isGameOver = false;

        // Find and assign UI elements in the new scene
        _scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        _timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
        _targetSpawner = FindObjectOfType<TargetSpawner>();

        UpdateScoreTextInstance();
        UpdateTimerTextInstance();

        if (_targetSpawner != null)
        {
            _targetSpawner.enabled = true;
        }
    }

    private void Update()
    {
        if (!_isGameOver)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                UpdateTimerTextInstance();
            }
            else
            {
                EndGameInstance();
            }
        }
    }

    public static void TargetDestroyed()
    {
        if (!Instance._isGameOver)
        {
            Instance._score++;
            Instance.UpdateScoreTextInstance();
        }
    }

    public static void MissedTarget()
    {
        if (!Instance._isGameOver)
        {
            Instance._score = Mathf.Max(0, Instance._score - 1);  // Prevent negative score
            Instance.UpdateScoreTextInstance();
        }
    }

    private void UpdateScoreTextInstance()
    {
        if (_scoreText != null)
            _scoreText.text = "Score: " + _score;
    }

    private void UpdateTimerTextInstance()
    {
        if (_timerText != null)
            _timerText.text = "Time: " + Mathf.RoundToInt(_timeRemaining);
    }

    private void EndGameInstance()
    {
        _isGameOver = true;
        if (_targetSpawner != null)
            _targetSpawner.enabled = false;
        Debug.Log("Game Over! Final Score: " + _score);
        // You can add more game over logic here, like showing a game over screen
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}