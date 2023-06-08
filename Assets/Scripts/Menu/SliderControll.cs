using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SliderControll : MonoBehaviour
{
    [SerializeField] private Slider targetSlider;
    [SerializeField] private float stepValue = 0.1f;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        Vector2 values = Gamepad.current.leftStick.ReadValue();
        if(values.x < 0)
        {
            targetSlider.value -= stepValue * Time.deltaTime;
            text.text = (int)(targetSlider.value * 100f) + "%";
        }
        if(values.x > 0)
        {
            targetSlider.value += stepValue * Time.deltaTime;
            text.text = (int)(targetSlider.value * 100f) + "%";

        }
    }
}
