using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Svae total number of coins collected
/// </summary>
public class SaveManager : MonoBehaviour
{
    public const float COINS_PER_SECOND = 0.01f;
    public class GameData
    {
        public float coins;
        public string lastPlayedTime;
    }

    const string SAVE_FILE_NAME = "SaveData.json";

    private static GameData lastLoadedGameData;

    public static GameData LastLoadedGameData
    {
        get
        {
            if (lastLoadedGameData == null) Load();
            return lastLoadedGameData;

        }
    }

    public static string GetSavePath()
    {
        return string.Format("{0}/{1}", Application.persistentDataPath, SAVE_FILE_NAME);
    }

    public static void Save(GameData data = null)
    {
        if(data == null)
        {
            if(lastLoadedGameData == null) Load();
            data = lastLoadedGameData;
        }

        LastLoadedGameData.lastPlayedTime = System.DateTime.Now.ToString();
        File.WriteAllText(GetSavePath(),JsonUtility.ToJson(data));
       
    }

    public static GameData Load(bool usePreviousLoadIfAvailable = false)
    {
        if(!usePreviousLoadIfAvailable && lastLoadedGameData != null)
        {
            return lastLoadedGameData;
        }

        string path = GetSavePath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            lastLoadedGameData = JsonUtility.FromJson<GameData>(json);

            if (!string.IsNullOrEmpty(LastLoadedGameData.lastPlayedTime))
            {
                DateTime lastTime = DateTime.Parse(LastLoadedGameData.lastPlayedTime);
                TimeSpan timeAway = DateTime.Now - lastTime;

                float offlineCoins = (float)timeAway.TotalSeconds * COINS_PER_SECOND;

                Debug.Log($"You were away for {timeAway.TotalMinutes:F1} minutes. Gained {offlineCoins:F1} coins.");
                LastLoadedGameData.coins += offlineCoins;
            }

            if (lastLoadedGameData == null) lastLoadedGameData = new GameData();
        }
        else
        {
            lastLoadedGameData = new GameData();
        }
        Debug.Log("Save path: " + Application.persistentDataPath);

        return lastLoadedGameData;
    }
}
