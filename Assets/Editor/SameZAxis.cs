using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SameZAxis : EditorWindow
{
    [MenuItem("Tools/AllignZAxis")]
    public static void OpenTheThing() => GetWindow<SameZAxis>("Allign Z Axis");

    private float zAxisAmount;

    private void OnEnable()
    {
        Selection.selectionChanged += Repaint;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
    }

    private void OnGUI()
    {
        GUILayout.Label("Sets all selected objects to the same global Z-axis");

        zAxisAmount = EditorGUILayout.FloatField("Global Z Axis", zAxisAmount);

        GUILayout.Height(100);
        using (new EditorGUI.DisabledGroupScope(Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button("Allign Selection ")) AllignSelection();
        }
    }

    void AllignSelection()
    {
        foreach(GameObject go in Selection.gameObjects)
        {
            Undo.RecordObject(go.transform, "Alligned Objects on Z axis");

            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, zAxisAmount);
        }
    }
}
