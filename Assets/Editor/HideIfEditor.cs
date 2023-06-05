using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(HideIfAttribute))]
public class HideIfEditor : PropertyDrawer
{
    private bool show;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (show)
        {
            return base.GetPropertyHeight(property, label);
        }
        else return 0;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        HideIfAttribute hideIf = attribute as HideIfAttribute;
        Type type = hideIf.value.GetType();
        show = Compare(property.serializedObject.FindProperty(hideIf.targetName), hideIf.value, hideIf.comparison);
        if (show)
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    private bool Compare(SerializedProperty target, object comparer, HideIfAttribute.Comparison compareType)
    {
        SerializedPropertyType propType = target.propertyType;
        try
        {
            switch (propType)
            {
                case SerializedPropertyType.Boolean:
                    return target.boolValue == (bool)comparer;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Types of two variables are not the same");
        }

        return false;
    }
}
