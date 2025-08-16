# Implementación: Plan de Desarrollo por Sprints

Documento maestro de implementación para GeoMotor. Define sprints, actividades, tareas y pasos detallados para completar el videojuego desde inicio hasta fin.

## Estructura del Documento

- **Sprint (S):** Período de desarrollo (1-2 semanas)
- **Fase (F):** Objetivo principal del sprint (F0, F1, etc.)
- **Tarea (T):** Componente específico a desarrollar
- **Acción (A):** Paso concreto y verificable (A-001, A-002, etc.)

## Sprint S0: Fundación del Proyecto (Semana 1)

### Objetivo del Sprint
El usuario va a poder jugar en un entorno 3D estable con gráficos modernos, ver un personaje que se mueve automáticamente hacia adelante, hacer que el personaje salte presionando la barra espaciadora, ver su vida actual en la pantalla y jugar sin que el juego se cierre por errores.

### Fase F0: Núcleo y Escena Base

#### Tarea T0: Configuración del Proyecto Unity
[ ] **A-001:** Crear proyecto Unity 3D (2021.3 LTS+)
[ ] **A-002:** Configurar URP en Project Settings
[ ] **A-003:** Instalar Input System 1.7+ desde Package Manager
[ ] **A-004:** Configurar Physics.gravity = (0, -pi^2, 0)
[ ] **A-005:** Ajustar Fixed Timestep = 0.0166667

#### Tarea T1: GameManager y Estados
[ ] **A-006:** Crear script GameManager.cs con patrón Singleton
[ ] **A-007:** Implementar enum GameState (Loading, Playing, GameOver)
[ ] **A-008:** Configurar DontDestroyOnLoad en GameManager
[ ] **A-009:** Crear método SetState() con notificaciones
[ ] **A-010:** Crear prefab GameManager y añadir a escena

#### Tarea T2: Escena Base
[ ] **A-011:** Crear escena Level_01.unity
[ ] **A-012:** Añadir plano como suelo (escala 50x50)
[ ] **A-013:** Configurar cámara principal (posición, rotación)
[ ] **A-014:** Añadir luz direccional
[ ] **A-015:** Configurar capas (Ground, Player, Hazard, Portal)

#### Tarea T3: Estructura de Carpetas
[ ] **A-016:** Crear carpeta Assets/Scripts/Core
[ ] **A-017:** Crear carpeta Assets/Scripts/Player
[ ] **A-018:** Crear carpeta Assets/Scripts/Vehicles
[ ] **A-019:** Crear carpeta Assets/Prefabs
[ ] **A-020:** Crear carpeta Assets/ScriptableObjects

### Fase F1: Player (Car) y Movimiento

#### Tarea T4: CarController Básico
[ ] **A-021:** Crear script CarController.cs
[ ] **A-022:** Implementar auto-forward en FixedUpdate
[ ] **A-023:** Añadir Rigidbody y configurar constraints
[ ] **A-024:** Implementar ground check con raycast
[ ] **A-025:** Configurar parámetros (forwardSpeed, jumpStrength)

#### Tarea T5: Sistema de Salto
[ ] **A-026:** Implementar input de salto (Space/W)
[ ] **A-027:** Añadir lógica de salto solo en suelo
[ ] **A-028:** Configurar jumpStrength y groundCheckDistance
[ ] **A-029:** Añadir LayerMask para ground detection
[ ] **A-030:** Testear salto y ground check

#### Tarea T6: Player Prefab
[ ] **A-031:** Crear GameObject Player con Rigidbody
[ ] **A-032:** Añadir CarController al Player
[ ] **A-033:** Configurar collider (CapsuleCollider)
[ ] **A-034:** Añadir tag "Player"
[ ] **A-035:** Crear prefab Player.prefab

#### Tarea T7: HUD Básico
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

