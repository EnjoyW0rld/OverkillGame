using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Damagable))]
public class DamagableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var obj = serializedObject.FindProperty("_decreaseTime");

        obj.floatValue = EditorGUILayout.FloatField(obj.floatValue);

        serializedObject.ApplyModifiedProperties();

    }
}
