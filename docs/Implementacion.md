# Implementación: Plan de Desarrollo por Sprints

Documento maestro de implementación para GeoMotor. Define sprints, actividades, tareas y pasos detallados para completar el videojuego desde inicio hasta fin.

## Estructura del Documento

- **Sprint (S):** Período de desarrollo (1-2 semanas)
- **Fase (F):** Objetivo principal del sprint (F0, F1, etc.)
- **Tarea (FX-Y):** Componente específico a desarrollar (F0-1, F1-3, etc.)
- **Acción (X-YYY):** Paso concreto y verificable (A-001, B-004, etc.)
  - **A:** Sprint S0 (Fundación)
  - **B:** Sprint S1 (Kinect)
  - **C:** Sprint S2 (Input System)
  - **D:** Sprint S3 (Portales/Interactividad)
  - **E:** Sprint S4 (Analíticas/Base de Datos)
  - **F:** Sprint S5 (Sistema de Vehículos)
  - **G:** Sprint S6 (Datos/Configuración)
  - **H:** Sprint S7 (Pulido/Optimización)
  - **I:** Sprint S8 (Entrega Final)

## Sprint S0: Fundación del Proyecto (Semana 1)

### Objetivo del Sprint
El usuario va a poder jugar en un entorno 3D estable con gráficos modernos, ver un personaje que se mueve automáticamente hacia adelante, hacer que el personaje salte presionando la barra espaciadora, ver su vida actual en la pantalla y jugar sin que el juego se cierre por errores.

### Fase F0: Núcleo y Escena Base

#### Tarea F0-1: Configuración del Proyecto Unity
[ ] **A-001:** Crear proyecto Unity 3D (2021.3 LTS+)
[ ] **A-002:** Configurar URP en Project Settings
[ ] **A-003:** Instalar Input System 1.7+ desde Package Manager
[ ] **A-004:** Configurar Physics.gravity = (0, -pi^2, 0)
[ ] **A-005:** Ajustar Fixed Timestep = 0.0166667

#### Tarea F0-2: GameManager y Estados
[ ] **A-006:** Crear script GameManager.cs con patrón Singleton
[ ] **A-007:** Implementar enum GameState (Loading, Playing, GameOver)
[ ] **A-008:** Configurar DontDestroyOnLoad en GameManager
[ ] **A-009:** Crear método SetState() con notificaciones
[ ] **A-010:** Crear prefab GameManager y añadir a escena

#### Tarea F0-3: Escena Base
[ ] **A-011:** Crear escena Level_01.unity
[ ] **A-012:** Añadir plano como suelo (escala 50x50)
[ ] **A-013:** Configurar cámara principal (posición, rotación)
[ ] **A-014:** Añadir luz direccional
[ ] **A-015:** Configurar capas (Ground, Player, Hazard, Portal)

#### Tarea F0-4: Estructura de Carpetas
[ ] **A-016:** Crear carpeta Assets/Scripts/Core
[ ] **A-017:** Crear carpeta Assets/Scripts/Player
[ ] **A-018:** Crear carpeta Assets/Scripts/Vehicles
[ ] **A-019:** Crear carpeta Assets/Prefabs
[ ] **A-020:** Crear carpeta Assets/ScriptableObjects

### Fase F1: Player (Car) y Movimiento

#### Tarea F1-1: CarController Básico
[ ] **A-021:** Crear script CarController.cs
[ ] **A-022:** Implementar auto-forward en FixedUpdate
[ ] **A-023:** Añadir Rigidbody y configurar constraints
[ ] **A-024:** Implementar ground check con raycast
[ ] **A-025:** Configurar parámetros (forwardSpeed, jumpStrength)

#### Tarea F1-2: Sistema de Salto
[ ] **A-026:** Implementar input de salto (Space/W)
[ ] **A-027:** Añadir lógica de salto solo en suelo
[ ] **A-028:** Configurar jumpStrength y groundCheckDistance
[ ] **A-029:** Añadir LayerMask para ground detection
[ ] **A-030:** Testear salto y ground check

