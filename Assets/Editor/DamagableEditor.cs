using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Damagable))]
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

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_playerTag"));

        var enums = serializedObject.FindProperty("_decreaseType");
        EditorGUILayout.PropertyField(enums);

        //EditorGUILayout.PropertyField(serializedObject.FindProperty("aa"));

        switch (enums.enumValueFlag)
        {
            case 0:
                var instDecrease = serializedObject.FindProperty("_instantDecrease");
                EditorGUILayout.PropertyField(instDecrease);
                break;
            case 1:
                var gradDecrease = serializedObject.FindProperty("_gradualDecrease");
                EditorGUILayout.PropertyField(gradDecrease);
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }
}
