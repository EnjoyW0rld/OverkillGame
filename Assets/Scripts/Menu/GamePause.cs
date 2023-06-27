using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GamePause : MonoBehaviour
{
    public static GamePause Instance;

    public EventHandler onGamePaused;
    public EventHandler onGameResumed;

    [SerializeField] private KeyCode keyToPause;

    [SerializeField] private Gamepad pad;


    private bool isPaused = false;


    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.LogError("ALREADY A GAMEPAUSE IN SCENE DESTROYING!" + this.name);

            return;
        }

        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPause))
        {
            //If paused unpause otherwise pause
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
            else ResumeGame();


        }
    }


    //Double sets pause so the method can be called independently or on the update
    public void PauseGame()
    {
        isPaused = true; 
        onGamePaused.Invoke(this, EventArgs.Empty);
    }

    public void ResumeGame()
    {
        isPaused = false;
        onGameResumed?.Invoke(this, EventArgs.Empty);
    }
}
