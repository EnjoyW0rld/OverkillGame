using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangePercentageSliderText : MonoBehaviour
{
    TextMeshProUGUI percentageText;
    [SerializeField] string playerPrefabName;


    public void SetupPercentage()
    {
        float amount = PlayerPrefs.GetFloat(playerPrefabName, -1);

        if (amount == -1) return;
        
        ChangeTextPercentage(amount);
        
    }

    public void ChangeTextPercentage(float amount)
    {
        amount *= 100;

        amount = (int)amount;

        if (percentageText == null) percentageText = this.GetComponent<TextMeshProUGUI>();

        percentageText.text = amount + "%";
    }
}
