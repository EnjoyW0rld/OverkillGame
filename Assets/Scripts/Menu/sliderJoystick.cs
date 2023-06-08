using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderJoystick : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float sensitivity = 1f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        slider.value += h * sensitivity * Time.deltaTime;
    }
}
