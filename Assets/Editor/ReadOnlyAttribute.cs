using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        //GUI.color = Color.red;
        //GUI.enabled = false;
        //GUI.contentColor = Color.white;
        //GUI.color = Color.white;
        //EditorGUI.PropertyField(position, property, label, true);
        GUI.TextArea(position, property.stringValue);
        //EditorGUILayout.TextArea(property.stringValue);
        GUI.enabled = true;
    }
}
