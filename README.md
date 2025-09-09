# GeoMotor: Runner 3D con Vehículos y Portales

Runner 3D tipo Geometry Dash con vehículos intercambiables, portales y detección de poses mediante Kinect. Proyecto Unity (C#) con arquitectura modular y desarrollo colaborativo.

## 1) Visión y Núcleo Jugable

**Concepto:** Runner 3D donde el jugador controla vehículos que avanzan automáticamente, saltando obstáculos y cambiando entre modos de movimiento mediante portales.

**Núcleo jugable:**
- **Auto-forward:** Vehículos avanzan constantemente hacia adelante
- **Vehículos intercambiables:** Car (salto) ↔ Ship (ascenso/descenso)
- **Portales:** Cambian vehículo y gravedad instantáneamente
- **Input dual:** Teclado/gamepad + Kinect (detección de poses)
- **Obstáculos:** Pinchos (muerte), bloques (daño), jump pads

## 2) Alcance MVP (v0.1)

**Entregables mínimos:**
- 1 nivel jugable con 3 secciones (Car → Portal → Ship → Portal → Car)
- Sistema de input dual (teclado + Kinect básico)
- 2 vehículos funcionales con físicas estables
- Sistema de daño y muerte
- UI básica (vida, vehículo actual, GameOver)
- 1-2 poses de Kinect detectables (manos arriba = salto)

**No incluido en v0.1:**
- Múltiples niveles
- Efectos visuales avanzados
- Sistema de puntuación
- Menús complejos
- Todas las poses de Kinect

## 3) Pila Tecnológica

**Motor y lenguaje:**
- Unity 2021.3 LTS+ (C#)
- Universal Render Pipeline (URP)
- Input System 1.7+

**Física y movimiento:**
- PhysX (Rigidbody, Colliders)
- Character Controller (opcional)

**Input y detección:**
- Input System (teclado, gamepad)
- Azure Kinect SDK + Body Tracking SDK
- Kinect for Windows SDK 2.0 (fallback)

**Assets y pipeline:**
- Blender 3.0+ (modelado)
- FBX/GLB (formato de exportación)
- ScriptableObjects (datos de nivel)

## 4) Arquitectura

### Estados del Juego
```csharp
public enum GameState {
    MainMenu,    // Menú principal
    Playing,     // Jugando
    Paused,      // Pausado
    GameOver,    // Muerte del jugador
    LevelComplete // Nivel completado
}
```

### Ciclos de Update
- **Update:** Input, UI, lógica de juego
- **FixedUpdate:** Física, movimiento de vehículos
- **LateUpdate:** Cámara, efectos post-procesamiento

### Módulos/Sistemas
1. **Core:** `GameManager`, `LevelManager`, `StateManager`
2. **Player:** `PlayerController`, `Health`, `VehicleSwitcher`
3. **Vehicles:** `CarController`, `ShipController`, `VehicleBase`
4. **Input:** `InputManager`, `KinectManager`, `PoseDetector`
5. **Level:** `LevelLoader`, `PortalManager`, `HazardManager`
6. **Collision:** `CollisionHandler`, `TriggerManager`
7. **Camera:** `CameraController`, `CinemachineBrain`
8. **Audio:** `AudioManager`, `SoundEffects`
9. **UI:** `UIManager`, `HUD`, `GameOverScreen`

### Eventos Principales
- `OnVehicleChanged(VehicleType)`
- `OnPlayerDamaged(int damage)`
- `OnPlayerDied()`
- `OnPortalEntered(PortalType)`
- `OnLevelCompleted()`

## 5) Mecánicas

### Vehículos
- **Car:** Movimiento horizontal (A/D), salto (Space/W)
- **Ship:** Ascenso continuo (W), descenso (S), movimiento horizontal (A/D)

### Portales
- **PortalVehicle:** Cambia tipo de vehículo + invulnerabilidad 0.25s
- **PortalGravity:** Invierte gravedad del nivel
- **JumpPad:** Impulso vertical extra

### Sistema de Daño
- **Pinchos:** Daño letal (muerte instantánea)
- **Bloques:** Daño porcentual (25% vida)
- **OOB:** Muerte por salir de límites

### Input por Poses (Kinect)
- **Manos arriba:** Salto (Car) / Ascenso (Ship)
- **Mano izquierda extendida:** Movimiento izquierda
- **Mano derecha extendida:** Movimiento derecha
- **Ambas manos abajo:** Descenso (Ship)

## 6) Datos y Niveles

### LevelData (ScriptableObject)
```csharp
[CreateAssetMenu(fileName = "LevelData", menuName = "GeoMotor/Level Data")]
public class LevelData : ScriptableObject {
    [Header("Level Info")]
    public string levelName;
    public int levelIndex;
    
    [Header("Player")]
    public Vector3 playerSpawn;
    public VehicleType startingVehicle;
    
    [Header("Portals")]
    public List<PortalData> portals;
    public List<GravityPortalData> gravityPortals;
    
    [Header("Hazards")]
    public List<HazardData> hazards;
    public List<JumpPadData> jumpPads;
    
    [Header("Level Bounds")]
    public Vector3 levelBounds;
    public float outOfBoundsY = -10f;
}
```

### Estructura de Assets
```
Assets/
├── ScriptableObjects/
│   ├── Levels/
│   │   ├── Level_01.asset
│   │   └── Level_02.asset
│   ├── Vehicles/
│   │   ├── CarConfig.asset
│   │   └── ShipConfig.asset
│   │   └── PlaneConfig.asset
│   └── Kinect/
│       └── KinectConfig.asset
├── Prefabs/
│   ├── Vehicles/
│   │   ├── Car.prefab
│   │   ├── Ship.prefab
│   │   └── PlaneConfig.asset
│   ├── Portals/
│   │   ├── Portal_Vehicle.prefab
│   │   └── Portal_Gravity.prefab
│   └── Hazards/
│       ├── Spike.prefab
│       └── Block.prefab
└── Scenes/
    ├── MainMenu.unity
    ├── Level_01.unity
    └── Level_02.unity
```

## 7) Pipeline de Assets

### Convenciones Blender → FBX/GLB
- **Escala:** 1 metro = 1 unidad Unity
- **Ejes:** +Y arriba, +Z adelante
- **Nombres:** `Vehicle_Car.fbx`, `Portal_Vehicle.fbx`, `Hazard_Spike.fbx`
- **Materiales:** URP/Lit simples, sin shaders complejos
- **Optimización:** < 1000 tris por modelo, LOD si aplica

### Pasos de Exportación
1. Modelar en Blender con escala correcta
2. Aplicar transformaciones (Ctrl+A)
3. Exportar como FBX (Binary, +Y Forward, +Z Up)
4. Importar en Unity con escala 1:1
5. Configurar colliders y rigidbodies

## 8) Controles e Input System

### Input Actions (Player Action Map)
```json
{
  "name": "Player",
  "actions": [
    {
      "name": "Move",
      "type": "Value",
      "expectedControlType": "Vector2",
      "bindings": [
        {
          "name": "Keyboard",
          "path": "2DVector",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "Move"
        }
      ]
    },
    {
      "name": "JumpOrAscend",
      "type": "Button",
      "expectedControlType": "Button",
      "bindings": [
        {
          "name": "Space",
          "path": "<Keyboard>/space",
          "interactions": "",
          "processors": "",
          "groups": "",
          "action": "JumpOrAscend"
        }
      ]
    },
    {
      "name": "Pause",
      "type": "Button",
      "expectedControlType": "Button",
      "bindings": [
        {
          "name": "Escape",
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

### Integración Kinect
- **KinectManager:** Gestiona conexión y streams
- **PoseDetector:** Detecta poses básicas
- **KinectInputProvider:** Convierte poses en input
- **Fallback:** Teclado si Kinect no disponible

## 9) Parámetros Tunables

### VehicleConfig (ScriptableObject)
```csharp
[Header("Car Settings")]
public float carSpeed = 10f;
public float carJumpForce = 15f;
public float carGroundCheckDistance = 0.1f;

[Header("Ship Settings")]
public float shipAscendSpeed = 8f;
public float shipDescendSpeed = 12f;
public float shipMaxHeight = 20f;

[Header("Physics")]
public float gravity = -20f;
public float airResistance = 0.95f;
```

### KinectConfig (ScriptableObject)
```csharp
[Header("Pose Detection")]
public float poseHoldTime = 0.5f;
public float handRaiseThreshold = 0.3f;
public float handExtendThreshold = 0.4f;

[Header("Smoothing")]
public float jointSmoothing = 0.8f;
public int smoothingWindowSize = 5;

[Header("Performance")]
public int maxBodiesTracked = 1;
public bool enableDebugVisualization = true;
```

## 10) Estructura de Proyecto

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── LevelManager.cs
│   │   └── StateManager.cs
│   ├── Player/
│   │   ├── PlayerController.cs
│   │   ├── Health.cs
│   │   └── VehicleSwitcher.cs
│   ├── Vehicles/
│   │   ├── VehicleBase.cs
│   │   ├── CarController.cs
│   │   └── ShipController.cs
│   ├── Input/
│   │   ├── InputManager.cs
│   │   ├── KinectManager.cs
│   │   └── PoseDetector.cs
│   ├── Level/
│   │   ├── LevelLoader.cs
│   │   ├── PortalManager.cs
│   │   └── HazardManager.cs
│   ├── UI/
│   │   ├── UIManager.cs
│   │   ├── HUD.cs
│   │   └── GameOverScreen.cs
│   └── Audio/
│       └── AudioManager.cs
├── ScriptableObjects/
├── Prefabs/
├── Scenes/
├── Art/
│   ├── Models/
│   ├── Materials/
│   └── Textures/
└── Audio/
    ├── SFX/
    └── Music/
```

## 11) Plan Iterativo y Checklists

### Hito 1: Núcleo Base (A0-A1)
**Objetivo:** Proyecto funcional con player básico
- [ ] A0: Configuración inicial y GameManager
- [ ] A1: Player (Car) con movimiento y salto
- [ ] DoD: Player se mueve y salta sin errores

### Hito 2: Vehículos (A2)
**Objetivo:** Sistema de vehículos intercambiables
- [ ] A2: Ship y sistema de conmutación
- [ ] DoD: Cambio entre Car y Ship funcional

### Hito 3: Interactividad (A3-A4)
**Objetivo:** Portales, hazards y sistema de daño
- [ ] A3: Portales y triggers
- [ ] A4: Sistema de daño y muerte
- [ ] DoD: Portales funcionan, daño aplicado correctamente

### Hito 4: Datos y Configuración (A5)
**Objetivo:** Niveles parametrizables
- [ ] A5: LevelData y LevelLoader
- [ ] DoD: Nivel cargado desde ScriptableObject

### Hito 5: Input Completo (A6-A7)
**Objetivo:** Input System + Kinect
- [ ] A6: Input System (teclado/gamepad)
- [ ] A7: Integración Kinect
- [ ] DoD: Ambos sistemas de input funcionan

## 12) Plan Maestro de Construcción

### Fase 1: Fundación (Semana 1)
**A0: Núcleo y escena base**
[ ] Crear proyecto Unity 3D
[ ] Configurar URP y Input System
[ ] Crear GameManager y escena Level_01
[ ] Configurar física (gravity, timestep)
**Entregables:** Proyecto base, GameManager, escena funcional
**DoD:** Play en Editor sin errores, State=Playing

**A1: Player (Car) y movimiento**
[ ] Crear CarController con Rigidbody
[ ] Implementar auto-forward y salto
[ ] Añadir ground check y HUD básico
**Entregables:** Player.prefab, CarController, HUD vida
**DoD:** Salta solo en suelo, avanza constante, HUD refleja vida

### Fase 2: Vehículos (Semana 2)
**A2: Player (Ship)**
[ ] Crear ShipController con ascenso/descenso
[ ] Implementar PlayerVehicleSwitcher
[ ] Añadir límites de altura
**Entregables:** ShipController, conmutación estable
**DoD:** Ascenso mantenido, límites respetados, cambio estable

### Fase 3: Interactividad (Semana 3)
**A3: Portales y triggers**
[ ] Crear prefabs de portales (Vehicle, Gravity)
[ ] Implementar colliders trigger y scripts
[ ] Añadir invulnerabilidad breve
**Entregables:** Prefabs de portales, efectos inmediatos
**DoD:** Efectos inmediatos con feedback visual

**A4: Daño y hazards**
[ ] Crear sistema Health con eventos
[ ] Implementar Hazard (pinchos, bloques)
[ ] Crear flujo GameOver simple
**Entregables:** Hazards prefabs, UI GameOver
**DoD:** Pincho mata, bloque daña, GameOver/reinicio

### Fase 4: Datos (Semana 4)
**A5: Datos de nivel**
[ ] Crear LevelData ScriptableObject
[ ] Implementar LevelLoader
[ ] Aplicar configuración al cargar escena
**Entregables:** LevelData asset, LevelLoader
**DoD:** Nivel parametrizable desde asset

### Fase 5: Input Avanzado (Semana 5-6)
**A6: Input System y pausa**
[ ] Crear InputSystem_Actions.inputactions
[ ] Enlazar acciones a controladores
[ ] Implementar sistema de pausa
**Entregables:** Asset de acciones, wiring completo
**DoD:** Controles funcionan con teclado/gamepad, pausa togglable

**A7: Integración Kinect**
[ ] Instalar Azure Kinect SDK
[ ] Crear KinectManager y PoseDetector
[ ] Implementar 4 poses básicas
[ ] Integrar con Input System existente
**Entregables:** KinectManager, detección de poses
**DoD:** 4 poses detectables, fallback a teclado, latencia < 100ms

## 13) Flujo de Trabajo

### Convenciones de Desarrollo
[ ] **Commits:** `feat:`, `fix:`, `refactor:`, `docs:`
[ ] **Branches:** `feature/A1-player`, `feature/A7-kinect`
[ ] **Scripts:** PascalCase, 1 clase pública por archivo
[ ] **Prefabs:** `Vehicle_Car.prefab`, `Portal_Vehicle.prefab`

### Desarrollo Colaborativo
[ ] **Paralelo:** A0-A1, A2, A3-A4 pueden desarrollarse en paralelo
[ ] **Dependencias:** A5 requiere A3-A4, A6-A7 requieren A1-A2
[ ] **Integración:** Merge semanal, testing conjunto

### Testing y Validación
[ ] **Unit Tests:** Lógica de vehículos y poses
[ ] **Integration Tests:** Flujo completo de nivel
[ ] **Manual Tests:** Checklist por actividad en DoD

## 14) Cómo Ejecutar

### Prerrequisitos
- Unity 2021.3 LTS+
- Visual Studio 2019+ o VS Code
- Azure Kinect SDK (para A7)

### Configuración Inicial
```bash
# 1. Clonar repositorio
git clone [repo-url]
cd GeoMotor-V1

# 2. Abrir en Unity
# Unity Hub → Add → Seleccionar carpeta GeoMotor-V1

# 3. Configurar Input System
# Window → Package Manager → Input System → Install

# 4. Configurar URP
# Window → Package Manager → Universal RP → Install
```

### Ejecución por Actividad
```bash
# A0: Núcleo base
# Abrir escena Level_01, Play en Editor

# A1-A6: Desarrollo normal
# Seguir plan maestro, testing por DoD

# A7: Kinect (requiere hardware)
# Conectar Azure Kinect, ejecutar KinectManager
```

## 15) Riesgos y Decisiones Abiertas

### Riesgos Técnicos
- **Física inestable:** Usar interpolación y masas razonables
- **Latencia Kinect:** Implementar smoothing y ventanas temporales
- **Compatibilidad:** Validar drivers y GPU para Body Tracking

### Decisiones Pendientes
- **Arquitectura física:** Rigidbody vs CharacterController
- **Sistema de niveles:** ScriptableObjects vs JSON
- **Efectos visuales:** URP vs HDRP
- **Audio:** Unity Audio vs FMOD

### Mitigaciones
- **Fallback Kinect:** Sistema de input dual siempre disponible
- **Configuración:** Parámetros tunables en ScriptableObjects
- **Testing:** DoD específico por actividad

## 16) Referencias y Glosario

### Unity
- [Unity Manual](https://docs.unity3d.com/Manual/index.html)
- [Scripting API](https://docs.unity3d.com/ScriptReference/)
- [Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/index.html)
- [Physics (PhysX)](https://docs.unity3d.com/Manual/PhysicsSection.html)

### Kinect
- [Azure Kinect Body Tracking](https://learn.microsoft.com/en-us/shows/mixed-reality/azure-kinect-body-tracking-unity-integration)
- [Azure Kinect Examples for Unity](https://assetstore.unity.com/packages/tools/integration/azure-kinect-and-femto-bolt-examples-for-unity-149700)
- [LightBuzz: Azure Kinect + Unity](https://lightbuzz.com/azure-kinect-unity/)

### Glosario
- **Runner:** Género de juego con movimiento automático hacia adelante
- **Portal:** Objeto que modifica propiedades del jugador/entorno
- **OOB:** Out of Bounds, salir de los límites del nivel
- **DoD:** Definition of Done, criterios de aceptación
- **URP:** Universal Render Pipeline, pipeline de renderizado de Unity
- **ScriptableObject:** Asset de Unity para almacenar datos
- **Rigidbody:** Componente de Unity para simulación física
- **Collider:** Componente para detección de colisiones
- **Trigger:** Collider que detecta entrada sin colisión física
