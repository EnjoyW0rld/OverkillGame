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
                optionCanvas.SetActive(false);
                controllsCanvas.SetActive(false);
            }
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
}
