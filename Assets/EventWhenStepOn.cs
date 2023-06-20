using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWhenStepOn : MonoBehaviour
{
    public UnityEvent onColliderEnter;

    [SerializeField] float delay = 1.0f;

    bool able = true;

    private IEnumerator coroutine;


    private void Start()
    {
        coroutine = Timer();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!able) return;

        onColliderEnter?.Invoke();
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        StartCoroutine(coroutine);
    }

    public IEnumerator Timer()
    {
        able = false;
        yield return new WaitForSeconds(delay);
        able = true;
    }
}
