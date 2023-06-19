using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
    [ReadMe("Event is being called when player enters trigger")]
    [SerializeField] private UnityEvent assignedEvent;

    private Collider coll;

    private void Start()
    {
        Collider coll = GetComponent<Collider>();
        if (!coll.isTrigger)
        {
            Debug.LogError("Object " + name + " is not trigger! Setting it to be trigger");
            coll.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            assignedEvent?.Invoke();
        }
    }

    private void OnValidate()
    {
        if (coll == null) coll = GetComponent<Collider>();
    }
    private void OnDrawGizmosSelected()
    {
        Bounds b = coll.bounds;
        Gizmos.DrawWireCube(b.center, b.size);

    }
}
