using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEditor : EditorWindow {
    public static MapEditor window;

    [MenuItem("Tools/Map Editor")]
    public static void OpenWindow() {
        window = (MapEditor) EditorWindow.GetWindow(typeof(MapEditor));
        window.title = "Map Editor";
    }

    void OnGUI() {
        if(window == null)
            OpenWindow();
        
    }

    void Update() {
        Repaint();
    }
}