#### Tarea T8: Configuración de SDK
[ ] **A-041:** Instalar Azure Kinect Sensor SDK
[ ] **A-042:** Instalar Azure Kinect Body Tracking SDK
[ ] **A-043:** Verificar compatibilidad de hardware
[ ] **A-044:** Configurar drivers y permisos
[ ] **A-045:** Testear conexión del sensor

#### Tarea T9: KinectManager
[ ] **A-046:** Crear script KinectManager.cs
[ ] **A-047:** Implementar inicialización del sensor
[ ] **A-048:** Configurar streams (depth, body)
[ ] **A-049:** Implementar detección de cuerpos
[ ] **A-050:** Añadir manejo de errores y fallback

#### Tarea T10: PoseDetector
[ ] **A-051:** Crear script PoseDetector.cs
[ ] **A-052:** Implementar detección de 4 poses básicas
[ ] **A-053:** Configurar umbrales y tiempos
[ ] **A-054:** Añadir smoothing de joints
[ ] **A-055:** Testear precisión de detección

#### Tarea T11: KinectInputProvider
[ ] **A-056:** Crear script KinectInputProvider.cs
[ ] **A-057:** Mapear poses a acciones del juego
[ ] **A-058:** Integrar con Input System existente
[ ] **A-059:** Implementar fallback a teclado
[ ] **A-060:** Añadir configuración de sensibilidad

#### Tarea T12: KinectConfig
[ ] **A-061:** Crear script KinectConfig.cs (ScriptableObject)
[ ] **A-062:** Definir parámetros de detección
[ ] **A-063:** Configurar umbrales por pose
[ ] **A-064:** Añadir opciones de rendimiento
[ ] **A-065:** Crear asset KinectConfig.asset

#### Tarea T13: Debug y Visualización
[ ] **A-066:** Crear KinectDebugOverlay
[ ] **A-067:** Visualizar joints en tiempo real
[ ] **A-068:** Mostrar poses detectadas
[ ] **A-069:** Añadir métricas de rendimiento
[ ] **A-070:** Testear visualización en Editor

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

#### Tarea T14: Input Actions Asset
[ ] **A-071:** Crear InputSystem_Actions.inputactions
[ ] **A-072:** Definir Action Map "Player"
[ ] **A-073:** Configurar acciones Move, JumpOrAscend, Pause
[ ] **A-074:** Añadir bindings para teclado
[ ] **A-075:** Añadir bindings para gamepad

#### Tarea T15: InputManager
[ ] **A-076:** Crear script InputManager.cs
[ ] **A-077:** Implementar PlayerInput component
[ ] **A-078:** Configurar comportamiento "Invoke Unity Events"
[ ] **A-079:** Vincular eventos con controladores
[ ] **A-080:** Testear input de teclado y gamepad

#### Tarea T16: Sistema de Pausa
[ ] **A-081:** Implementar lógica de pausa en GameManager
[ ] **A-082:** Crear UI de pausa
[ ] **A-083:** Añadir botones continuar/salir
[ ] **A-084:** Configurar Time.timeScale
[ ] **A-085:** Testear pausa y reanudación

#### Tarea T17: Reasignación de Controles
[ ] **A-086:** Implementar sistema de rebinding
[ ] **A-087:** Crear UI de configuración
[ ] **A-088:** Guardar configuración en PlayerPrefs
[ ] **A-089:** Cargar configuración al inicio
[ ] **A-090:** Testear reasignación en runtime

#### Tarea T18: Feedback de Input
[ ] **A-091:** Añadir indicadores visuales de input
[ ] **A-092:** Implementar rumble en gamepad
[ ] **A-093:** Crear efectos de sonido de input
[ ] **A-094:** Testear feedback en todos los dispositivos
[ ] **A-095:** Optimizar latencia de input

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

#### Tarea T19: PortalVehicle
[ ] **A-096:** Crear script PortalVehicle.cs
[ ] **A-097:** Implementar OnTriggerEnter
[ ] **A-098:** Añadir parámetro target (VehicleKind)
[ ] **A-099:** Configurar invulnerabilidad (invulnSeconds)
[ ] **A-100:** Crear prefab Portal_Vehicle.prefab

