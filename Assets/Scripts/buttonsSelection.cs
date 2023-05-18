using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class buttonsSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] standardButtons;
    [SerializeField] private GameObject[] selectedButtons;
    [SerializeField] private GameObject creditsScene;

    private int buttonIndex = 0;

    public ButtonControl aButton { get; }
    public ButtonControl bButton { get; }

    private bool isInputAllowed = true;
    [SerializeField] private float joystickThreshold = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > joystickThreshold)
        {
            SelectButton(buttonIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < -joystickThreshold)
        {
            SelectButton(buttonIndex + 1);
        }

        if (Gamepad.current.aButton.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Return))
        {
            if (GameObject.Find("playSelected"))
            {
                //start game
                Debug.Log("game start");
            }
            else if (GameObject.Find("OptionsSelected"))
            {
                //open options
                Debug.Log("options");
            }
            else if (GameObject.Find("exitSelected"))
            {
                //exit game
                Debug.Log("exit game");
            }
            else if (GameObject.Find("creditsSelected"))
            {
                //credits
                Debug.Log("credits");
                creditsScene.SetActive(true);
            }
        }
        if (Gamepad.current.bButton.wasPressedThisFrame)
        {
            if (GameObject.Find("creditsPage"))
            {
                creditsScene.SetActive(false);
            }
        }
    }

    private void SelectButton(int index)
    {

        if (!isInputAllowed)
            return;

        // Wrap the index to loop within valid bounds
        index = WrapIndex(index, standardButtons.Length);

        // Deactivate the previously selected button
        standardButtons[buttonIndex].SetActive(true);
        selectedButtons[buttonIndex].SetActive(false);

        // Activate the newly selected button
        standardButtons[index].SetActive(false);
        selectedButtons[index].SetActive(true);

        // Update the current selected index
        buttonIndex = index;

        isInputAllowed = false;
        Invoke("AllowInput", 0.2f);
    }

    private int WrapIndex(int index, int length)
    {
        // Wrap the index to loop within valid bounds
        if (index < 0)
        {
            index = length - 1;
        }
        else if (index >= length)
        {
            index = 0;
        }

        return index;
    }

    private void AllowInput()
    {
        isInputAllowed = true;
    }

}
