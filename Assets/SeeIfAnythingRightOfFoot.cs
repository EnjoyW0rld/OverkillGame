using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SeeIfAnythingRightOfFoot : MonoBehaviour
{

    [SerializeField] LayerMask layerCheck;

    public UnityEvent somethingBeforeLeg;
    public UnityEvent nothingBeforeLeg;
    private bool lastFrameBefore = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Physics.Raycast(this.transform.position, Vector3.back, 10, layerCheck))
        {
            somethingBeforeLeg?.Invoke();
            lastFrameBefore = true;
        }
        else if (lastFrameBefore)
        {
            nothingBeforeLeg?.Invoke();
            lastFrameBefore = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.back);
    }
}
