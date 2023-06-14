using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeSwitcher : MonoBehaviour
{


    [SerializeField] Volume volumeStart;
    [SerializeField] Volume volumeEnd;

    public void SetPercentageVolumes(float amount)
    {
        volumeStart.weight = 1 - amount;
        volumeEnd.weight = amount;
    }
}
