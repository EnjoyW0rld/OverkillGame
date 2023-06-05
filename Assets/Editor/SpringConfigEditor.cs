using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;

[CustomEditor(typeof(ZigZagSpringConfiguration))]
public class SpringConfigEditor : Editor
{

    public void OnEnable()
    {
        HideOrOnHideSprings();
    }

    public void OnDisable()
    {
        HideOrOnHideSprings();
    }


    void HideOrOnHideSprings()
    {

        ZigZagSpringConfiguration script = (target as ZigZagSpringConfiguration);


        SpringJoint[] springs = script.GetComponentsInChildren<SpringJoint>();

        if (!script.HideSpringsInInspector) foreach (SpringJoint spring in springs) { spring.hideFlags = HideFlags.None; }
        else foreach (SpringJoint spring in springs) { spring.hideFlags = HideFlags.NotEditable; }
        Debug.Log(spring.hide)

    }




    public void OnSceneGUI()
    {
        CheckStrenght();
        CheckTolerance();
        CheckDamper();

    }


    float oldStrenght = 0;

    public void CheckStrenght()
    {
        ZigZagSpringConfiguration script = (ZigZagSpringConfiguration)target;

        float newPercentage = script.springStrenght;


        if (newPercentage != oldStrenght)
        {


            Undo.RecordObject(script, "Updated Strenght");
            script.SetStrenghtToStrings();

            EditorUtility.SetDirty(script);
        }


        oldStrenght = newPercentage;
    }

    float oldtolerance = 0;

    public void CheckTolerance()
    {
        ZigZagSpringConfiguration script = (ZigZagSpringConfiguration)target;

        float newPercentage = script.tolerance;


        if (newPercentage != oldtolerance)
        {


            Undo.RecordObject(script, "Updated Tolerance");
            script.SetToleranceToStrings();

            EditorUtility.SetDirty(script);
        }


        oldtolerance = newPercentage;
    }

    float oldDamper = 0;

    public void CheckDamper()
    {
        ZigZagSpringConfiguration script = (ZigZagSpringConfiguration)target;

        float newPercentage = script.damper;


        if (newPercentage != oldDamper)
        {


            Undo.RecordObject(script, "Updated Damper");
            script.SetDamperToStrings();

            EditorUtility.SetDirty(script);
        }


        oldDamper = newPercentage;
    }
}

