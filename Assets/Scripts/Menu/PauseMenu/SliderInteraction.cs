using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SliderInteraction : MonoBehaviour
{
    private Gamepad[] gamepads;
    [SerializeField] private Slider slider;
    [SerializeField,Min(1)] private float changeSpeed;
    private void Start()
    {
        if (slider == null) Debug.LogError("No slider applied to " + gameObject.name);
        gamepads = Gamepad.all.ToArray();
    }
    private void Update()
    {
        float stickValue = GetStickValue();
        if(stickValue == 0)
        {
            return;
        }
        slider.value += stickValue / changeSpeed * Time.deltaTime;
    }
    private float GetStickValue()
    {
        for (int i = 0; i < gamepads.Length; i++)
        {
            float stickValue = gamepads[i].leftStick.ReadValue().x;
            if(stickValue != 0)
            {
                return stickValue;
            }
        }
        return 0;
    }
}