#### Tarea F1-3: Player Prefab
[ ] **A-031:** Crear GameObject Player con Rigidbody
[ ] **A-032:** Añadir CarController al Player
[ ] **A-033:** Configurar collider (CapsuleCollider)
[ ] **A-034:** Añadir tag "Player"
[ ] **A-035:** Crear prefab Player.prefab

#### Tarea F1-4: HUD Básico
[ ] **A-036:** Crear Canvas en escena
[ ] **A-037:** Añadir Slider para vida
[ ] **A-038:** Crear script HealthBarUI.cs
[ ] **A-039:** Vincular HealthBarUI con Player
[ ] **A-040:** Testear visualización de vida

### DoD Sprint S0
**El usuario va a poder:**
- Jugar en un entorno 3D estable con gráficos modernos (URP)
- Ver un personaje que se mueve automáticamente hacia adelante
- Hacer que el personaje salte presionando la barra espaciadora
- Ver su vida actual en la pantalla
- Jugar sin que el juego se cierre por errores

**Checklist técnico:**
- [ ] Proyecto Unity configurado con URP e Input System
- [ ] GameManager funcional con estados
- [ ] Escena Level_01 con suelo y cámara
- [ ] Player con CarController y salto funcional
- [ ] HUD básico mostrando vida
- [ ] Play en Editor sin errores

---

## Sprint S1: Integración Kinect (Semana 2)

### Objetivo del Sprint
El usuario va a poder controlar el juego con movimientos corporales usando Kinect, levantar las manos para hacer que el vehículo salte/suba, bajar las manos para hacer que el vehículo baje, extender las manos hacia los lados para moverse izquierda/derecha, jugar sin Kinect usando teclado como respaldo, ajustar la sensibilidad de detección según sus preferencias y ver en pantalla cómo el sistema detecta sus movimientos.

### Fase F1: Integración Kinect

#### Tarea F1-1: Configuración de SDK
[ ] **B-001:** Instalar Azure Kinect Sensor SDK
[ ] **B-002:** Instalar Azure Kinect Body Tracking SDK
[ ] **B-003:** Verificar compatibilidad de hardware
[ ] **B-004:** Configurar drivers y permisos
[ ] **B-005:** Testear conexión del sensor

#### Tarea F1-2: KinectManager
[ ] **B-006:** Crear script KinectManager.cs
[ ] **B-007:** Implementar inicialización del sensor
[ ] **B-008:** Configurar streams (depth, body)
[ ] **B-009:** Implementar detección de cuerpos
[ ] **B-010:** Añadir manejo de errores y fallback

#### Tarea F1-3: PoseDetector
[ ] **B-011:** Crear script PoseDetector.cs
[ ] **B-012:** Implementar detección de 4 poses básicas
[ ] **B-013:** Configurar umbrales y tiempos
[ ] **B-014:** Añadir smoothing de joints
[ ] **B-015:** Testear precisión de detección

#### Tarea F1-4: KinectInputProvider
[ ] **B-016:** Crear script KinectInputProvider.cs
[ ] **B-017:** Mapear poses a acciones del juego
[ ] **B-018:** Integrar con Input System existente
[ ] **B-019:** Implementar fallback a teclado
[ ] **B-020:** Añadir configuración de sensibilidad

#### Tarea F1-5: KinectConfig
[ ] **B-021:** Crear script KinectConfig.cs (ScriptableObject)
[ ] **B-022:** Definir parámetros de detección
[ ] **B-023:** Configurar umbrales por pose
[ ] **B-024:** Añadir opciones de rendimiento
[ ] **B-025:** Crear asset KinectConfig.asset

#### Tarea F1-6: Debug y Visualización
[ ] **B-026:** Crear KinectDebugOverlay
[ ] **B-027:** Visualizar joints en tiempo real
[ ] **B-028:** Mostrar poses detectadas
[ ] **B-029:** Añadir métricas de rendimiento
[ ] **B-030:** Testear visualización en Editor

