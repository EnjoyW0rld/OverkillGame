using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FadeInOut;

public class SwitchLevelCube : MonoBehaviour
{

    [SerializeField] int index;


    public void FadeSwitchLevel()
    {
        FadeInOut.Instance.Fade(MySceneManager.LoadMainMenu);

    }
}
