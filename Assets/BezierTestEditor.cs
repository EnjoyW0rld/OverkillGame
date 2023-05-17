using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(BezierTest))]
public class BezierTestEditor : Editor
{




    public void OnSceneGUI()
    {
        BezierTest test = (BezierTest)target;
        Handles.DrawBezier(test.start, test.end, test.tangentStart, test.tangentEnd, Color.red, EditorGUIUtility.whiteTexture, 1f);

        test.start = Handles.PositionHandle(test.start, Quaternion.identity);

        test.end = Handles.PositionHandle(test.end, Quaternion.identity);

        test.tangentStart = Handles.PositionHandle(test.tangentStart, Quaternion.identity);

        test.tangentEnd = Handles.PositionHandle(test.tangentEnd, Quaternion.identity);

    }

    public override void OnInspectorGUI()
    {

        BezierTest test = (BezierTest)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Generate")) test.Generate();
    }

    public void OnDrawGizmos()
    {
        BezierTest test = (BezierTest)target;
        Handles.DrawBezier(test.start, test.end, test.tangentStart, test.tangentEnd, Color.red, EditorGUIUtility.whiteTexture, 1f);
    }


}
