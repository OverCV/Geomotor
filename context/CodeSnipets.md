# Documents: Ejemplos y snippets esenciales (Unity/C#)

Esta guía reúne ejemplos mínimos y directamente utilizables en Unity para implementar los bloques fundamentales del proyecto.

## 1) GameState y GameManager

```csharp
using UnityEngine;

public enum GameState { Loading, Playing, GameOver }

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
    Rigidbody body;

    void Awake() { body = GetComponent<Rigidbody>(); }

    void FixedUpdate() {
        if (GameManager.Instance.State != GameState.Playing) return;
        body.MovePosition(body.position + Vector3.forward * Time.fixedDeltaTime * 12f);
        float input = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) ? 1f : 0f;
        float vy = Mathf.Clamp(body.velocity.y + verticalAccel * input * Time.fixedDeltaTime, -maxVerticalSpeed, maxVerticalSpeed);
        body.velocity = new Vector3(body.velocity.x, vy, body.velocity.z);
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

    public void SwitchTo(VehicleKind kind, float invuln) {
        car.enabled = (kind == VehicleKind.Car);
        ship.enabled = (kind == VehicleKind.Ship);
        if (invuln > 0) { if (!invulnerable) StartCoroutine(Invuln(invuln)); }
    }

    System.Collections.IEnumerator Invuln(float t) {
        invulnerable = true; yield return new WaitForSeconds(t); invulnerable = false;
    }
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

## 10) Checklist de escena mínima

- Crear `GameManager` en escena (DontDestroyOnLoad), `State = Playing` para pruebas.
- Crear `Player` con `Rigidbody`, `CarController` y `ShipController` (uno activo), `PlayerVehicleSwitcher` configurado.
- Añadir `PortalVehicle`, `PortalGravity`, `JumpPad`, `Hazard` con colliders como triggers.
- Añadir UI `Canvas` + `Slider` y vincular a `HealthBarUI`.
- Ajustar `Physics.gravity` según `LevelData` al cargar el nivel.

## 11) GlobalConfig (ScriptableObject) y aplicación

```csharp
using UnityEngine;

[CreateAssetMenu(menuName = "Runner/GlobalConfig")]
public class GlobalConfig : ScriptableObject {
    public float carForwardSpeed = 12f;
    public float carJumpStrength = 6f;
    public float shipVerticalAccel = 20f;
    public float shipMaxVerticalSpeed = 10f;
}

public class ApplyGlobalConfig : MonoBehaviour {
    public GlobalConfig config;
    public CarController car;
    public ShipController ship;

    void Awake() {
        if (config == null) return;
        if (car != null) { car.forwardSpeed = config.carForwardSpeed; car.jumpStrength = config.carJumpStrength; }
        if (ship != null) { ship.verticalAccel = config.shipVerticalAccel; ship.maxVerticalSpeed = config.shipMaxVerticalSpeed; }
    }
}
```

## 12) LevelLoader (aplica LevelData)

```csharp
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    public LevelData level;
    public Transform player;

    void Start() {
        if (level == null) return;
        Physics.gravity = level.gravity;
        if (player != null) player.position = level.playerSpawn;
        // Opcional: instanciar portales/pads/hazards desde prefabs según definiciones
    }
}
```

