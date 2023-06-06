using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Not used anymore due to different mesh movement system")]
public class HipToBodyConnector : MonoBehaviour
{
    [SerializeField] private Transform body;
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        transform.position = body.localPosition + startPos;
    }
}
