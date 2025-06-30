using UnityEngine;

public class SaveOnQuit : MonoBehaviour
{
    private static SaveOnQuit instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quitting game... saving data.");
        SaveManager.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("App paused... saving data.");
            SaveManager.Save();
        }
    }
}
