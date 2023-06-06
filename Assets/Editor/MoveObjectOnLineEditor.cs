using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ObjectMoveOnLine))]
public class MoveObjectOnLineEditor : Editor
{


    float oldPercentage = 0;

    public void OnSceneGUI()
    {
        ObjectMoveOnLine script = (ObjectMoveOnLine)target;

        float newPercentage = script.getCurrentPositionPercentage();


        if (newPercentage != oldPercentage)
        {
            Undo.RecordObject(script, "Updated Start Percentage");
            script.EditorSetObjectAtPercentageLine();

            EditorUtility.SetDirty(script);
        }


        oldPercentage = newPercentage;

    }
}
