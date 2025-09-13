using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Loading,
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public enum VehicleType
{
    Car,
    Ship,
    Plane
}

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public GameState currentState = GameState.MainMenu;
    public VehicleType currentVehicle = VehicleType.Car;
    
    [Header("Scene Names")]
    public string mainMenuScene = "MainMenu";
    public string carLevelScene = "CarTest";
    public string shipLevelScene = "ShipLevel";
    public string planeLevelScene = "PlaneLevel";
    
    // Singleton pattern
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
    
    // Events
    public System.Action<GameState> OnStateChanged;
    public System.Action<VehicleType> OnVehicleChanged;
    
    void Awake()
    {
        // Singleton enforcement
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        // Initialize
        SetState(GameState.MainMenu);
    }
    
    void Start()
    {
        // Load main menu if we're not already there
        if (SceneManager.GetActiveScene().name != mainMenuScene)
        {
            LoadMainMenu();
        }
    }
    
    public void SetState(GameState newState)
    {
        if (currentState != newState)
        {
            GameState previousState = currentState;
            currentState = newState;
            
            Debug.Log($"Game state changed from {previousState} to {newState}");
            
            // Notify listeners
            OnStateChanged?.Invoke(newState);
            
            // Handle state-specific logic
            HandleStateChange(newState);
        }
    }
    
    private void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Loading:
                // Show loading screen, disable input, etc.
                break;
                
            case GameState.MainMenu:
                // Enable menu UI, reset game data
                Time.timeScale = 1f;
                break;
                
            case GameState.Playing:
                // Start gameplay, enable player input
                Time.timeScale = 1f;
                break;
                
            case GameState.Paused:
                // Pause game, show pause menu
                Time.timeScale = 0f;
                break;
                
            case GameState.GameOver:
                // Show game over screen, save scores
                Time.timeScale = 1f;
                break;
        }
    }
    
    public void SetVehicle(VehicleType vehicleType)
    {
        currentVehicle = vehicleType;
        OnVehicleChanged?.Invoke(vehicleType);
        Debug.Log($"Vehicle changed to: {vehicleType}");
    }
    
    // Scene Management Methods
    public void LoadMainMenu()
    {
        SetState(GameState.Loading);
        SceneManager.LoadScene(mainMenuScene);
    }
    
    public void StartCarLevel()
    {
        SetVehicle(VehicleType.Car);
        SetState(GameState.Loading);
        SceneManager.LoadScene(carLevelScene);
    }
    
    public void StartShipLevel()
    {
        SetVehicle(VehicleType.Ship);
        SetState(GameState.Loading);
        SceneManager.LoadScene(shipLevelScene);
    }
    
    public void StartPlaneLevel()
    {
        SetVehicle(VehicleType.Plane);
        SetState(GameState.Loading);
        SceneManager.LoadScene(planeLevelScene);
    }
    
    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            SetState(GameState.Paused);
        }
    }
    
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            SetState(GameState.Playing);
        }
    }
    
    public void GameOver()
    {
        SetState(GameState.GameOver);
    }
    
    public void RestartLevel()
    {
        SetState(GameState.Loading);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // Scene loading event handlers
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");
        
        // Set appropriate state based on scene
        if (scene.name == mainMenuScene)
        {
            SetState(GameState.MainMenu);
        }
        else if (scene.name == carLevelScene || scene.name == shipLevelScene || scene.name == planeLevelScene)
        {
            SetState(GameState.Playing);
        }
    }
}