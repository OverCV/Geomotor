# Documents: Ejemplos y snippets esenciales (Unity/C#)

Esta guía reúne ejemplos mínimos y directamente utilizables en Unity para implementar los bloques fundamentales del proyecto.

## 1) GameState y GameManager

```csharp
using UnityEngine;

public enum GameState { Loading, Playing, GameOver, Paused }

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState State = GameState.Loading;

    void Awake() {
        if (Instance == null) Instance = this; else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetState(GameState next) {
        State = next;
        // TODO: notificar a sistemas interesados (UI, audio)
    }

    public void TogglePause() {
        if (State == GameState.Playing) {
            SetState(GameState.Paused);
            Time.timeScale = 0f;
        } else if (State == GameState.Paused) {
            SetState(GameState.Playing);
            Time.timeScale = 1f;
        }
    }
}
```

## 2) LevelData (ScriptableObject)

```csharp
using UnityEngine;

[CreateAssetMenu(menuName = "Runner/LevelData")]
public class LevelData : ScriptableObject {
    public float trackWidth = 12f;
    public Vector3 playerSpawn = new Vector3(0, 1, 0);
    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    public PortalVehicleDef[] portals;
    public PortalGravityDef[] gravityPortals;
    public JumpPadDef[] jumpPads;
    public HazardDef[] hazards;
    public CoinDef[] collectibles;
}

[System.Serializable] public struct PortalVehicleDef { public Vector3 pos; public VehicleKind vehicle; }
[System.Serializable] public struct PortalGravityDef { public Vector3 pos; public Vector3 gravity; }
[System.Serializable] public struct JumpPadDef { public Vector3 pos; public float strength; }
[System.Serializable] public struct HazardDef { public Vector3 pos; public bool lethal; public float percentDamage; }
[System.Serializable] public struct CoinDef { public Vector3 pos; public int id; }

public enum VehicleKind { Car, Ship }
```

## 3) CarController (Rigidbody)

```csharp
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {
    public float forwardSpeed = 12f;
    public float jumpStrength = 6f;
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;

    Rigidbody body;

    void Awake() { body = GetComponent<Rigidbody>(); }

    void FixedUpdate() {
        if (GameManager.Instance.State != GameState.Playing) return;
        body.MovePosition(body.position + Vector3.forward * forwardSpeed * Time.fixedDeltaTime);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
            body.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
        }
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }
}
```

## 4) ShipController (Rigidbody)

```csharp
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour {
    public float verticalAccel = 20f;
    public float maxVerticalSpeed = 10f;
    public float shipMaxHeight = 20f;
    Rigidbody body;

    void Awake() { body = GetComponent<Rigidbody>(); }

    void FixedUpdate() {
        if (GameManager.Instance.State != GameState.Playing) return;
        body.MovePosition(body.position + Vector3.forward * Time.fixedDeltaTime * 12f);
        float input = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) ? 1f : 0f;
        float vy = Mathf.Clamp(body.velocity.y + verticalAccel * input * Time.fixedDeltaTime, -maxVerticalSpeed, maxVerticalSpeed);
        body.velocity = new Vector3(body.velocity.x, vy, body.velocity.z);
        
        // Límite de altura
        if (transform.position.y > shipMaxHeight) {
            transform.position = new Vector3(transform.position.x, shipMaxHeight, transform.position.z);
            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
        }
    }
}
```

## 5) PortalVehicle

```csharp
using UnityEngine;

public class PortalVehicle : MonoBehaviour {
    public VehicleKind target = VehicleKind.Ship;
    public float invulnSeconds = 0.25f;

    void OnTriggerEnter(Collider other) {
        var vehicle = other.GetComponent<PlayerVehicleSwitcher>();
        if (vehicle == null) return;
        vehicle.SwitchTo(target, invulnSeconds);
    }
}

public class PlayerVehicleSwitcher : MonoBehaviour {
    public CarController car;
    public ShipController ship;
    bool invulnerable;
    public System.Action<VehicleKind> OnVehicleChanged;

    public void SwitchTo(VehicleKind kind, float invuln) {
        car.enabled = (kind == VehicleKind.Car);
        ship.enabled = (kind == VehicleKind.Ship);
        OnVehicleChanged?.Invoke(kind);
        if (invuln > 0) { if (!invulnerable) StartCoroutine(Invuln(invuln)); }
    }

    System.Collections.IEnumerator Invuln(float t) {
        invulnerable = true; yield return new WaitForSeconds(t); invulnerable = false;
    }

    public bool IsInvulnerable => invulnerable;
}
```

