using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// to display player's coins
/// </summary>
public class UICoinDisplay : MonoBehaviour
{
    private TextMeshProUGUI displayTarget;
    public PlayerCollector collector;

    private void Start()
    {
        displayTarget = GetComponentInChildren<TextMeshProUGUI>();
        UpdateDisplay();
        if(collector != null)
        {
            collector.onCoinCollected += UpdateDisplay;
        }
    }

    public void UpdateDisplay()
    {
        if (collector != null)
        {
            displayTarget.text = Mathf.RoundToInt(collector.GetCoins()).ToString(); 

        }
        else
        {
            float coins = SaveManager.LastLoadedGameData.coins;
            displayTarget.text = Mathf.RoundToInt(coins).ToString();
        }
    }
    
}
