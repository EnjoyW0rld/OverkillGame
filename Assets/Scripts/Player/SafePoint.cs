using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafePoint : MonoBehaviour
{
    private Sanity sanity;

    [SerializeField] UnityEvent onFrogEnterGrass;

    private void Start()
    {
        sanity = FindObjectOfType<Sanity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body")
        {
            onFrogEnterGrass?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Body")
        {
            sanity.ResetSanityAmount();
            other.GetComponent<Respawning_Player>().SetGrassPoint(transform.position);
        }
    }
}
