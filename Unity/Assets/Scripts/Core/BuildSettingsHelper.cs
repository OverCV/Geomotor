using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
[InitializeOnLoad]
public class BuildSettingsHelper
{
    static BuildSettingsHelper()
    {
        // Auto-configure build settings when Unity starts
        ConfigureBuildSettings();
    }
    
    [MenuItem("Geomotor/Configure Build Settings")]
    public static void ConfigureBuildSettings()
    {
        // Get current build settings
        EditorBuildSettingsScene[] originalScenes = EditorBuildSettings.scenes;
        
        // Define our scenes
        string[] scenePaths = {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/CarTest.unity"
        };
        
        // Create new build settings scene list
        EditorBuildSettingsScene[] newScenes = new EditorBuildSettingsScene[scenePaths.Length];
        
        for (int i = 0; i < scenePaths.Length; i++)
        {
            newScenes[i] = new EditorBuildSettingsScene(scenePaths[i], true);
            Debug.Log($"Added scene to build: {scenePaths[i]}");
        }
        
        // Update build settings
        EditorBuildSettings.scenes = newScenes;
        
        Debug.Log("Build Settings configured successfully!");
        Debug.Log($"Total scenes in build: {newScenes.Length}");
        
        // Print current build settings
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = EditorBuildSettings.scenes[i];
            Debug.Log($"Scene {i}: {scene.path} (enabled: {scene.enabled})");
        }
    }
}
#endif