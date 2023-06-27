using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class EventWhenStepOn : MonoBehaviour
{
    public UnityEvent onColliderEnter;

    [SerializeField] float delay = 1.0f;
    private bool isTrigger;

    bool able = true;

    private IEnumerator coroutine;


    private void Start()
    {
        coroutine = Timer();
        isTrigger = GetComponent<Collider>().isTrigger;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;
        if (!able) return;

        onColliderEnter?.Invoke();
        StartCoroutine(coroutine);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;
        if (!able) return;
        onColliderEnter?.Invoke();
        StartCoroutine(coroutine);
    }

    public IEnumerator Timer()
    {
        able = false;
        yield return new WaitForSeconds(delay);
        able = true;
    }
}
