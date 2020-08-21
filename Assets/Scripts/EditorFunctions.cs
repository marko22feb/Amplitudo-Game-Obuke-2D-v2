using UnityEngine;
using UnityEditor;

public class EditorFunctions : EditorWindow
{
    [MenuItem("Window/EditorFunctions")]
    public static void ShowWindow()
    {
        GetWindow<EditorFunctions>("EditorFunctions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Destroy Current NavGrid"))
        {
            GameObject.Find("NavGrid").GetComponent<MakeGrid>().DestroyCurrentNavGrid();
        }

        if (GUILayout.Button("Create NavGrid"))
        {
            GameObject.Find("NavGrid").GetComponent<MakeGrid>().GenerateNavGrid();
        }
    }
}
