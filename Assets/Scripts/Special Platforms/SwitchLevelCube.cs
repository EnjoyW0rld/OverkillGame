using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FadeInOut;

public class SwitchLevelCube : MonoBehaviour
{
    [SerializeField] string nameScene;

    public void FadeSwitchLevel()
    {
        //Anomynous function to switch scenes with name
        FadeInOut.Instance.Fade( () => MySceneManager.SetScene(nameScene));
    }

}
