using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetSliderValueStart : MonoBehaviour
{

    Slider slider;
    [SerializeField] string playerPrefabName;


    public void SetupPercentage()
    {
        if (slider == null) slider = GetComponent<Slider>();

        if (playerPrefabName == "") Debug.LogWarning(this.name + "Doesn't have a playerprefabname!");

        float amount = PlayerPrefs.GetFloat(playerPrefabName, -1);
        slider.value = amount;

    }

}
