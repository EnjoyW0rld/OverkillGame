using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierTest : MonoBehaviour
{


    [SerializeField] public Vector3 start;
    [SerializeField] public Vector3 end;
    [SerializeField] public Vector3 tangentStart;
    [SerializeField] public Vector3 tangentEnd;


    [SerializeField] 

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnDrawGizmos()
    {
        Handles.DrawBezier(start, end, tangentStart, tangentEnd, Color.red, EditorGUIUtility.whiteTexture, 1f);
    
        
    }


    public void Generate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
