using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    /// <summary>
    /// Changes the scene to the one with the same name
    /// </summary>
    /// <param name="name">Name of scene to switch to</param>
    public static void SetScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.X)) ReloadScene();
    }

}