#### Tarea T20: PortalGravity
[ ] **A-101:** Crear script PortalGravity.cs
[ ] **A-102:** Implementar cambio de Physics.gravity
[ ] **A-103:** Añadir parámetro gravity (Vector3)
[ ] **A-104:** Configurar trigger detection
[ ] **A-105:** Crear prefab Portal_Gravity.prefab

#### Tarea T21: JumpPad
[ ] **A-106:** Crear script JumpPad.cs
[ ] **A-107:** Implementar impulso vertical
[ ] **A-108:** Configurar parámetro strength
[ ] **A-109:** Añadir efecto visual básico
[ ] **A-110:** Crear prefab JumpPad.prefab

#### Tarea T22: Sistema de Invulnerabilidad
[ ] **A-111:** Implementar corrutina Invuln() en PlayerVehicleSwitcher
[ ] **A-112:** Añadir variable invulnerable
[ ] **A-113:** Configurar duración de invulnerabilidad
[ ] **A-114:** Añadir efecto visual de invulnerabilidad
[ ] **A-115:** Testear protección contra daño

#### Tarea T23: Efectos Visuales de Portales
[ ] **A-116:** Crear materiales para portales
[ ] **A-117:** Añadir partículas básicas
[ ] **A-118:** Implementar animación de rotación
[ ] **A-119:** Configurar colores por tipo de portal
[ ] **A-120:** Testear feedback visual

### Fase F4: Daño y Hazards

#### Tarea T24: Sistema de Salud
[ ] **A-121:** Crear script Health.cs
[ ] **A-122:** Implementar current y max health
[ ] **A-123:** Añadir métodos ApplyFlat() y ApplyPercent()
[ ] **A-124:** Implementar evento OnDied
[ ] **A-125:** Vincular con Player

#### Tarea T25: Hazard System
[ ] **A-126:** Crear script Hazard.cs
[ ] **A-127:** Implementar parámetros lethal y percentDamage
[ ] **A-128:** Configurar OnTriggerEnter
[ ] **A-129:** Crear prefabs Spike.prefab y Block.prefab
[ ] **A-130:** Testear diferentes tipos de daño

#### Tarea T26: GameOver System
[ ] **A-131:** Crear script GameOverManager.cs
[ ] **A-132:** Implementar lógica de muerte
[ ] **A-133:** Crear UI GameOverScreen
[ ] **A-134:** Añadir botón de reinicio
[ ] **A-135:** Vincular con GameManager

#### Tarea T27: Out of Bounds
[ ] **A-136:** Crear script OutOfBounds.cs
[ ] **A-137:** Implementar detección de límites
[ ] **A-138:** Configurar parámetro outOfBoundsY
[ ] **A-139:** Añadir trigger invisible
[ ] **A-140:** Testear muerte por OOB

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

## Sprint S4: Sistema de Vehículos (Semana 5)

### Objetivo del Sprint
El usuario va a poder cambiar entre dos tipos de vehículos: un carro (que salta) y una nave (que vuela), controlar la nave subiendo y bajando con las teclas W y S, ver en la pantalla qué vehículo está usando actualmente, jugar con físicas estables sin que los vehículos se comporten de forma extraña y experimentar dos estilos de juego diferentes en el mismo personaje.

### Fase F4: Player (Ship)

#### Tarea T28: ShipController
[ ] **A-141:** Crear script ShipController.cs
[ ] **A-142:** Implementar ascenso continuo (W/Space)
[ ] **A-143:** Implementar descenso (S)
[ ] **A-144:** Configurar límites de velocidad vertical
[ ] **A-145:** Añadir auto-forward igual que Car

#### Tarea T29: Físicas del Ship
[ ] **A-146:** Configurar Rigidbody para Ship
[ ] **A-147:** Implementar verticalAccel y maxVerticalSpeed
[ ] **A-148:** Añadir límites de altura (shipMaxHeight)
[ ] **A-149:** Configurar airResistance para Ship
[ ] **A-150:** Testear físicas de ascenso/descenso

