using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector Instance { get; private set; }
    public PlayerSO playerSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("EXTRA" + this + "DELETED");
        }
    }

    public static PlayerSO GetData()
    {
        return Instance.playerSO;
    }

    public void SelectCharacter(PlayerSO player)
    {
        playerSO = player;
    }

    public void DestorySingleton()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
