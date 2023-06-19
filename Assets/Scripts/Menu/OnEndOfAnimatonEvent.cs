using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class OnEndOfAnimatonEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnComplete;
    [SerializeField] private PlayableDirector director;
    private void Update()
    {
        if (director.state == PlayState.Paused)
        {
            OnComplete?.Invoke();
        }
    }
}