### DoD Sprint S1
**El usuario va a poder:**
- Controlar el juego con movimientos corporales usando Kinect
- Levantar las manos para hacer que el vehículo salte/suba
- Bajar las manos para hacer que el vehículo baje
- Extender las manos hacia los lados para moverse izquierda/derecha
- Jugar sin Kinect usando teclado como respaldo
- Ajustar la sensibilidad de detección según sus preferencias
- Ver en pantalla cómo el sistema detecta sus movimientos

**Checklist técnico:**
- [ ] Kinect detecta al menos 1 cuerpo
- [ ] 4 poses básicas funcionando (arriba, abajo, izquierda, derecha)
- [ ] Latencia < 100ms
- [ ] Fallback a teclado si Kinect no disponible
- [ ] Configuración parametrizable
- [ ] Debug overlay funcional

---

## Sprint S2: Input System Completo (Semana 3)

### Objetivo del Sprint
El usuario va a poder jugar con teclado o gamepad según su preferencia, pausar el juego en cualquier momento presionando Escape, personalizar sus controles según sus gustos, recibir feedback visual y táctil cuando presiona botones, tener sus configuraciones guardadas automáticamente y experimentar controles responsivos sin retrasos.

### Fase F2: Input System y Pausa

#### Tarea F2-1: Input Actions Asset
[ ] **C-001:** Crear InputSystem_Actions.inputactions
[ ] **C-002:** Definir Action Map "Player"
[ ] **C-003:** Configurar acciones Move, JumpOrAscend, Pause
[ ] **C-004:** Añadir bindings para teclado
[ ] **C-005:** Añadir bindings para gamepad

#### Tarea F2-2: InputManager
[ ] **C-006:** Crear script InputManager.cs
[ ] **C-007:** Implementar PlayerInput component
[ ] **C-008:** Configurar comportamiento "Invoke Unity Events"
[ ] **C-009:** Vincular eventos con controladores
[ ] **C-010:** Testear input de teclado y gamepad

#### Tarea F2-3: Sistema de Pausa
[ ] **C-011:** Implementar lógica de pausa en GameManager
[ ] **C-012:** Crear UI de pausa
[ ] **C-013:** Añadir botones continuar/salir
[ ] **C-014:** Configurar Time.timeScale
[ ] **C-015:** Testear pausa y reanudación

#### Tarea F2-4: Reasignación de Controles
[ ] **C-016:** Implementar sistema de rebinding
[ ] **C-017:** Crear UI de configuración
[ ] **C-018:** Guardar configuración en PlayerPrefs
[ ] **C-019:** Cargar configuración al inicio
[ ] **C-020:** Testear reasignación en runtime

#### Tarea F2-5: Feedback de Input
[ ] **C-021:** Añadir indicadores visuales de input
[ ] **C-022:** Implementar rumble en gamepad
[ ] **C-023:** Crear efectos de sonido de input
[ ] **C-024:** Testear feedback en todos los dispositivos
[ ] **C-025:** Optimizar latencia de input

### DoD Sprint S2
**El usuario va a poder:**
- Jugar con teclado o gamepad según su preferencia
- Pausar el juego en cualquier momento presionando Escape
- Personalizar sus controles según sus gustos
- Recibir feedback visual y táctil cuando presiona botones
- Sus configuraciones se guardan automáticamente
- Experimentar controles responsivos sin retrasos

**Checklist técnico:**
- [ ] Input System funciona con teclado y gamepad
- [ ] Pausa togglable y funcional
- [ ] Controles reasignables en runtime
- [ ] Feedback de input implementado
- [ ] Configuración guardada/cargada
- [ ] Latencia de input < 16ms

---

## Sprint S3: Interactividad y Portales (Semana 4)

### Objetivo del Sprint
El usuario va a poder pasar por portales que cambian su vehículo automáticamente, ver efectos visuales cuando toca portales y otros elementos, recibir daño al tocar obstáculos (pinchos matan, bloques dañan), ver una pantalla de Game Over cuando muere, reiniciar el juego fácilmente después de morir, ser protegido brevemente después de cambiar de vehículo y morir si sale de los límites del nivel.

### Fase F3: Portales y Triggers