#### Tarea T30: PlayerVehicleSwitcher
[ ] **A-151:** Crear script PlayerVehicleSwitcher.cs
[ ] **A-152:** Implementar referencias a Car y Ship
[ ] **A-153:** Crear método SwitchTo(VehicleKind)
[ ] **A-154:** Implementar activación/desactivación de controladores
[ ] **A-155:** Añadir evento OnVehicleChanged

#### Tarea T31: Integración de Vehículos
[ ] **A-156:** Añadir ShipController al Player prefab
[ ] **A-157:** Configurar PlayerVehicleSwitcher
[ ] **A-158:** Testear conmutación manual entre vehículos
[ ] **A-159:** Ajustar parámetros de ambos vehículos
[ ] **A-160:** Verificar estabilidad física

#### Tarea T32: UI de Vehículo Actual
[ ] **A-161:** Añadir icono de vehículo al HUD
[ ] **A-162:** Crear script VehicleIconUI.cs
[ ] **A-163:** Vincular con PlayerVehicleSwitcher
[ ] **A-164:** Testear cambio visual de icono
[ ] **A-165:** Ajustar diseño del HUD

### DoD Sprint S4
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

## Sprint S5: Datos y Configuración (Semana 6)

### Objetivo del Sprint
El usuario va a poder jugar en niveles que se configuran automáticamente sin necesidad de programar, experimentar diferentes configuraciones de gravedad y parámetros, disfrutar de un juego más estable y configurable, mientras que los desarrolladores pueden crear nuevos niveles fácilmente sin tocar código y el juego se adapta automáticamente a diferentes configuraciones.

### Fase F5: Datos de Nivel

#### Tarea T33: LevelData ScriptableObject
[ ] **A-166:** Crear script LevelData.cs
[ ] **A-167:** Implementar estructuras de datos (PortalData, HazardData, etc.)
[ ] **A-168:** Añadir CreateAssetMenu
[ ] **A-169:** Configurar campos serializables
[ ] **A-170:** Crear asset Level_01.asset

#### Tarea T34: LevelLoader
[ ] **A-171:** Crear script LevelLoader.cs
[ ] **A-172:** Implementar carga de LevelData
[ ] **A-173:** Aplicar configuración de gravedad
[ ] **A-174:** Posicionar player en spawn
[ ] **A-175:** Instanciar elementos del nivel

#### Tarea T35: GlobalConfig
[ ] **A-176:** Crear script GlobalConfig.cs
[ ] **A-177:** Definir parámetros tunables
[ ] **A-178:** Crear asset GlobalConfig.asset
[ ] **A-179:** Implementar ApplyGlobalConfig.cs
[ ] **A-180:** Vincular con controladores

#### Tarea T36: Instanciación Dinámica
[ ] **A-181:** Crear prefabs para todos los elementos
[ ] **A-182:** Implementar instanciación desde LevelData
[ ] **A-183:** Configurar posiciones y parámetros
[ ] **A-184:** Testear carga completa de nivel
[ ] **A-185:** Optimizar rendimiento de instanciación

#### Tarea T37: Validación de Datos
[ ] **A-186:** Implementar validación en LevelData
[ ] **A-187:** Añadir checks de rangos válidos
[ ] **A-188:** Crear editor custom si necesario
[ ] **A-189:** Testear casos edge
[ ] **A-190:** Documentar estructura de datos

### DoD Sprint S5
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

## Sprint S6: Pulido y Optimización (Semana 7)

### Objetivo del Sprint
El usuario va a poder disfrutar de efectos visuales atractivos (partículas, animaciones), escuchar efectos de sonido y música que mejoran la experiencia, usar una interfaz pulida y fácil de entender, jugar sin problemas de rendimiento (60 FPS estables), experimentar un juego completo sin errores molestos y sentir que está jugando un producto terminado y profesional.

### Fase F6: Pulido General

