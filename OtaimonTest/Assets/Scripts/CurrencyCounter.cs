using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyCounter : MonoBehaviour
{
    public CurrencyColor[] colors;
    public TextMeshProUGUI[] texts;

    private void Update()
    {
        if (Inventory.updated)
        {
            UpdateTexts();
            Inventory.updated = false;
        }    
    }

    public void UpdateTexts()
    {        
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = "x " + Inventory.money[(int)colors[i]];
        }
    }    
        
}