#### Tarea F3-1: PortalVehicle
[ ] **D-001:** Crear script PortalVehicle.cs
[ ] **D-002:** Implementar OnTriggerEnter
[ ] **D-003:** Añadir parámetro target (VehicleKind)
[ ] **D-004:** Configurar invulnerabilidad (invulnSeconds)
[ ] **D-005:** Crear prefab Portal_Vehicle.prefab

#### Tarea F3-2: PortalGravity
[ ] **D-006:** Crear script PortalGravity.cs
[ ] **D-007:** Implementar cambio de Physics.gravity
[ ] **D-008:** Añadir parámetro gravity (Vector3)
[ ] **D-009:** Configurar trigger detection
[ ] **D-010:** Crear prefab Portal_Gravity.prefab

#### Tarea F3-3: JumpPad
[ ] **D-011:** Crear script JumpPad.cs
[ ] **D-012:** Implementar impulso vertical
[ ] **D-013:** Configurar parámetro strength
[ ] **D-014:** Añadir efecto visual básico
[ ] **D-015:** Crear prefab JumpPad.prefab

#### Tarea F3-4: Sistema de Invulnerabilidad
[ ] **D-016:** Implementar corrutina Invuln() en PlayerVehicleSwitcher
[ ] **D-017:** Añadir variable invulnerable
[ ] **D-018:** Configurar duración de invulnerabilidad
[ ] **D-019:** Añadir efecto visual de invulnerabilidad
[ ] **D-020:** Testear protección contra daño

#### Tarea F3-5: Efectos Visuales de Portales
[ ] **D-021:** Crear materiales para portales
[ ] **D-022:** Añadir partículas básicas
[ ] **D-023:** Implementar animación de rotación
[ ] **D-024:** Configurar colores por tipo de portal
[ ] **D-025:** Testear feedback visual

### Fase F4: Daño y Hazards

#### Tarea F4-1: Sistema de Salud
[ ] **D-026:** Crear script Health.cs
[ ] **D-027:** Implementar current y max health
[ ] **D-028:** Añadir métodos ApplyFlat() y ApplyPercent()
[ ] **D-029:** Implementar evento OnDied
[ ] **D-030:** Vincular con Player

#### Tarea F4-2: Hazard System
[ ] **D-031:** Crear script Hazard.cs
[ ] **D-032:** Implementar parámetros lethal y percentDamage
[ ] **D-033:** Configurar OnTriggerEnter
[ ] **D-034:** Crear prefabs Spike.prefab y Block.prefab
[ ] **D-035:** Testear diferentes tipos de daño

#### Tarea F4-3: GameOver System
[ ] **D-036:** Crear script GameOverManager.cs
[ ] **D-037:** Implementar lógica de muerte
[ ] **D-038:** Crear UI GameOverScreen
[ ] **D-039:** Añadir botón de reinicio
[ ] **D-040:** Vincular con GameManager

#### Tarea F4-4: Out of Bounds
[ ] **D-041:** Crear script OutOfBounds.cs
[ ] **D-042:** Implementar detección de límites
[ ] **D-043:** Configurar parámetro outOfBoundsY
[ ] **D-044:** Añadir trigger invisible
[ ] **D-045:** Testear muerte por OOB

### DoD Sprint S3
**El usuario va a poder:**
- Pasar por portales que cambian su vehículo automáticamente
- Ver efectos visuales cuando toca portales y otros elementos
- Recibir daño al tocar obstáculos (pinchos matan, bloques dañan)
- Ver una pantalla de Game Over cuando muere
- Reiniciar el juego fácilmente después de morir
- Ser protegido brevemente después de cambiar de vehículo
- Morir si sale de los límites del nivel

**Checklist técnico:**
- [ ] Portales funcionan con efectos inmediatos
- [ ] Sistema de daño aplicado correctamente
- [ ] GameOver y reinicio funcional
- [ ] Invulnerabilidad protege contra daño
- [ ] OOB detecta salida de límites
- [ ] Feedback visual en todos los elementos

---

## Sprint S4: Analíticas y Base de Datos (Semana 5)

