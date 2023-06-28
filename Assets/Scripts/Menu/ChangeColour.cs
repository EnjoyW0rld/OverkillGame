using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColour : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color colour;

    public void ChangeColourImage()
    {
        image.color = colour;
    }
}
