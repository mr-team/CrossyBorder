using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class ChromaKeyEditor : EditorWindow {
    public static ChromaKeyEditor window;

    static Texture2D image;
    static Color32 chromaKeyColor = new Color32(0, 255, 0, 255);
    static Color32 chromaKeyReplacement = new Color32(0, 0, 0, 0);

    static bool hasChanged = false;

    [MenuItem("Tools/Chroma Key Tool")]
    public static void OpenWindow() {
        window = (ChromaKeyEditor) EditorWindow.GetWindow(typeof(ChromaKeyEditor));
        window.title = "Chroma Key Tool";
    }

    void OnGUI() {
        if(window == null)
            OpenWindow();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Chroma Key Color");
        chromaKeyColor = EditorGUILayout.ColorField(chromaKeyColor);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Chroma Key Replacement");
        chromaKeyReplacement = EditorGUILayout.ColorField(chromaKeyReplacement);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Image");
        image = (Texture2D) EditorGUILayout.ObjectField(image, typeof(Texture2D), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Save")) {
            byte[] data = image.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/../text.png", data);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(image);
        GUILayout.EndHorizontal();

    }

    void Update() {
        ReplaceColors();
        Repaint();
    }

    void ReplaceColors() {
        int width = image.width;
        int height = image.height;

        for(int w = 0; w < width; w++) {
            for(int h = 0; h < height; h++) {
                Color c = image.GetPixel(w, h);
                if(c == chromaKeyColor)
                    image.SetPixel(w, h, chromaKeyReplacement);
            }
        }
    }
}
