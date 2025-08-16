# REQS (Unity): Requisitos, arquitectura y plan maestro

Este documento consolida requisitos funcionales/no funcionales, arquitectura propuesta en Unity (C#) y un plan maestro incremental con actividades/DoD para construir un runner 3D con vehículos y portales. Sirve como guía accionable para el equipo.

## 1) Objetivo

- Mantener un documento vivo, técnico y accionable que sirva como:
  - Documento de diseño y arquitectura de los pasos que debemos ejecutar .
  - Plan maestro de construcción (dividir y conquistar) con pasos y subpasos para que todo sea lógico y coherente en la construcción.
  - Fuente de verdad para assets, niveles, parámetros y flujos.
- Glosario y referencias para explicar conceptos técnicos de Unity/C#.

## 2) Alcance

- Estructura, estilo, contenido y proceso de actualización del `reqs-prompt.md`.
- No dicta implementación directa de código, pero sí cómo planificarla y volverla por objetivos claros (siempre entendiéndose el por qué se necesita).

## 3) Principios rectores

- Preferir soluciones simples, explícitas y consistentes (lograr alta simplicidad es una tarea de altísima complejidad).
- Dividir y conquistar: pasos → subpasos → entregables → DoD.
- Evitar secciones duplicadas o desconectadas; consolidar y unificar (single-responsability).
- Manejo de errores y riesgos visible; parámetros tunables en config (ScriptableObjects/JSON).
- No ejecutar comandos por ti: proveer los comandos/documentación para el usuario.
- Mantener bajo el costo cognitivo y el acoplamiento entre secciones (ej. no tiene sentido diseñar clases de 400 LOC).

## 4) Estructura canónica del README

1. Visión y núcleo jugable
2. Alcance MVP (v0.1)
3. Pila tecnológica (Unity, C#, FBX/GLB, Blender)
4. Arquitectura (GameObjects/DOTS): componentes, estados, módulos/servicios, recursos, eventos, ciclos (Update/FixedUpdate/LateUpdate)
5. Mecánicas (ordenadas): vehículos, meta, daños, portales, muerte, pads
6. Datos y niveles: ScriptableObjects o JSON, ejemplo, estructura de `Assets/`
7. Pipeline de assets: Blender → FBX/GLB (convenciones detalladas)
8. Controles e Input System, UI/HUD
9. Parámetros tunables iniciales
10. Estructura de proyecto (carpetas, escenas, prefabs, scripts)
11. Plan iterativo y checklists detallados (hitos con DoD)
12. Plan maestro de construcción (pasos/subpasos, dependencias, entregables)
13. Flujo de trabajo (convenciones, desarrollo, tests, métricas)
14. Cómo ejecutar
15. Riesgos y decisiones abiertas
16. Referencias y glosario (palabras raras/desconocidas, en markdown se puede referenciar)

Mantener este orden salvo razón fuerte. Si aparecen nuevas necesidades, integrarlas en la sección que corresponda, no crear duplicados.

## 5) Proceso iterativo (ciclo por cada mejora)

1) Revisión rápida
- Leer README actual y detectar: huecos, duplicados, inconsistencias, desalineación con el código/plan.

2) Plan de edición
- Listar cambios propuestos y la sección exacta a tocar.
- Verificar dependencias entre secciones (p. ej., cambios de mecánicas afectan parámetros, UI y plan).

3) Investigación mínima
- Consultar referencias oficiales (Unity Manual/Scripting API, Physics, Input System, Addressables, glTF/FBX, Blender) para NO REINVENTAR LA RUEDA.
- Adoptar terminología y APIs ACTUALES de la versión objetivo de Unity.

4) Edición estructurada
- Mantener títulos y jerarquía consistentes.
- Usar listas con criterios de aceptación (DoD) y entregables.
- Añadir ejemplos mínimos (JSON/ScriptableObjects, esquemas) cuando aclaren conceptos.

5) Validación (DoD global por iteración)
- El README se lee de principio a fin sin contradicciones.
- No hay secciones redundantes; las relacionadas están unificadas.
- Los checklists son accionables y están alineados con el plan maestro.
- Glosario actualizado con términos nuevos.

6) Registro de cambios
- Añadir una bala breve en “Riesgos y decisiones abiertas” si hubo decisiones.
- Actualizar el roadmap/checklists si cambió el orden o alcance.

