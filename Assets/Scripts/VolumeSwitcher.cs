using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class VolumeSwitcher : MonoBehaviour
{
    [SerializeField] private Volume volumeStart;
    [SerializeField] private Volume volumeEnd;

    public void SetPercentageVolumes(float amount)
    {
        volumeStart.weight = 1 - amount;
        volumeEnd.weight = amount;
    }
}
