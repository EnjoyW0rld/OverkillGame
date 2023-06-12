using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseObject : MonoBehaviour
{

    public UnityEvent onObjectPaused;
    public UnityEvent onObjectResumed;

    // Start is called before the first frame update
    void Start()
    {
        GamePause.Instance.onGamePaused += GamePause_OnGamePaused;
        GamePause.Instance.onGameResumed += GamePause_OnGameResumed;
    }

    private void GamePause_OnGamePaused(object obj, EventArgs e)
    {
        onObjectPaused?.Invoke();
    }

    private void GamePause_OnGameResumed(object obj, EventArgs e)
    {
        onObjectResumed?.Invoke();
    }
}
