using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColour : MonoBehaviour
{

    [SerializeField] Image image;

    [SerializeField] Color colour;
    public void ChangeColourImage()
    {
        image.color = colour;
    }
}
