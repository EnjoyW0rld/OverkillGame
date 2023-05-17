using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [Range(0,5)]
    [SerializeField] float range = 1.0f;

    [Range(0, 10)]
    [SerializeField] float speed = 1.0f;

    Vector3 startPos;
    [SerializeField] Transform player;


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos= transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.LogWarning(Vector3.Distance(player.position, this.transform.position));
        if (Vector3.Distance(player.position, this.transform.position) <= range)
        {
            Debug.Log("feafa");
            Vector3 difNormal = (player.position - this.transform.position).normalized;

            transform.position += difNormal * speed * Time.deltaTime;
        }
        else if (Vector3.Distance(startPos, this.transform.position) > .1f)
        {
            transform.position = Vector3.Lerp(this.transform.position, startPos, Time.deltaTime * 4f);
           // transform.position = Vector3.Lerp(this.transform.position, startPos, .05f);
        }
    }


  
}
