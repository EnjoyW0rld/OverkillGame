using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadMeAttribute))]
public class ReadOnlyEditor : DecoratorDrawer
{
    float height;
    public override float GetHeight()
    {
        //var readme = attribute as ReadMeAttribute;
        return height + 15f;//base.GetHeight() + 15f;
    }
    public override void OnGUI(Rect position)
    {
        var readme = attribute as ReadMeAttribute;

        GUIStyle style = GUIStyle.none;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        style.fontSize = 15;
        style.wordWrap = true;
        GUIContent content = new GUIContent(readme._text);
        height = style.CalcHeight(content, EditorGUIUtility.currentViewWidth);

        EditorGUI.TextArea(position, readme._text, style);

        //base.OnGUI(position);
    }

}
