using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DirectionEvent : UnityEvent<Vector3> { }

public class Fly : MonoBehaviour
{
    [Range(0, 50)]
    [SerializeField] float range = 1.0f;

    [Range(0, 10)]
    [SerializeField] private float speed = 1.0f;

    [SerializeField] private float reduceSanitySpeed = 2.0f;

    private Vector3 startPos;
    private Transform player;

    public DirectionEvent onDirectionChanged;

    private bool frogInDamgaeArea = false;

    public UnityEvent onPlayerEnterFly;
    public UnityEvent onPlayerExitFly;
    public UnityEvent onPlayerEnterTargetRange;
    public UnityEvent onPlayerExitTargetRange;

    private bool inRange = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Body").transform;

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) <= range)
        {
            Vector3 difNormal = (player.position - this.transform.position).normalized;

            if (frogInDamgaeArea) return;
            onDirectionChanged?.Invoke(difNormal);

            transform.position += difNormal * speed * Time.deltaTime;

            if (!inRange)
            {
                onPlayerEnterTargetRange?.Invoke();
                inRange = true;
            }

        }
        else if (Vector3.Distance(startPos, this.transform.position) > .1f)
        {
            transform.position = Vector3.Lerp(this.transform.position, startPos, Time.deltaTime * 4f);

            if (inRange)
            {
                onPlayerExitTargetRange?.Invoke();
                inRange = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        frogInDamgaeArea = true;
        onPlayerEnterFly?.Invoke();
    }

    public void OnTriggerExit(Collider other)
    {
        frogInDamgaeArea = false;
        onPlayerExitFly?.Invoke();
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
