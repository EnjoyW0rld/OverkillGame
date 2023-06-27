using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildNumber : MonoBehaviour
{

    [SerializeField] private string versionName;
    [SerializeField] private float buildNumber;
    [SerializeField] private TextMeshProUGUI buildTest;

    // Start is called before the first frame update
    void Start()
    {
        string buildNumberString = buildNumber.ToString();
        buildNumberString = buildNumberString.Replace(',', '.');
        buildTest.text = $" Version: {versionName} {buildNumberString}   ";
    }

}
