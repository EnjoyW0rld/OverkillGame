using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class EventWhenStepOn : MonoBehaviour
{
    public UnityEvent onColliderEnter;

    [SerializeField] private float delay = 1.0f;

    private bool isTrigger;
    private bool able = true;
    private IEnumerator timerCourotine;


    private void Start()
    {
        timerCourotine = Timer();
        isTrigger = GetComponent<Collider>().isTrigger;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;
        if (!able) return;

        onColliderEnter?.Invoke();
        StartCoroutine(timerCourotine);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;
        if (!able) return;
        onColliderEnter?.Invoke();
        StartCoroutine(timerCourotine);
    }

    public IEnumerator Timer()
    {
        able = false;
        yield return new WaitForSeconds(delay);
        able = true;
    }
}
