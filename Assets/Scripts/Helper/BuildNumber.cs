using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildNumber : MonoBehaviour
{

    [SerializeField] string versionName;
    [SerializeField] float buildNumber;

    [SerializeField] TextMeshProUGUI buildTest;

    // Start is called before the first frame update
    void Start()
    {
        string buildNumberString = buildNumber.ToString();
        buildNumberString = buildNumberString.Replace(',', '.');
        Debug.Log(buildNumberString);
        buildTest.text = $" Version: {versionName} {buildNumberString}   ";
    }

}
