using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Damagable))]
[System.Obsolete("Not viable anymore due to HideIf attribute")]
public class DamagableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        /**
        serializedObject.Update();
        var obj = serializedObject.FindProperty("_decreaseTime");

        obj.floatValue = EditorGUILayout.FloatField(obj.floatValue);

        serializedObject.ApplyModifiedProperties();
         **/
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerTag"));

        var enums = serializedObject.FindProperty("decreaseType");
        EditorGUILayout.PropertyField(enums);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("executeAutomatically"));
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("aa"));

        switch (enums.enumValueFlag)
        {
            case 0:
                var instDecrease = serializedObject.FindProperty("instantDecrease");
                EditorGUILayout.PropertyField(instDecrease);
                break;
            case 1:
                var gradDecrease = serializedObject.FindProperty("gradualDecrease");
                EditorGUILayout.PropertyField(gradDecrease);
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }
}
