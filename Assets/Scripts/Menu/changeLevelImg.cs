using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeLevelImg : MonoBehaviour
{
    [SerializeField] private GameObject[] levelButtons;
    [SerializeField] private Sprite[] levelImgs;
    [SerializeField] private Image backgroundImg;

    void Start()
    {
        backgroundImg = GetComponent<Image>();
    }

    void Update()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i].activeInHierarchy)
            {
                backgroundImg.sprite = levelImgs[i];
                break;
            }
        }
    }
}
