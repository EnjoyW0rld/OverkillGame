using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [Range(0, 5)]
    [SerializeField] float range = 1.0f;

    [Range(0, 10)]
    [SerializeField] float speed = 1.0f;


    [SerializeField] float reduceSanitySpeed = 2.0f;

    Vector3 startPos;
    Transform player;

    Sanity sanity;




    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Body").transform;

        startPos = transform.position;

        sanity = GameObject.FindObjectOfType<Sanity>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.position, this.transform.position) <= range)
        {
            Vector3 difNormal = (player.position - this.transform.position).normalized;

            transform.position += difNormal * speed * Time.deltaTime;
        }
        else if (Vector3.Distance(startPos, this.transform.position) > .1f)
        {
            transform.position = Vector3.Lerp(this.transform.position, startPos, Time.deltaTime * 4f);
            // transform.position = Vector3.Lerp(this.transform.position, startPos, .05f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        sanity.ReduceSanity(reduceSanitySpeed);
    }

    public void OnTriggerExit(Collider other)
    {
        sanity.ResetSanity();
    }





}
