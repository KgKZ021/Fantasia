using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all scene transistion in game
/// </summary>
public class SceneController : MonoBehaviour
{
    [SerializeField] private UISavedDataDisplay savedDataDisplay;

    private void Start()
    {
        savedDataDisplay = GetComponent<UISavedDataDisplay>();
    }
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1.0f;
    }
}
