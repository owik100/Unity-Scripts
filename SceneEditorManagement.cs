using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneEditorManagement : EditorWindow
{
    private string _folderPath = "Assets/Scenes/Game";
    private bool _loadScenesInactive = false;
    private bool _loadScenesWithJustTileMaps = false;

    [MenuItem("Tools/Scene Editor Management/Open All Scenes Settings Panel")]
    private static void ShowWindow()
    {
        GetWindow<SceneEditorManagement>("Scene Editor Management");
    }

    private void OnGUI()
    {
        GUILayout.Label("Open all scenes additively ", EditorStyles.boldLabel);

        if (GUILayout.Button("Open GameRoot"))
        {
            OpenGameRootScene();
        }

        EditorGUILayout.BeginHorizontal();
        _folderPath = EditorGUILayout.TextField("Folder Path:", _folderPath);

        if (GUILayout.Button("Focus Folder", GUILayout.Width(100)))
        {
            FocusFolderInProject();
        }
        EditorGUILayout.EndHorizontal();

        _loadScenesInactive = EditorGUILayout.Toggle("Load scenes inactive:", _loadScenesInactive);

        if (_loadScenesInactive)
        {
            _loadScenesWithJustTileMaps = false;
        }

        EditorGUI.BeginDisabledGroup(_loadScenesInactive);
        _loadScenesWithJustTileMaps = EditorGUILayout.Toggle("Open just Tile Maps:", _loadScenesWithJustTileMaps);
        EditorGUI.EndDisabledGroup();

        if (GUILayout.Button("Open All Scenes", GUILayout.Height(30)))
        {
            OpenScenesAdditively();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Clear Overview (Unload All Scenes)", GUILayout.Height(30)))
        {
            ClearAllLoadedRooms();
        }
    }

    private void OpenGameRootScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/GameRoot.unity");
    }

    private void FocusFolderInProject()
    {
        if (string.IsNullOrEmpty(_folderPath))
        {
            EditorUtility.DisplayDialog("Error", "Folder path is empty.", "OK");
            return;
        }

        string normalizedPath = _folderPath.TrimEnd('/', '\\');

        if (!AssetDatabase.IsValidFolder(normalizedPath))
        {
            EditorUtility.DisplayDialog("Error", $"Folder does not exist in Project:\n{normalizedPath}", "OK");
            return;
        }

        Object folder = AssetDatabase.LoadAssetAtPath<Object>(normalizedPath);
        if (folder != null)
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = folder;
            EditorGUIUtility.PingObject(folder);
        }
        else
        {
            EditorUtility.DisplayDialog("Error", $"Failed to load folder:\n{normalizedPath}", "OK");
        }
    }

    private void OpenScenesAdditively()
    {
        string[] sceneFiles = Directory.GetFiles(_folderPath, "*.unity", SearchOption.AllDirectories);

        if (sceneFiles.Length == 0)
        {
            EditorUtility.DisplayDialog("No Scenes Found", $"No .unity scenes found in:\n{_folderPath}", "OK");
            return;
        }

        foreach (string scenePath in sceneFiles)
        {
            var scene = EditorSceneManager.OpenScene(scenePath, _loadScenesInactive ? OpenSceneMode.AdditiveWithoutLoading : OpenSceneMode.Additive);
            if (_loadScenesWithJustTileMaps)
            {
                DisableEverythingExceptTilemaps(scene);
            }

            Debug.Log($"Opened: {scene.name}");
        }

        EditorUtility.DisplayDialog("Done", $"Opened {sceneFiles.Length} scenes additively.", "OK");
    }

    private void DisableEverythingExceptTilemaps(Scene scene)
    {
        foreach (GameObject rootObj in scene.GetRootGameObjects())
        {
            DeactivateEverythingExceptTilemaps(rootObj);
        }
    }

    private static void DeactivateEverythingExceptTilemaps(GameObject obj)
    {
        bool containsAllowed = ContainsTilemap(obj);

        if (!containsAllowed)
        {
            obj.SetActive(false);
            return;
        }

        ActivateHierarchy(obj);

        foreach (Transform child in obj.transform)
        {
            DeactivateEverythingExceptTilemaps(child.gameObject);
        }
    }

    private static bool ContainsTilemap(GameObject obj)
    {
        return obj.GetComponentInChildren<UnityEngine.Tilemaps.Tilemap>() != null;
    }

    private static void ActivateHierarchy(GameObject obj)
    {
        Transform current = obj.transform;
        while (current != null)
        {
            current.gameObject.SetActive(true);
            current = current.parent;
        }
    }

    private static void ClearAllLoadedRooms()
    {
        const string gameRootName = "GameRoot";

        Scene gameRootScene = default;
        var scenesToClose = new List<Scene>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.IsValid() && s.name == gameRootName)
            {
                gameRootScene = s;
            }
            else if (s.IsValid())
            {
                scenesToClose.Add(s);
            }
        }

        if (!gameRootScene.IsValid())
        {
            EditorUtility.DisplayDialog("Clear Overview", $"Scene '{gameRootName}' not found. No scenes were closed.", "OK");
            return;
        }

        EditorSceneManager.SetActiveScene(gameRootScene);

        foreach (var s in scenesToClose)
        {
            if (s.IsValid())
            {
                EditorSceneManager.CloseScene(s, true);
                Debug.Log($"Closed: {s.name}");
            }
        }
        Debug.Log("Cleared all loaded room scenes.");
    }
}
