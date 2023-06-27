using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuHandler : MonoBehaviour
{
    private Gamepad[] gamepads;
    [SerializeField] private GameObject gamePauseCanvas, optionCanvas, controllsCanvas;
    private void Start()
    {
        gamepads = Gamepad.all.ToArray();
    }
    private void Update()
    {
        if (PressedPauseButton())
        {
            gamePauseCanvas.SetActive(!gamePauseCanvas.activeSelf);


            if (!gamePauseCanvas.activeSelf)
            {
                PauseTime(true);
                Time.timeScale = 0;
                optionCanvas.SetActive(false);
                controllsCanvas.SetActive(false);
            }
            else PauseTime(false);
        }
    }
    private bool PressedPauseButton()
    {
        for (int i = 0; i < gamepads.Length; i++)
        {
            if (gamepads[i].startButton.wasPressedThisFrame)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Use this function to pause and unpause the game time
    /// </summary>
    /// <param name="pause">true = time is not going, false = time is going</param>
    public void PauseTime(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }
}