## 6) PortalGravity

```csharp
using UnityEngine;

public class PortalGravity : MonoBehaviour {
    public Vector3 gravity = new Vector3(0, 9.81f, 0);
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) Physics.gravity = gravity;
    }
}
```

## 7) JumpPad

```csharp
using UnityEngine;

public class JumpPad : MonoBehaviour {
    public float strength = 8f;
    void OnTriggerEnter(Collider other) {
        var rb = other.attachedRigidbody; if (rb == null) return;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * strength, ForceMode.VelocityChange);
    }
}
```

## 8) Salud y Daño

```csharp
using UnityEngine;

public class Health : MonoBehaviour {
    public float max = 100f;
    public float current = 100f;
    public System.Action OnDied;
    public void ApplyFlat(float amount) { current = Mathf.Max(0, current - amount); if (current <= 0) OnDied?.Invoke(); }
    public void ApplyPercent(float percent) { ApplyFlat(max * percent); }
}

public class Hazard : MonoBehaviour {
    public bool lethal = false; public float percent = 0.25f;
    void OnTriggerEnter(Collider other) {
        var h = other.GetComponent<Health>(); if (h == null) return;
        if (lethal) h.ApplyFlat(h.max); else h.ApplyPercent(percent);
    }
}
```

## 9) UI de vida (Slider)

```csharp
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    public Health health; public Slider slider;
    void Update() { if (health != null && slider != null) slider.value = health.current / health.max; }
}
```

## 10) Input System (Unity Input System 1.7+)

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public PlayerInput playerInput;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference pauseAction;

    void Awake() {
        // Configurar callbacks
        jumpAction.action.performed += OnJump;
        pauseAction.action.performed += OnPause;
    }

    void OnJump(InputAction.CallbackContext context) {
        if (GameManager.Instance.State == GameState.Playing) {
            // Notificar al controlador activo
            var car = FindObjectOfType<CarController>();
            if (car != null && car.enabled) {
                // Trigger jump
            }
        }
    }

    void OnPause(InputAction.CallbackContext context) {
        GameManager.Instance.TogglePause();
    }

    void OnDestroy() {
        jumpAction.action.performed -= OnJump;
        pauseAction.action.performed -= OnPause;
    }
}
```

## 11) Sistema de Pausa

```csharp
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
    public GameObject pausePanel;
    public Button continueButton;
    public Button exitButton;

    void Start() {
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(ExitGame);
        pausePanel.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.TogglePause();
        }
        
        pausePanel.SetActive(GameManager.Instance.State == GameState.Paused);
    }

    void ContinueGame() {
        GameManager.Instance.TogglePause();
    }

    void ExitGame() {
        Application.Quit();
    }
}
```

## 12) Kinect Integration

```csharp
using UnityEngine;

public class KinectManager : MonoBehaviour {
    public static KinectManager Instance;
    public bool kinectAvailable = false;
    public KinectConfig config;

    void Awake() {
        if (Instance == null) Instance = this; else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        InitializeKinect();
    }

    void InitializeKinect() {
        try {
            // Aquí iría la inicialización del SDK de Kinect
            kinectAvailable = true;
            Debug.Log("Kinect inicializado correctamente");
        } catch (System.Exception e) {
            kinectAvailable = false;
            Debug.LogWarning("Kinect no disponible: " + e.Message);
        }
    }
}

[CreateAssetMenu(menuName = "Runner/KinectConfig")]
public class KinectConfig : ScriptableObject {
    public float poseThreshold = 0.7f;
    public float smoothingFactor = 0.8f;
    public float detectionLatency = 0.1f;
    public bool enableDebugVisualization = true;
}

public class PoseDetector : MonoBehaviour {
    public KinectConfig config;
    public enum PoseType { None, Jump, Descend, Left, Right }
    
    public PoseType currentPose = PoseType.None;
    public System.Action<PoseType> OnPoseDetected;