### Objetivo del Sprint
El usuario va a poder ver sus progresos de actividad física en tiempo real, recibir feedback sobre calorías quemadas y tiempo de movimiento, acceder a un historial de partidas con métricas de salud, recibir recomendaciones personalizadas para mejorar su actividad física, y sentir que está jugando un videojuego que realmente mejora su salud y bienestar.

### Fase F4: Sistema de Datos de Salud

#### Tarea F4-1: Configuración de Supabase
[ ] **E-001:** Configurar proyecto Supabase
[ ] **E-002:** Crear tablas para datos de usuario y partidas
[ ] **E-003:** Implementar conexión desde Unity
[ ] **E-004:** Testear conectividad/permisos
[ ] **E-005:** Habilitar tiempo real

#### Tarea F4-2: DataCollector
[ ] **E-006:** Crear script DataCollector.cs
[ ] **E-007:** Registrar movimientos y poses detectadas
[ ] **E-008:** Implementar tracking de tiempo de reacción
[ ] **E-009:** Calcular calorías quemadas (estimación)
[ ] **E-010:** Guardar métricas de actividad física

#### Tarea F4-3: HealthMetrics
[ ] **E-011:** Crear script HealthMetrics.cs
[ ] **E-012:** Implementar cálculo de calorías por movimiento
[ ] **E-013:** Añadir métricas de tiempo sedentario vs activo
[ ] **E-014:** Calcular beneficios cardiovasculares
[ ] **E-015:** Generar recomendaciones de salud

#### Tarea F4-4: AnalyticsUI
[ ] **E-016:** Crear pantalla de analíticas post-partida
[ ] **E-017:** Mostrar calorías quemadas y tiempo activo
[ ] **E-018:** Visualizar progreso histórico
[ ] **E-019:** Añadir comparativas con objetivos
[ ] **E-020:** Implementar gráficos de tendencias

#### Tarea F4-5: HealthDashboard
[ ] **E-021:** Crear dashboard principal de salud
[ ] **E-022:** Mostrar estadísticas semanales/mensuales
[ ] **E-023:** Añadir logros y badges de salud
[ ] **E-024:** Implementar notificaciones de bienestar
[ ] **E-025:** Crear sistema de objetivos personalizados

### DoD Sprint S4
**El usuario va a poder:**
- Ver cuántas calorías quemó en cada partida
- Acceder a un historial completo de su actividad física
- Recibir feedback inmediato sobre su tiempo de movimiento
- Ver recomendaciones personalizadas para mejorar su salud
- Sentir que está jugando un videojuego que realmente mejora su bienestar
- Comparar su progreso con objetivos de salud establecidos

**Checklist técnico:**
- [ ] Conexión a Supabase funcional y segura
- [ ] Datos de actividad física se guardan correctamente
- [ ] Cálculo de calorías y métricas de salud implementado
- [ ] UI de analíticas muestra datos relevantes
- [ ] Dashboard de salud funcional y atractivo
- [ ] Sistema de recomendaciones personalizadas

---

## Sprint S5: Sistema de Vehículos (Semana 6)

### Objetivo del Sprint
El usuario va a poder cambiar entre dos tipos de vehículos: un carro (que salta) y una nave (que vuela), controlar la nave subiendo y bajando con las teclas W y S, ver en la pantalla qué vehículo está usando actualmente, jugar con físicas estables sin que los vehículos se comporten de forma extraña y experimentar dos estilos de juego diferentes en el mismo personaje.

### Fase F5: Player (Ship)

#### Tarea F5-1: ShipController
[ ] **F-001:** Crear script ShipController.cs
[ ] **F-002:** Implementar ascenso continuo (W/Space)
[ ] **F-003:** Implementar descenso (S)
[ ] **F-004:** Configurar límites de velocidad vertical
[ ] **F-005:** Añadir auto-forward igual que Car

#### Tarea F5-2: Físicas del Ship
[ ] **F-006:** Configurar Rigidbody para Ship
[ ] **F-007:** Implementar verticalAccel y maxVerticalSpeed
[ ] **F-008:** Añadir límites de altura (shipMaxHeight)
[ ] **F-009:** Configurar airResistance para Ship
[ ] **F-010:** Testear físicas de ascenso/descenso