## 6) Plantilla de edición por iteración

- Contexto: qué problema mejora esta edición.
- Secciones tocadas: lista con enlaces internos.
- Cambios clave: bullets breves pero claros.
- DoD específico: qué valida que quedó bien.
- Impactos cruzados: qué otras secciones se actualizaron.

## 7) Criterios de aceptación (DoD) por secciones clave

- Arquitectura: estados, plugins y schedules listados con responsabilidades claras y orden recomendado.
- Mecánicas: reglas enunciadas en lenguaje (inglés) no ambiguo; interacciones y eventos.
- Datos/Level: definición de ScriptableObjects o JSON con ejemplo válido y desambiguado.
- Pipeline FBX/GLB: convenciones concretas (escala, ejes, nombres, materiales URP/HDRP), pasos de exportación.
- Plan/Checklists: hitos con subpasos, entregables y dependencias; sin solapamientos.
- Glosario: definiciones cortas y enlace a doc oficial/cheatbook.

## 8) Guías de contenido (resumen operativo)

- Estados/Ciclos: definir `GameState`, qué corre en `Update`, `FixedUpdate` y `LateUpdate`.
- Módulos/Sistemas: `Core`, `Player`, `Vehicle`, `Level`, `Collision`, `Damage`, `Camera`, `Audio`, `UI`.
- Mecánicas: `Car` (auto-forward + salto), `Ship` (ascenso continuo), portales, gravedad, jump pads, daños (flat/%/letal), OOB.
- Datos: `LevelData` con `track`, `player_spawn`, `portals`, `gravity_portals`, `jump_pads`, `hazards`.
- JSON/ScriptableObjects: incluir ejemplo minimal y validar tipos; mantener consistencia con el código y serialización de Unity.
- Blender→FBX/GLB: 1m=1u, +Y up, +Z forward; nombres por tipo; materiales URP simples.
- UI/HUD: vida, icono vehículo, gravedad; mensajes de GameOver; debug overlay.
- Parámetros: tabla o lista de tunables con valores iniciales.
- Roadmap/Plan maestro: vincular checklists a entregables de código/assets.

## 9) Mantenimiento del glosario

- Añadir término al surgir en el README o código.
- Formato: definición corta + 1 enlace oficial/cheatbook útil.
- Revisar enlaces rotos o desactualizados en cada iteración mayor.

## 10) Integración de investigación online

- Priorizar: Unity Manual, Scripting API, Physics (PhysX), Input System, Addressables, docs de glTF/FBX y Blender.
- Citar enlaces con texto descriptivo; evitar URLs sueltas.
- Si una API cambió entre versiones de Unity, anotar la versión objetivo y el cambio.

## 11) Sincronización con el código

- Tras cambios de arquitectura/mecánicas en el código, actualizar README en la misma PR.
- Los parámetros tunables deben existir en código o config y estar reflejados en README.
- Si se agrega un módulo/sistema nuevo, incorporarlo en “Arquitectura” y “Estructura de proyecto”.

## 12) Estilo y formato

- Idioma: español claro; evitar jerga innecesaria.
- Markdown: usar `###`/`##`, bullets con negritas como pseudo‑encabezados, `inline code` para nombres (`AppState`, `VehicleKind`), enlaces con ancla.
- Evitar párrafos largos; preferir listas y secciones cortas.
- Mantener títulos estables para facilitar diffs.

## 13) Lista de “no hacer”

- No duplicar la misma información en distintas secciones.
- No mezclar decisiones con implementaciones sin marcar su estado (p. ej., “propuesto”, “implementado”).
- No introducir comandos que requieran interacción; solo proveerlos para que el usuario los ejecute.
- No referenciar APIs deprecadas sin anotar la migración planeada.

## 14) Enlaces de referencia

