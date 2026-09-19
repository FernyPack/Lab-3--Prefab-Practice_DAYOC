using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TMP_Text killText;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject pauseUI;
    public Button retryButton;
    public Button mainMenuButton;

    [Header("Gameplay Settings")]
    public int requiredKills = 3;

    private int currentKills = 0;
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        SetupUI();
        UpdateKillUI();
        HandleCursorState();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupUI();
        UpdateKillUI();
        HandleCursorState();
    }

    void SetupUI()
    {
        if (killText == null)
        {
            GameObject go = GameObject.Find("KillText");
            if (go != null) killText = go.GetComponent<TMP_Text>();
        }

        if (winUI != null) winUI.SetActive(false);
        if (loseUI != null) loseUI.SetActive(false);
        if (pauseUI != null) pauseUI.SetActive(false);

        if (retryButton != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(RetryLevel);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    // =========================
    // Kill Handling
    // =========================
    public void AddKill()
    {
        currentKills++;
        UpdateKillUI();
        Debug.Log($"AddKill called -> {currentKills}/{requiredKills}");

        if (currentKills >= requiredKills)
            WinLevel();
    }

    void UpdateKillUI()
    {
        if (killText != null)
            killText.text = $"Kills: {currentKills}/{requiredKills}";
    }

    // =========================
    // Win / Lose
    // =========================
    void WinLevel()
    {
        Debug.Log("Level Complete!");
        if (winUI != null) winUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseLevel()
    {
        Debug.Log("You Died!");
        if (loseUI != null) loseUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // =========================
    // Pause System
    // =========================
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseUI != null)
            pauseUI.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // =========================
    // Button Callbacks
    // =========================
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        currentKills = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        currentKills = 0;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("All levels completed! You Win!");
            if (winUI != null) winUI.SetActive(true);
        }
    }

    // =========================
    // Cursor Control
    // =========================
    void HandleCursorState()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
