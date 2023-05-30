using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadMeAttribute))]
public class ReadOnlyEditor : DecoratorDrawer
{
    public override float GetHeight()
    {
        return base.GetHeight() + 15f;
    }
    public override void OnGUI(Rect position)
    {
        var readme = attribute as ReadMeAttribute;

        GUIStyle style = GUIStyle.none;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        style.fontSize = 15;
        style.wordWrap = true;

        EditorGUI.TextArea(position, readme._text, style);
        //base.OnGUI(position);
    }
    /**
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //return 10;
        return base.GetPropertyHeight(property, label) + 15f;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var readme = attribute as ReadMeAttribute;
        //base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(position, label, property);
        GUIStyle style = GUIStyle.none;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        style.fontSize = 15;
        style.wordWrap = true;
        
        EditorGUI.TextArea(position, readme._text, style);


        EditorGUI.EndProperty();

    }
    /**/
}
