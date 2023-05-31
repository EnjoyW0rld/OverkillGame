using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class ReadMeAttribute : PropertyAttribute
{
    public string _text;
    /// <summary>
    /// Shows read me text in the inspector
    /// </summary>
    /// <param name="text">Text to show</param>
    public ReadMeAttribute(string text)
    {
        _text = text;
    }
}
