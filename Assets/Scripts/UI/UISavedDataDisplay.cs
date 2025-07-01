using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UISavedDataDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinDisplay;

    

    private void Start()
    {
        DisplayCoin();
    }

    public void DisplayCoin()
    {

        if (coinDisplay == null)
        {
            Debug.LogError("TextMeshProUGUI not assigned in the Inspector!");
            return;
        }

        // Attempt to read saved data
        if (SaveManager.LastLoadedGameData != null)
        {
            float savedCoins = SaveManager.LastLoadedGameData.coins;
            coinDisplay.text = $"{Mathf.RoundToInt(savedCoins)}";
        }
        else
        {
            coinDisplay.text = "No saved data found.";
            Debug.LogWarning("SaveManager.LastLoadedGameData is null!");
        }
    }

    private void OnEnable()
    {
        DisplayCoin();
    }
}
