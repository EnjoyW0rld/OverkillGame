using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class buttonsSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] standardButtons;
    [SerializeField] private GameObject[] selectedButtons;
    [SerializeField] private Button backButton;
    [SerializeField] private AudioSource selectSound;

    private int buttonIndex = 0;
    private Button targetButton;


    public ButtonControl aButton { get; private set; }
    public ButtonControl bButton { get; private set; }

    private bool isInputAllowed = true;
    [SerializeField] private float joystickThreshold = 0.5f;

    private Gamepad game;
    private void Start()
    {
        game = Gamepad.current;
        aButton = game.aButton;
        bButton = game.bButton;
        print(Gamepad.current);
        //aButton.
        //ButtonControl a = 
    }

    void Update()
    {
        //if (game.aButton.wasPressedThisFrame) print("aaaa");
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > joystickThreshold)
        {
            SelectButton(buttonIndex - 1);
            if (selectSound!=null) {
                selectSound.Play();
            } 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < -joystickThreshold)
        {
            SelectButton(buttonIndex + 1);
            if (selectSound != null)
            {
                selectSound.Play();
            }
        }

        if (game.aButton.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Return))
        {
            print("pressed");
            for (int i = 0; i < selectedButtons.Length; i++)
            {
                if (selectedButtons[i].activeSelf)
                {
                    targetButton = selectedButtons[i].GetComponent<Button>();
                    targetButton.onClick.Invoke();
                }
            }
        }
        if (Gamepad.current.bButton.wasPressedThisFrame)
        {
            if (backButton)
            {
                targetButton = backButton.GetComponent<Button>();
                targetButton.onClick.Invoke();
                Debug.Log("go back");
                
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