- Unity Manual: https://docs.unity3d.com/Manual/index.html
- Unity Scripting API (C#): https://docs.unity3d.com/ScriptReference/
- Unity Input System: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/index.html
- Unity Physics (PhysX): https://docs.unity3d.com/Manual/PhysicsSection.html
- Addressables: https://docs.unity3d.com/Packages/com.unity.addressables@1.21/manual/index.html
- URP: https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest
- glTF 2.0: https://www.khronos.org/gltf/
- Blender export (FBX/GLTF): https://docs.blender.org/manual/en/latest/files/import_export/
- Azure Kinect Body Tracking (MS Learn): https://learn.microsoft.com/en-us/shows/mixed-reality/azure-kinect-body-tracking-unity-integration
- Azure Kinect Examples for Unity (Asset Store): https://assetstore.unity.com/packages/tools/integration/azure-kinect-and-femto-bolt-examples-for-unity-149700
- LightBuzz: Guía Azure Kinect + Unity: https://lightbuzz.com/azure-kinect-unity/
- RF Solutions: Azure Kinect Examples blog: https://rfilkov.com/2019/07/24/azure-kinect-examples-for-unity/
- Azure Kinect Unity Body Tracker (GitHub): https://github.com/satoshi-maemoto/Azure-Kinect-Unity-Body-Tracker
- Discusión: Conectar Kinect con SDK oficial en Unity: https://discussions.unity.com/t/connecting-kinect-unity-with-official-sdk/492431

## 15) Mapa entre el Plan maestro y el Roadmap

- Plan maestro pasos 0–10 se reflejan 1:1 en “Plan iterativo y checklists” y “Plan maestro de construcción”.
- Al cambiar el orden de pasos, actualizar ambas secciones y dependencias.

## 16) Checklist rápido para una iteración

- [ ] Identificar hueco/duplicado
- [ ] Plan de edición con secciones concretas
- [ ] Investigar referencias (1–2 enlaces)
- [ ] Editar con pasos/subpasos, DoD, entregables
- [ ] Unificar con secciones relacionadas (evitar duplicados)
- [ ] Actualizar glosario y riesgos/decisiones
- [ ] Releer de punta a punta (consistencia) y guardar

## 17) Mantenimiento de `documents.md`

- Añadir/actualizar ejemplos cuando el README introduzca un concepto nuevo o cambie arquitectura.
- Mantener snippets mínimos, compilables y alineados con la versión de Unity objetivo.
- Vincular desde README (Playbooks/Documentos de apoyo) a los apartados relevantes.

## 18) Pruebas de aceptación (scripts) y ADRs

- Mantener una sección de “Pruebas de aceptación por hito” en README con casos manuales claros.
- Cada cambio grande debe:
  - Añadir/actualizar las pruebas de aceptación correspondientes
  - Crear/actualizar un ADR si la decisión impacta arquitectura o herramientas
- Ubicar ADRs en `docs/adr/` con la plantilla mínima provista.

---

Usa este documento como guía operativa: cada edición del README/REQS debe pasar por el ciclo iterativo y cumplir DoD. Si el proyecto evoluciona (p. ej., cambios de controladores de vehículo, migración de URP, nuevos sistemas), refleja el cambio en arquitectura, plan, parámetros y glosario en la misma iteración.

## 20) Convenciones de proyecto (Unity)

- Carpetas en `Assets/`:
  - `Assets/Scenes/`, `Assets/Prefabs/`, `Assets/Scripts/{Core,Player,Vehicles,Level,UI,Audio}/`
  - `Assets/ScriptableObjects/`, `Assets/Art/{Models,Materials,Textures}/`, `Assets/Audio/`
- Nombres:
  - Escenas: `Level_01.unity`, `MainMenu.unity`
  - Prefabs: `Player.prefab`, `Portal_Vehicle.prefab`, `JumpPad.prefab`
  - Scripts: `PascalCase` por clase; 1 clase pública por archivo
- Física: `Fixed Timestep = 0.0166667`; `Interpolation` activada en cámara si hay jitter

## 21) Mapa de entradas (Input System)

- Acciones requeridas (Action Map: `Player`):
  - `Move` (Vector2): teclado A/D o flechas (usar X), gamepad stick X
  - `JumpOrAscend` (Button): Tecla Space/W, botón sur gamepad
  - `Pause` (Button): Esc/Start
- Binding sugerido: `PlayerInput` en el `Player` con comportamiento `Invoke Unity Events`
- DoD:
  - Reasignable en Editor; eventos conectados a controladores; pausa funcional

## 22) Actividades incrementales (A0–A6)

- A0 Núcleo y escena base
  - Objetivo: proyecto 3D, `GameManager`, escena `Level_01` con cámara
  - Tareas: crear `GameManager`, configurar `Physics.gravity`, añadir cámara y suelo
  - Entregables: escena guardada, `GameManager` en `DontDestroyOnLoad`
  - DoD: play en Editor muestra escena sin errores; `State=Playing`

- A1 Player (Car) y movimiento
  - Objetivo: auto‑forward + salto con chequeo de suelo
  - Tareas: `Rigidbody`, `CarController`, ground mask/raycast; HUD de vida mínimo
  - Entregables: `Player.prefab` con `CarController` + `Health`
  - DoD: salta solo en suelo; avanza constante; HUD refleja vida

- A2 Player (Ship)
  - Objetivo: ascenso continuo con límite de velocidad
  - Tareas: `ShipController`, conmutación por `PlayerVehicleSwitcher`
  - Entregables: `ShipController` operativo y conmutación estable
  - DoD: ascendencia mantenida; límites respetados; cambio de vehículo estable

- A3 Portales y triggers
  - Objetivo: `PortalVehicle`, `PortalGravity`, `JumpPad`
  - Tareas: colliders como trigger, scripts; invulnerabilidad breve
  - Entregables: prefabs de portales/pads
  - DoD: efectos inmediatos con feedback

- A4 Daño y hazards
  - Objetivo: sistema de daño flat/% y muerte
  - Tareas: `Health`, `Hazard`, flujo GameOver simple
  - Entregables: hazards prefabs; UI GameOver mínima
  - DoD: pincho letal mata; bloque daña %; GameOver/reinicio

- A5 Datos de nivel
  - Objetivo: `LevelData` (ScriptableObject) aplicado al cargar escena
  - Tareas: `LevelLoader` que aplica gravedad, spawn y coloca triggers básicos
  - Entregables: `LevelData` asset y `LevelLoader`
  - DoD: nivel parametrizable desde asset sin tocar escena

- A6 Input System y pausa
  - Objetivo: acciones `Move`, `JumpOrAscend`, `Pause` con `PlayerInput`
  - Tareas: crear `*.inputactions`, enlazar a controladores/UI
  - Entregables: asset de acciones y wiring
  - DoD: controles funcionan con teclado y gamepad; pausa togglable

## 23) Criterios de rendimiento y telemetría

- 60 FPS objetivo en hardware medio; evitar spikes de GC
- Perfilado: Profiler de Unity; medir FixedUpdate de controladores
- Logs: usa `Debug.Log` solo en desarrollo; wrappers si aplica

## 24) Riesgos y mitigaciones

- Jitter de cámara: usar `LateUpdate` + interpolación o Cinemachine
- Física inestable por escalas: normalizar unidades (1u=1m) y masas razonables
- Input System vs viejo Input: eliminar duplicidad progresivamente
- Addressables (si aplica): latencias; precarga de escenas críticas

## 25) Entregables por hito (resumen)

- H1 Movimiento y HUD: `Player.prefab`, `CarController`, HUD vida
- H2 Conmutación vehículo: `ShipController`, `PlayerVehicleSwitcher`
- H3 Triggers/Portales: prefabs + efectos
- H4 Daño/GameOver: `Health`, `Hazard`, UI básica
- H5 Datos de nivel: `LevelData` + `LevelLoader`

## 19) Integración de Kinect en Unity para Detección de Poses

### Objetivo
- Integrar Kinect para controlar el vehículo mediante poses corporales (arriba/abajo/izquierda/derecha) en un proyecto 3D normal (no VR).

### Tipos de proyecto Unity
- Proyecto 3D normal (recomendado). El template VR solo si se combina con HMDs.

### Opciones de Kinect
- Azure Kinect (recomendado): mejor tracking; SDK Sensor + Body Tracking; múltiples personas.
- Kinect v2 (legacy): SDK 2.0; válido para detección de poses básica.
- Alternativas comerciales: Nuitrack (si no se dispone de Azure Kinect).

### Prerrequisitos
- Windows 10/11 x64, USB 3.0 (Azure/Kinect v2), GPU compatible (para Body Tracking Azure).
- Editor y build en x86_64; permisos de cámara/sensores habilitados.

### Proceso de implementación
1) Instalación de SDKs
   - Azure Kinect Sensor SDK y Body Tracking SDK, o Kinect for Windows SDK 2.0.
   - Verificar herramientas (firmware, visualizadores) y drivers.