#### Tarea F5-3: PlayerVehicleSwitcher
[ ] **F-011:** Crear script PlayerVehicleSwitcher.cs
[ ] **F-012:** Implementar referencias a Car y Ship
[ ] **F-013:** Crear método SwitchTo(VehicleKind)
[ ] **F-014:** Implementar activación/desactivación de controladores
[ ] **F-015:** Añadir evento OnVehicleChanged

#### Tarea F5-4: Integración de Vehículos
[ ] **F-016:** Añadir ShipController al Player prefab
[ ] **F-017:** Configurar PlayerVehicleSwitcher
[ ] **F-018:** Testear conmutación manual entre vehículos
[ ] **F-019:** Ajustar parámetros de ambos vehículos
[ ] **F-020:** Verificar estabilidad física

#### Tarea F5-5: UI de Vehículo Actual
[ ] **F-021:** Añadir icono de vehículo al HUD
[ ] **F-022:** Crear script VehicleIconUI.cs
[ ] **F-023:** Vincular con PlayerVehicleSwitcher
[ ] **F-024:** Testear cambio visual de icono
[ ] **F-025:** Ajustar diseño del HUD

### DoD Sprint S5
**El usuario va a poder:**
- Cambiar entre dos tipos de vehículos: un carro (que salta) y una nave (que vuela)
- Controlar la nave subiendo y bajando con las teclas W y S
- Ver en la pantalla qué vehículo está usando actualmente
- Jugar con físicas estables sin que los vehículos se comporten de forma extraña
- Experimentar dos estilos de juego diferentes en el mismo personaje

**Checklist técnico:**
- [ ] ShipController funcional con ascenso/descenso
- [ ] Conmutación estable entre Car y Ship
- [ ] Límites de altura respetados
- [ ] HUD muestra vehículo actual
- [ ] Físicas estables en ambos vehículos
- [ ] Eventos de cambio de vehículo funcionando

---

## Sprint S6: Datos y Configuración (Semana 7)

### Objetivo del Sprint
El usuario va a poder jugar en niveles que se configuran automáticamente sin necesidad de programar, experimentar diferentes configuraciones de gravedad y parámetros, disfrutar de un juego más estable y configurable, mientras que los desarrolladores pueden crear nuevos niveles fácilmente sin tocar código y el juego se adapta automáticamente a diferentes configuraciones.

### Fase F6: Datos de Nivel

#### Tarea F6-1: LevelData ScriptableObject
[ ] **G-001:** Crear script LevelData.cs
[ ] **G-002:** Implementar estructuras de datos (PortalData, HazardData, etc.)
[ ] **G-003:** Añadir CreateAssetMenu
[ ] **G-004:** Configurar campos serializables
[ ] **G-005:** Crear asset Level_01.asset

#### Tarea F6-2: LevelLoader
[ ] **G-006:** Crear script LevelLoader.cs
[ ] **G-007:** Implementar carga de LevelData
[ ] **G-008:** Aplicar configuración de gravedad
[ ] **G-009:** Posicionar player en spawn
[ ] **G-010:** Instanciar elementos del nivel

#### Tarea F6-3: GlobalConfig
[ ] **G-011:** Crear script GlobalConfig.cs
[ ] **G-012:** Definir parámetros tunables
[ ] **G-013:** Crear asset GlobalConfig.asset
[ ] **G-014:** Implementar ApplyGlobalConfig.cs
[ ] **G-015:** Vincular con controladores

#### Tarea F6-4: Instanciación Dinámica
[ ] **G-016:** Crear prefabs para todos los elementos
[ ] **G-017:** Implementar instanciación desde LevelData
[ ] **G-018:** Configurar posiciones y parámetros
[ ] **G-019:** Testear carga completa de nivel
[ ] **G-020:** Optimizar rendimiento de instanciación

#### Tarea F6-5: Validación de Datos
[ ] **G-021:** Implementar validación en LevelData
[ ] **G-022:** Añadir checks de rangos válidos
[ ] **G-023:** Crear editor custom si necesario
[ ] **G-024:** Testear casos edge
[ ] **G-025:** Documentar estructura de datos