    void Update() {
        if (!KinectManager.Instance.kinectAvailable) return;
        
        PoseType newPose = DetectPose();
        if (newPose != currentPose) {
            currentPose = newPose;
            OnPoseDetected?.Invoke(newPose);
        }
    }

    PoseType DetectPose() {
        // Aquí iría la lógica de detección de poses
        // Por ahora retornamos None
        return PoseType.None;
    }
}

public class KinectInputProvider : MonoBehaviour {
    public PoseDetector poseDetector;
    public InputManager inputManager;

    void Start() {
        poseDetector.OnPoseDetected += OnPoseDetected;
    }

    void OnPoseDetected(PoseDetector.PoseType pose) {
        switch (pose) {
            case PoseDetector.PoseType.Jump:
                // Simular input de salto
                break;
            case PoseDetector.PoseType.Descend:
                // Simular input de descenso
                break;
            case PoseDetector.PoseType.Left:
                // Simular input izquierda
                break;
            case PoseDetector.PoseType.Right:
                // Simular input derecha
                break;
        }
    }
}
```

## 13) Sistema de Analíticas y Base de Datos

```csharp
using UnityEngine;
using System.Collections.Generic;

public class DataCollector : MonoBehaviour {
    public static DataCollector Instance;
    public HealthMetrics healthMetrics;
    
    private float sessionStartTime;
    private float totalActiveTime;
    private int totalJumps;
    private int totalPoses;
    private List<float> reactionTimes = new List<float>();

    void Awake() {
        if (Instance == null) Instance = this; else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        sessionStartTime = Time.time;
        GameManager.Instance.OnDied += OnGameOver;
    }

    public void RecordJump() {
        totalJumps++;
        healthMetrics.AddCaloriesBurned(5f); // 5 calorías por salto
    }

    public void RecordPose() {
        totalPoses++;
        healthMetrics.AddCaloriesBurned(2f); // 2 calorías por pose
    }

    public void RecordReactionTime(float time) {
        reactionTimes.Add(time);
    }

    void OnGameOver() {
        float sessionDuration = Time.time - sessionStartTime;
        float caloriesBurned = healthMetrics.GetTotalCaloriesBurned();
        
        // Guardar datos en Supabase
        SaveGameData(sessionDuration, caloriesBurned, totalJumps, totalPoses);
    }

    void SaveGameData(float duration, float calories, int jumps, int poses) {
        // Aquí iría la lógica de guardado en Supabase
        Debug.Log($"Sesión: {duration}s, Calorías: {calories}, Saltos: {jumps}, Poses: {poses}");
    }
}

public class HealthMetrics : MonoBehaviour {
    private float totalCaloriesBurned = 0f;
    private float sedentaryTime = 0f;
    private float activeTime = 0f;

    public void AddCaloriesBurned(float calories) {
        totalCaloriesBurned += calories;
        activeTime += Time.deltaTime;
    }

    public float GetTotalCaloriesBurned() => totalCaloriesBurned;
    public float GetActiveTime() => activeTime;
    public float GetSedentaryTime() => sedentaryTime;

    public string GetHealthRecommendation() {
        if (activeTime < 300f) // Menos de 5 minutos activo
            return "Intenta moverte más durante el juego";
        else if (activeTime > 1800f) // Más de 30 minutos activo
            return "¡Excelente! Mantén este nivel de actividad";
        else
            return "Buen trabajo, sigue así";
    }
}
```

## 14) UI de Analíticas

```csharp
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnalyticsUI : MonoBehaviour {
    public GameObject analyticsPanel;
    public TextMeshProUGUI caloriesText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI recommendationText;
    public Button closeButton;

    void Start() {
        closeButton.onClick.AddListener(CloseAnalytics);
        analyticsPanel.SetActive(false);
    }

    public void ShowAnalytics() {
        analyticsPanel.SetActive(true);
        UpdateAnalyticsDisplay();
    }

    void UpdateAnalyticsDisplay() {
        var metrics = FindObjectOfType<HealthMetrics>();
        if (metrics != null) {
            caloriesText.text = $"Calorías quemadas: {metrics.GetTotalCaloriesBurned():F1}";
            timeText.text = $"Tiempo activo: {metrics.GetActiveTime():F0}s";
            recommendationText.text = metrics.GetHealthRecommendation();
        }
    }

    void CloseAnalytics() {
        analyticsPanel.SetActive(false);
    }
}