#### Tarea T38: Efectos Visuales
[ ] **A-191:** Implementar partículas para portales
[ ] **A-192:** Añadir efectos de salto y movimiento
[ ] **A-193:** Crear efectos de daño y muerte
[ ] **A-194:** Implementar post-processing básico
[ ] **A-195:** Optimizar efectos para rendimiento

#### Tarea T39: Sistema de Audio
[ ] **A-196:** Crear AudioManager.cs
[ ] **A-197:** Implementar SFX para acciones
[ ] **A-198:** Añadir música de fondo
[ ] **A-199:** Configurar mezcla de audio
[ ] **A-200:** Testear en diferentes dispositivos

#### Tarea T40: UI/UX Final
[ ] **A-201:** Diseñar HUD completo
[ ] **A-202:** Crear menú principal
[ ] **A-203:** Implementar pantallas de transición
[ ] **A-204:** Añadir animaciones de UI
[ ] **A-205:** Testear usabilidad

#### Tarea T41: Optimización
[ ] **A-206:** Profilar rendimiento
[ ] **A-207:** Optimizar scripts críticos
[ ] **A-208:** Reducir draw calls
[ ] **A-209:** Optimizar física
[ ] **A-210:** Testear en hardware objetivo

#### Tarea T42: Testing Final
[ ] **A-211:** Testing de funcionalidad completa
[ ] **A-212:** Testing de input (teclado + Kinect)
[ ] **A-213:** Testing de rendimiento
[ ] **A-214:** Testing de usabilidad
[ ] **A-215:** Corrección de bugs finales

### DoD Sprint S6
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

## Sprint S7: Entrega Final (Semana 8)

### Objetivo del Sprint
El usuario va a poder descargar e instalar el juego fácilmente, encontrar documentación clara sobre cómo jugar, configurar Kinect siguiendo guías paso a paso, resolver problemas comunes con la guía de troubleshooting, disfrutar de un juego completamente funcional y entregable, mientras que los desarrolladores tienen todo organizado para futuras mejoras.

### Fase F7: Entrega Final

#### Tarea T43: Build Final
[ ] **A-216:** Configurar build settings
[ ] **A-217:** Optimizar para plataforma objetivo
[ ] **A-218:** Generar build ejecutable
[ ] **A-219:** Testear build final
[ ] **A-220:** Crear instalador si es necesario

#### Tarea T44: Documentación Final
[ ] **A-221:** Actualizar README.md
[ ] **A-222:** Crear manual de usuario
[ ] **A-223:** Documentar configuración de Kinect
[ ] **A-224:** Crear guía de instalación
[ ] **A-225:** Documentar troubleshooting

#### Tarea T45: Assets Finales
[ ] **A-226:** Organizar assets finales
[ ] **A-227:** Crear prefabs finales
[ ] **A-228:** Configurar ScriptableObjects finales
[ ] **A-229:** Optimizar assets para build
[ ] **A-230:** Verificar integridad de assets

#### Tarea T46: Presentación
[ ] **A-231:** Crear video de demostración
[ ] **A-232:** Preparar presentación del proyecto
[ ] **A-233:** Documentar características implementadas
[ ] **A-234:** Preparar Q&A
[ ] **A-235:** Revisar entrega completa

### DoD Sprint S7
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
| S4 | Sistema de vehículos, conmutación | Car ↔ Ship estable |
| S5 | LevelData, configuración parametrizable | Niveles desde assets |
| S6 | Efectos, audio, UI, optimización | Experiencia pulida |
| S7 | Build final, documentación | Proyecto entregable |

## Notas de Desarrollo

- **Dependencias:** Cada sprint depende del anterior
- **Paralelización:** Algunas tareas pueden desarrollarse en paralelo
- **Testing:** Cada DoD debe validarse antes de continuar
- **Flexibilidad:** El plan puede ajustarse según necesidades
- **Colaboración:** Múltiples desarrolladores pueden trabajar en diferentes tareas
