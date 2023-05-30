using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadMeAttribute))]
public class ReadOnlyEditor : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        /*
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        style.padding.bottom = 10;
        style.wordWrap = true;
         */
        //position.x = 1000;
        //GUILayoutOption options = new GUILayoutOption();
        //position.yMax = 1000;
        //position.xMax = 100;
        //style.
        //GUILayout.Box(property.stringValue,style);
        //GUILayout.TextArea(property.stringValue);
        //GUILayout.Label(label, EditorStyles.boldLabel);
        //base.OnGUI(position, property, label);
        GUILayout.BeginArea(position,label);
        GUILayout.TextArea(property.stringValue);

        GUILayout.EndArea();

    }
}