public class HealthDashboard : MonoBehaviour {
    public TextMeshProUGUI weeklyStatsText;
    public TextMeshProUGUI monthlyStatsText;
    public GameObject[] achievementBadges;

    void Start() {
        LoadHealthStats();
    }

    void LoadHealthStats() {
        // Cargar estadísticas desde Supabase
        // Por ahora mostramos datos de ejemplo
        weeklyStatsText.text = "Esta semana: 1,250 calorías, 2.5 horas activo";
        monthlyStatsText.text = "Este mes: 5,800 calorías, 12 horas activo";
    }
}
```

## 15) Sistema de Audio

```csharp
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip portalSound;
    public AudioClip damageSound;
    public AudioClip gameOverSound;
    public AudioClip backgroundMusic;

    void Awake() {
        if (Instance == null) Instance = this; else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        PlayBackgroundMusic();
    }

    public void PlayJumpSound() {
        sfxSource.PlayOneShot(jumpSound);
    }

    public void PlayPortalSound() {
        sfxSource.PlayOneShot(portalSound);
    }

    public void PlayDamageSound() {
        sfxSource.PlayOneShot(damageSound);
    }

    public void PlayGameOverSound() {
        sfxSource.PlayOneShot(gameOverSound);
    }

    void PlayBackgroundMusic() {
        if (backgroundMusic != null) {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume) {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume) {
        sfxSource.volume = volume;
    }
}
```

## 16) UI de Vehículo Actual

```csharp
using UnityEngine;
using UnityEngine.UI;

public class VehicleIconUI : MonoBehaviour {
    public Image vehicleIcon;
    public Sprite carSprite;
    public Sprite shipSprite;
    public PlayerVehicleSwitcher vehicleSwitcher;

    void Start() {
        if (vehicleSwitcher != null) {
            vehicleSwitcher.OnVehicleChanged += OnVehicleChanged;
        }
    }