### DoD Sprint S6
**El usuario va a poder:**
- Jugar en niveles que se configuran automáticamente sin necesidad de programar
- Experimentar diferentes configuraciones de gravedad y parámetros
- Disfrutar de un juego más estable y configurable
- Los desarrolladores pueden crear nuevos niveles fácilmente sin tocar código
- El juego se adapta automáticamente a diferentes configuraciones

**Checklist técnico:**
- [ ] LevelData parametriza completamente el nivel
- [ ] LevelLoader aplica configuración correctamente
- [ ] GlobalConfig permite tuning de parámetros
- [ ] Instanciación dinámica funciona
- [ ] Datos validados y sin errores
- [ ] Nivel cargado desde asset sin tocar escena

---

## Sprint S7: Pulido y Optimización (Semana 8)

### Objetivo del Sprint
El usuario va a poder disfrutar de efectos visuales atractivos (partículas, animaciones), escuchar efectos de sonido y música que mejoran la experiencia, usar una interfaz pulida y fácil de entender, jugar sin problemas de rendimiento (60 FPS estables), experimentar un juego completo sin errores molestos y sentir que está jugando un producto terminado y profesional.

### Fase F7: Pulido General

#### Tarea F7-1: Efectos Visuales
[ ] **H-001:** Implementar partículas para portales
[ ] **H-002:** Añadir efectos de salto y movimiento
[ ] **H-003:** Crear efectos de daño y muerte
[ ] **H-004:** Implementar post-processing básico
[ ] **H-005:** Optimizar efectos para rendimiento

#### Tarea F7-2: Sistema de Audio
[ ] **H-006:** Crear AudioManager.cs
[ ] **H-007:** Implementar SFX para acciones
[ ] **H-008:** Añadir música de fondo
[ ] **H-009:** Configurar mezcla de audio
[ ] **H-010:** Testear en diferentes dispositivos

#### Tarea F7-3: UI/UX Final
[ ] **H-011:** Diseñar HUD completo
[ ] **H-012:** Crear menú principal
[ ] **H-013:** Implementar pantallas de transición
[ ] **H-014:** Añadir animaciones de UI
[ ] **H-015:** Testear usabilidad

#### Tarea F7-4: Optimización
[ ] **H-016:** Profilar rendimiento
[ ] **H-017:** Optimizar scripts críticos
[ ] **H-018:** Reducir draw calls
[ ] **H-019:** Optimizar física
[ ] **H-020:** Testear en hardware objetivo

#### Tarea F7-5: Testing Final
[ ] **H-021:** Testing de funcionalidad completa
[ ] **H-022:** Testing de input (teclado + Kinect)
[ ] **H-023:** Testing de rendimiento
[ ] **H-024:** Testing de usabilidad
[ ] **H-025:** Corrección de bugs finales

### DoD Sprint S7
**El usuario va a poder:**
- Disfrutar de efectos visuales atractivos (partículas, animaciones)
- Escuchar efectos de sonido y música que mejoran la experiencia
- Usar una interfaz pulida y fácil de entender
- Jugar sin problemas de rendimiento (60 FPS estables)
- Experimentar un juego completo sin errores molestos
- Sentir que está jugando un producto terminado y profesional

**Checklist técnico:**
- [ ] Efectos visuales implementados y optimizados
- [ ] Sistema de audio completo
- [ ] UI/UX pulida y funcional
- [ ] Rendimiento objetivo alcanzado (60 FPS)
- [ ] Testing completo sin bugs críticos
- [ ] Experiencia de juego pulida

---

## Sprint S8: Entrega Final (Semana 9)

### Objetivo del Sprint
El usuario va a poder descargar e instalar el juego fácilmente, encontrar documentación clara sobre cómo jugar, configurar Kinect siguiendo guías paso a paso, resolver problemas comunes con la guía de troubleshooting, disfrutar de un juego completamente funcional y entregable, mientras que los desarrolladores tienen todo organizado para futuras mejoras.

### Fase F8: Entrega Final

