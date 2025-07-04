using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all scene transistion in game
/// </summary>
public class SceneController : MonoBehaviour
{
    
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1.0f;
    }

    public void ApplicationExit()
    {
        Application.Quit();
    }
}
