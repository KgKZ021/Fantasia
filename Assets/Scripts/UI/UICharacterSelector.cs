using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using System.Xml;
using Unity.VisualScripting;

public class UICharacterSelector : MonoBehaviour
{

    public PlayerSO playerSO;
    public static PlayerSO selected;

    [Header("Template")]
    public Toggle toggleTemplate;
    public string characterNamePath = "Character Name";
    public string weaponIconPath = "Weapon Icon";
    public string characterIconPath = "Character Icon";
    public List<Toggle> selectableToggle = new List<Toggle>();

    [Header("DescriptionBox")]
    [SerializeField] private TextMeshProUGUI characterFullName;
    [SerializeField] private TextMeshProUGUI characterDescription;
    [SerializeField] private Image selectedCharacterIcon;
    [SerializeField] private Image selectedCharacterWeapon;

    public static UICharacterSelector Instance { get; private set; }

    private void Start()
    {
        if (playerSO) Select(playerSO);
    }

    public static PlayerSO[] GetAllPlayerSOAssets()
    {
        List<PlayerSO> characters = new List<PlayerSO>();

        //Populate the list with PlayerSO assets
#if UNITY_EDITOR
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssetPaths)
        {
            if(assetPath.EndsWith(".asset"))
            {
                PlayerSO playerSO = AssetDatabase.LoadAssetAtPath<PlayerSO>(assetPath);
                if(playerSO != null)
                {
                    characters.Add(playerSO);
                    
                }
            }
        }
#else
        Debug.LogWarning("This function cannot be called");
#endif
        
        return characters.ToArray();
        
    }

    public static PlayerSO GetData()
    {
        if (selected)
        {
            return selected;
        }
        else
        {
            PlayerSO[] characters = GetAllPlayerSOAssets();
            if (characters.Length > 0)
            {
                Debug.Log("In here" + characters[Random.Range(0, characters.Length)]);
                return characters[Random.Range(0, characters.Length)];


            }
        }
        return null;
    }

    public void Select(PlayerSO character)
    {
        selected = character;

        characterFullName.text = character.FullName;
        characterDescription.text = character.Description;
        selectedCharacterIcon.sprite = character.Icon;
        WeaponsSO weaponSprite = character.StartingWeapon.GetComponent<WeaponsSO>();
        selectedCharacterWeapon.sprite = weaponSprite.Icon;
    }
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

    //public static PlayerSO GetData()
    //{
    //    return Instance.playerSO;
    //}

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