#### Tarea F8-1: Build Final
[ ] **I-001:** Configurar build settings
[ ] **I-002:** Optimizar para plataforma objetivo
[ ] **I-003:** Generar build ejecutable
[ ] **I-004:** Testear build final
[ ] **I-005:** Crear instalador si es necesario

#### Tarea F8-2: Documentación Final
[ ] **I-006:** Actualizar README.md
[ ] **I-007:** Crear manual de usuario
[ ] **I-008:** Documentar configuración de Kinect
[ ] **I-009:** Crear guía de instalación
[ ] **I-010:** Documentar troubleshooting

#### Tarea F8-3: Assets Finales
[ ] **I-011:** Organizar assets finales
[ ] **I-012:** Crear prefabs finales
[ ] **I-013:** Configurar ScriptableObjects finales
[ ] **I-014:** Optimizar assets para build
[ ] **I-015:** Verificar integridad de assets

#### Tarea F8-4: Presentación
[ ] **I-016:** Crear video de demostración
[ ] **I-017:** Preparar presentación del proyecto
[ ] **I-018:** Documentar características implementadas
[ ] **I-019:** Preparar Q&A
[ ] **I-020:** Revisar entrega completa

### DoD Sprint S8
**El usuario va a poder:**
- Descargar e instalar el juego fácilmente
- Encontrar documentación clara sobre cómo jugar
- Configurar Kinect siguiendo guías paso a paso
- Resolver problemas comunes con la guía de troubleshooting
- Disfrutar de un juego completamente funcional y entregable
- Los desarrolladores tienen todo organizado para futuras mejoras

**Checklist técnico:**
- [ ] Build final funcional
- [ ] Documentación completa
- [ ] Assets organizados y optimizados
- [ ] Presentación preparada
- [ ] Proyecto listo para entrega
- [ ] Todas las características MVP implementadas

---

## Resumen de Entregables por Sprint

| Sprint | Entregables Principales | DoD |
|--------|------------------------|-----|
| S0 | Proyecto base, GameManager, Player básico | Player funcional con salto |
| S1 | Kinect, detección de poses | Input dual funcional |
| S2 | Input System, pausa, reasignación | Input completo |
| S3 | Portales, hazards, sistema de daño | Interactividad completa |
| S4 | Analíticas, Supabase, métricas de salud | Videojuego serio funcional |
| S5 | Sistema de vehículos, conmutación | Car ↔ Ship estable |
| S6 | LevelData, configuración parametrizable | Niveles desde assets |
| S7 | Efectos, audio, UI, optimización | Experiencia pulida |
| S8 | Build final, documentación | Proyecto entregable |

## Notas de Desarrollo

- **Dependencias:** Cada sprint depende del anterior
- **Paralelización:** Algunas tareas pueden desarrollarse en paralelo
- **Testing:** Cada DoD debe validarse antes de continuar
- **Flexibilidad:** El plan puede ajustarse según necesidades
- **Colaboración:** Múltiples desarrolladores pueden trabajar en diferentes tareas

---

## Consideraciones de Diseño

### **Cambio de Vehículos por Poses (No Portales)**
Si el Kinect es muy bueno reconociendo poses, entonces no sería necesario usar portales. Con cambiar de pose se cambia de vehículo y se puede sobrevivir en dicho entorno (tierra/agua). Esto simplifica la mecánica y la hace más intuitiva para actividad física.

### **Nitros en lugar de JumpPads**
Los pads realmente son nitros que permiten al auto meter acelerón en el aire o al submarino subir muy rápido. Esto añade más dinamismo y requiere más movimiento físico del usuario.

### **Geometría Voxel y Navegación Diagonal**
La geometría del escenario pueden ser vóxels, el vehículo navega en diagonal al escenario y así es más fácil colocar los bloques pues la esquina/punta es fácil de hacer sea la colisión. Esto simplifica el diseño de niveles y mejora el rendimiento.

### **Enfoque en Videojuego Serio**
Este es un videojuego serio que busca mejorar la salud de las personas. No es solo entretenimiento, sino una herramienta para actividad física, evitar enfermedades por sedentarismo, aliviar articulaciones y promover el movimiento saludable.