    void OnVehicleChanged(VehicleKind kind) {
        switch (kind) {
            case VehicleKind.Car:
                vehicleIcon.sprite = carSprite;
                break;
            case VehicleKind.Ship:
                vehicleIcon.sprite = shipSprite;
                break;
        }
    }
}
```

## 17) Game Over System

```csharp
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button mainMenuButton;
    public TextMeshProUGUI finalScoreText;

    void Start() {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver() {
        gameOverPanel.SetActive(true);
        UpdateFinalScore();
        AudioManager.Instance.PlayGameOverSound();
    }

    void UpdateFinalScore() {
        var metrics = FindObjectOfType<HealthMetrics>();
        if (metrics != null) {
            finalScoreText.text = $"Calorías quemadas: {metrics.GetTotalCaloriesBurned():F1}";
        }
    }

    void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
```

## 18) Out of Bounds

```csharp
using UnityEngine;

public class OutOfBounds : MonoBehaviour {
    public float outOfBoundsY = -10f;
    public Transform player;

    void Update() {
        if (player != null && player.position.y < outOfBoundsY) {
            var health = player.GetComponent<Health>();
            if (health != null) {
                health.ApplyFlat(health.max); // Muerte instantánea
            }
        }
    }
}
```

## 19) GlobalConfig (ScriptableObject) y aplicación

```csharp
using UnityEngine;

[CreateAssetMenu(menuName = "Runner/GlobalConfig")]
public class GlobalConfig : ScriptableObject {
    public float carForwardSpeed = 12f;
    public float carJumpStrength = 6f;
    public float shipVerticalAccel = 20f;
    public float shipMaxVerticalSpeed = 10f;
    public float shipMaxHeight = 20f;
    public float jumpPadStrength = 8f;
    public float portalInvulnTime = 0.25f;
}

public class ApplyGlobalConfig : MonoBehaviour {
    public GlobalConfig config;
    public CarController car;
    public ShipController ship;

    void Awake() {
        if (config == null) return;
        if (car != null) { 
            car.forwardSpeed = config.carForwardSpeed; 
            car.jumpStrength = config.carJumpStrength; 
        }
        if (ship != null) { 
            ship.verticalAccel = config.shipVerticalAccel; 
            ship.maxVerticalSpeed = config.shipMaxVerticalSpeed;
            ship.shipMaxHeight = config.shipMaxHeight;
        }
    }
}
```

## 20) LevelLoader (aplica LevelData)

```csharp
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    public LevelData level;
    public Transform player;
    public GameObject portalPrefab;
    public GameObject jumpPadPrefab;
    public GameObject hazardPrefab;

    void Start() {
        if (level == null) return;
        Physics.gravity = level.gravity;
        if (player != null) player.position = level.playerSpawn;
        
        // Instanciar elementos del nivel
        InstantiateLevelElements();
    }

    void InstantiateLevelElements() {
        // Instanciar portales
        foreach (var portalDef in level.portals) {
            var portal = Instantiate(portalPrefab, portalDef.pos, Quaternion.identity);
            var portalVehicle = portal.GetComponent<PortalVehicle>();
            if (portalVehicle != null) {
                portalVehicle.target = portalDef.vehicle;
            }
        }

        // Instanciar jump pads
        foreach (var padDef in level.jumpPads) {
            var pad = Instantiate(jumpPadPrefab, padDef.pos, Quaternion.identity);
            var jumpPad = pad.GetComponent<JumpPad>();
            if (jumpPad != null) {
                jumpPad.strength = padDef.strength;
            }
        }

        // Instanciar hazards
        foreach (var hazardDef in level.hazards) {
            var hazard = Instantiate(hazardPrefab, hazardDef.pos, Quaternion.identity);
            var hazardComp = hazard.GetComponent<Hazard>();
            if (hazardComp != null) {
                hazardComp.lethal = hazardDef.lethal;
                hazardComp.percent = hazardDef.percentDamage;
            }
        }
    }
}
```

## 21) Checklist de escena mínima

- Crear `GameManager` en escena (DontDestroyOnLoad), `State = Playing` para pruebas.
- Crear `Player` con `Rigidbody`, `CarController` y `ShipController` (uno activo), `PlayerVehicleSwitcher` configurado.
- Añadir `PortalVehicle`, `PortalGravity`, `JumpPad`, `Hazard` con colliders como triggers.
- Añadir UI `Canvas` + `Slider` y vincular a `HealthBarUI`.
- Ajustar `Physics.gravity` según `LevelData` al cargar el nivel.
- Configurar `InputManager` con Input Actions Asset.
- Añadir `AudioManager` para efectos de sonido.
- Configurar `DataCollector` y `HealthMetrics` para analíticas.
- Añadir `KinectManager` y `PoseDetector` si se usa Kinect.
- Configurar `GameOverManager` y `OutOfBounds`.

## 22) Configuración de Input Actions Asset

```json
{
  "name": "InputSystem_Actions",
  "maps": [
    {
      "name": "Player",
      "id": "player-map",
      "actions": [
        {
          "name": "Move",
          "type": "Value",
          "id": "move-action",
          "expectedControlType": "Vector2"
        },
        {
          "name": "JumpOrAscend",
          "type": "Button",
          "id": "jump-action"
        },
        {
          "name": "Pause",
          "type": "Button",
          "id": "pause-action"
        }
      ],
      "bindings": [
        {
          "name": "WASD",
          "id": "wasd-binding",
          "path": "2DVector",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "Move",
          "isComposite": true,
          "isPartOfComposite": false
        },
        {
          "name": "up",
          "id": "up-binding",
          "path": "<Keyboard>/w",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "Move",
          "isComposite": false,
          "isPartOfComposite": true
        },
        {
          "name": "down",
          "id": "down-binding",
          "path": "<Keyboard>/s",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "Move",
          "isComposite": false,
          "isPartOfComposite": true
        },
        {
          "name": "left",
          "id": "left-binding",
          "path": "<Keyboard>/a",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "Move",
          "isComposite": false,
          "isPartOfComposite": true
        },
        {
          "name": "right",
          "id": "right-binding",
          "path": "<Keyboard>/d",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "Move",
          "isComposite": false,
          "isPartOfComposite": true
        },
        {
          "name": "",
          "id": "jump-binding",
          "path": "<Keyboard>/space",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "JumpOrAscend"
        },
        {
          "name": "",
          "id": "pause-binding",
          "path": "<Keyboard>/escape",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "Pause"
        }
      ]
    }
  ]
}
```

