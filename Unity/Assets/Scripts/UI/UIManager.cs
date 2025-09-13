using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    public GameObject mainMenuPanel;
    public Button carLevelButton;
    public Button shipLevelButton;
    public Button planeLevelButton;
    public Button quitButton;
    
    [Header("Game UI")]
    public GameObject gameUIPanel;
    public Button pauseButton;
    public Button restartButton;
    public Button mainMenuButton;
    
    [Header("Pause Menu")]
    public GameObject pauseMenuPanel;
    public Button resumeButton;
    public Button pauseRestartButton;
    public Button pauseMainMenuButton;
    
    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public Button gameOverRestartButton;
    public Button gameOverMainMenuButton;
    
    private void Start()
    {
        // Subscribe to GameManager events
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged += HandleStateChanged;
        }
        
        // Setup button listeners
        SetupButtons();
        
        // Initialize UI based on current state
        HandleStateChanged(GameManager.Instance?.currentState ?? GameState.MainMenu);
    }
    
    private void SetupButtons()
    {
        // Main Menu buttons
        if (carLevelButton != null)
            carLevelButton.onClick.AddListener(() => GameManager.Instance.StartCarLevel());
            
        if (shipLevelButton != null)
            shipLevelButton.onClick.AddListener(() => GameManager.Instance.StartShipLevel());
            
        if (planeLevelButton != null)
            planeLevelButton.onClick.AddListener(() => GameManager.Instance.StartPlaneLevel());
            
        if (quitButton != null)
            quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
        
        // Game UI buttons
        if (pauseButton != null)
            pauseButton.onClick.AddListener(() => GameManager.Instance.PauseGame());
            
        if (restartButton != null)
            restartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
            
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadMainMenu());
        
        // Pause Menu buttons
        if (resumeButton != null)
            resumeButton.onClick.AddListener(() => GameManager.Instance.ResumeGame());
            
        if (pauseRestartButton != null)
            pauseRestartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
            
        if (pauseMainMenuButton != null)
            pauseMainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadMainMenu());
        
        // Game Over buttons
        if (gameOverRestartButton != null)
            gameOverRestartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
            
        if (gameOverMainMenuButton != null)
            gameOverMainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadMainMenu());
    }
    
    private void HandleStateChanged(GameState newState)
    {
        // Hide all panels first
        SetPanelActive(mainMenuPanel, false);
        SetPanelActive(gameUIPanel, false);
        SetPanelActive(pauseMenuPanel, false);
        SetPanelActive(gameOverPanel, false);
        
        // Show appropriate panel based on state
        switch (newState)
        {
            case GameState.MainMenu:
                SetPanelActive(mainMenuPanel, true);
                break;
                
            case GameState.Playing:
                SetPanelActive(gameUIPanel, true);
                break;
                
            case GameState.Paused:
                SetPanelActive(gameUIPanel, true);
                SetPanelActive(pauseMenuPanel, true);
                break;
                
            case GameState.GameOver:
                SetPanelActive(gameOverPanel, true);
                break;
                
            case GameState.Loading:
                // Could show loading screen here
                break;
        }
    }
    
    private void SetPanelActive(GameObject panel, bool active)
    {
        if (panel != null)
        {
            panel.SetActive(active);
        }
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from events
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged -= HandleStateChanged;
        }
    }
}