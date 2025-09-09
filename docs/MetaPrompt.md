# Meta‑Prompt: Guía para iterar y mejorar el README del proyecto

Este documento define cómo evolucionar el `README.md` de forma continua, estructurada y sin reinventar la rueda, alineado con la visión del juego (runner 3D tipo Geometry Dash con vehículos y portales) y las buenas prácticas de Unity (C#) con arquitectura modular (GameObjects/DOTS cuando aplique).

## 1) Objetivo del meta‑prompt

- Mantener un `context/unity/reqs-prompt.md` vivo, técnico y accionable que sirva como:
  - Documento de diseño y arquitectura de los pasos que debemos ejecutar .
  - Plan maestro de construcción (dividir y conquistar) con pasos y subpasos para que todo sea lógico y coherente en la construcción.
  - Fuente de verdad para assets, niveles, parámetros y flujos.
  - Glosario y referencias para explicar conceptos técnicos de Unity/C#.

## 2) Alcance

- Estructura, estilo, contenido y proceso de actualización del `README.md`.
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

- Arquitectura: estados, módulos y ciclos (Update/FixedUpdate/LateUpdate) listados con responsabilidades claras y orden recomendado.
- Mecánicas: reglas enunciadas en lenguaje (inglés) no ambiguo; interacciones y eventos.
- Datos/Level: definición de ScriptableObjects o JSON con ejemplo válido y desambiguado.
- Pipeline FBX/GLB: convenciones concretas (escala, ejes, nombres, materiales URP/HDRP), pasos de exportación.
- Plan/Checklists: hitos con subpasos, entregables y dependencias; sin solapamientos.
- Glosario: definiciones cortas y enlace a doc oficial.

## 8) Guías de contenido (resumen operativo)

- Estados/Ciclos: definir `GameState`, qué corre en `Update`, `FixedUpdate` y `LateUpdate`.
- Módulos/Sistemas: `Core`, `Player`, `Vehicle`, `Level`, `Collision`, `Damage`, `Camera`, `Audio`, `UI`.
- Mecánicas: `Car` (auto-forward + salto), `Ship` (ascenso continuo), portales, gravedad, jump pads, daños (flat/%/letal), OOB.
- Datos: `LevelData` (ScriptableObject/JSON) con `track`, `player_spawn`, `portals`, `gravity_portals`, `jump_pads`, `hazards`.
- JSON/ScriptableObjects: incluir ejemplo minimal y validar tipos; mantener consistencia con el código y serialización de Unity.
- Blender→FBX/GLB: 1m=1u, +Y up, +Z forward; nombres por tipo; materiales URP simples.
- UI/HUD: vida, icono vehículo, gravedad; mensajes de GameOver; debug overlay.
- Parámetros: lista de tunables con valores iniciales (ScriptableObject de configuración).
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
- Incluir relación con `Scenes`, `Prefabs`, `ScriptableObjects` y Addressables cuando aplique.

## 12) Estilo y formato

- Idioma: español claro; evitar jerga innecesaria.
- Markdown: usar `###`/`##`, bullets con negritas como pseudo‑encabezados, `inline code` para nombres (`GameState`, `VehicleKind`), enlaces con ancla.
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

Usa este meta‑prompt como guía operativa: cada edición del README debe pasar por el ciclo iterativo y cumplir DoD. Si el proyecto evoluciona (p. ej., cambiar de CharacterController a Rigidbody, o migrar entre versiones de Unity/URP/HDRP), refleja el cambio en arquitectura, plan, parámetros y glosario en la misma iteración.

## 19) Integración de Kinect en Unity para Detección de Poses

- Resumen: Es viable integrar Kinect en un proyecto 3D normal de Unity (no VR). El template VR no es necesario salvo que se combine con HMDs.
- Opciones de hardware/SDK:
  - Azure Kinect (recomendado): mejor tracking, SDK de Body Tracking, múltiples personas.
  - Kinect v2 (legacy): SDK 2.0, útil para casos básicos. Alternativas de terceros como Nuitrack.
- Proceso alto nivel:
  1) Instalar SDKs: Sensor SDK + Body Tracking SDK (Azure) o Kinect v2 SDK.
  2) Proyecto Unity 3D normal: importar paquete/plugin de Kinect; copiar DLLs nativas en `Assets/Plugins/x86_64`.
  3) Escena: crear `KinectManager` (o script wrapper) y habilitar streams requeridos (depth/body).
  4) Poses: acceder a articulaciones (head, hands, shoulders, hips) y derivar gestos/poses para acciones.
  5) Configuración: ScriptableObject con umbrales/tiempos para detección y mapeo a acciones del juego.
- DoD específico (Kinect):
  - Proyecto compila en Editor x86_64 con DLLs cargadas sin errores.
  - Detección de al menos 1 cuerpo y 32 joints visibles (Azure) con gizmos de debug opcionales.
  - Acciones de juego disparadas por 4 poses básicas: arriba, abajo, izquierda, derecha.
  - Rendimiento ≥ 30 FPS en máquina objetivo; documentación de requisitos GPU/CPU para Body Tracking.
- Riesgos/decisiones:
  - Dependencia de drivers/GPU para Body Tracking; validar compatibilidad temprano.
  - Latencia de tracking: aplicar smoothing y ventanas temporales.
  - Soporte multi‑persona: definir políticas (priorizar jugador 1 por proximidad/centro).

## 20) Plantilla de Actividad (A#)

- Objetivo: frase breve y medible.
- Tareas: lista de 3–7 pasos concretos.
- Entregables: artefactos verificables (scripts, prefabs, escenas, assets).
- DoD (aceptación): criterios observables en Editor/Play.
- Snippets: enlace(s) a `context/unity/documents.md`.
- Métricas: 1–2 checks (FPS, logs, ausencia de errores en consola).
- Riesgos/fallos: cómo degradar con gracia o fallback.

Ejemplo mínimo:
- Objetivo: “Implementar `PortalVehicle` con invulnerabilidad 0.25s”.
- Tareas: crear script, añadir `OnTriggerEnter`, llamar a `PlayerVehicleSwitcher`.
- Entregables: `Portal_Vehicle.prefab` con collider trigger.
- DoD: al tocar, cambia vehículo y no recibe daño por 0.25s.
- Snippets: ver §5 en `documents.md`.
- Métricas: sin errores en consola; interacción reproducible 3/3.
- Riesgos: collider mal configurado → no dispara; validar `isTrigger` y capa.

## 21) Plantilla de Snippet

- Contexto: para qué sirve, dependencias (paquetes/Components/Player Settings).
- Código mínimo y seguro (null‑checks, clamps, evitar GC innecesario en Update).
- Uso: cómo arrastrar/referenciar en Inspector y probar.
- Variantes: opcionales (URP/HDRP, Addressables, Cinemachine).

## 22) Versionado de Unity y paquetes

- Editor objetivo: 2021 LTS+ (o superior si se acuerda).
- Paquetes mínimos: Input System 1.7+, URP opcional, Addressables opcional.
- Política: cambios de versión requieren ADR y smoke test de A0–A3.