2) Configuración del proyecto Unity
   - Crear proyecto 3D normal.
   - Importar paquete/plugin de Kinect (Asset Store o repos oficiales).
   - Copiar DLLs nativas a `Assets/Plugins/x86_64` (según documentación del paquete).
   - Ajustar Player Settings: Architecture x86_64; desactivar stripping agresivo de engine si afecta a plugins nativos.
3) Configuración en la escena
   - Crear `KinectManager` (o script wrapper) para inicializar sensor y streams (depth/body).
   - Añadir un `KinectDebugOverlay` opcional para visualizar joints en tiempo real.
   - Crear un `KinectConfig` (ScriptableObject) con umbrales (ángulos, alturas, tiempos) y sensibilidad del smoothing.
4) Detección de poses y acciones
   - Leer joints clave: `Head`, `ShoulderLeft/Right`, `HandLeft/Right`, `HipLeft/Right`.
   - Calcular poses básicas con heurísticas robustas:
     - Arriba: manos por encima de cabeza por ≥ tUp ms.
     - Abajo: manos por debajo de caderas por ≥ tDown ms.
     - Izquierda/Derecha: mano dominante extendida lateralmente con umbral de distancia x hombro.
   - Mapear a acciones del juego (Input/Command bus) y controlar el vehículo.

```csharp
// Ejemplo mínimo de lectura y mapeo (pseudocódigo orientativo)
void Update() {
    if (!kinect.TryGetPrimaryBody(out var body)) return;
    var head = body.Joints[JointType.Head].Position;
    var handL = body.Joints[JointType.HandLeft].Position;
    var handR = body.Joints[JointType.HandRight].Position;
    var hip   = body.Joints[JointType.SpineBase].Position;

    if (handL.y > head.y && handR.y > head.y && Hold(tUp))    vehicle.Up();
    else if (handL.y < hip.y  && handR.y < hip.y && Hold(tDown)) vehicle.Down();
    else if (handR.x - head.x > side && Hold(tSide))              vehicle.Right();
    else if (head.x - handL.x > side && Hold(tSide))              vehicle.Left();
}
```

### Rendimiento y calidad
- Azure Body Tracking puede usar GPU; documentar requisitos mínimos y FPS esperados.
- Limitar número de cuerpos rastreados a 1–2 para estabilidad.
- Smoothing/filtrado de joints y ventanas temporales para reducir jitter.

### Errores comunes y mitigaciones
- DLLs no encontradas: revisar ruta `Assets/Plugins/x86_64` y dependencias (Visual C++ Redistributable).
- Permisos/sensor no detectado: validar drivers y cable USB 3.0; usar viewer oficial.
- Coordenadas: normalizar espacios (sensor vs mundo); aplicar transform de calibración.

### DoD (Kinect)
- El editor detecta el sensor y muestra joints en debug.
- Poses arriba/abajo/izquierda/derecha disparan acciones en el juego de forma consistente (≥ 95% en pruebas guiadas).
- Documentación de setup y troubleshooting en README.

### Pruebas manuales
- Caso 1: Levantar ambas manos 1s → subir.
- Caso 2: Bajar ambas manos 1s → bajar.
- Caso 3: Extender mano derecha 0.5s → derecha.
- Caso 4: Extender mano izquierda 0.5s → izquierda.

### Recursos recomendados
- [Azure Kinect Body Tracking (MS Learn)](https://learn.microsoft.com/en-us/shows/mixed-reality/azure-kinect-body-tracking-unity-integration)
- [LightBuzz: Azure Kinect + Unity](https://lightbuzz.com/azure-kinect-unity/)
- [Azure Kinect Examples for Unity (Asset Store)](https://assetstore.unity.com/packages/tools/integration/azure-kinect-and-femto-bolt-examples-for-unity-149700)
- [RF: Azure Kinect Examples blog](https://rfilkov.com/2019/07/24/azure-kinect-examples-for-unity/)
- [Azure-Kinect-Unity-Body-Tracker (GitHub)](https://github.com/satoshi-maemoto/Azure-Kinect-Unity-Body-Tracker)
