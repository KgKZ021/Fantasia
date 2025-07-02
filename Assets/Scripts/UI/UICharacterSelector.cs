using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    // This list is filled in editor only
    private static List<PlayerSO> editorCharacterCache = new List<PlayerSO>();

#if UNITY_EDITOR
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void RefreshCharacterListEditorOnly()
    {
        editorCharacterCache.Clear();
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssetPaths)
        {
            if (assetPath.EndsWith(".asset"))
            {
                PlayerSO playerSO = AssetDatabase.LoadAssetAtPath<PlayerSO>(assetPath);
                if (playerSO != null)
                {
                    editorCharacterCache.Add(playerSO);
                }
            }
        }
    }
#endif

    public static PlayerSO GetData()
    {
        if (selected)
        {
            return selected;
        }
        else
        {
#if UNITY_EDITOR
            if (editorCharacterCache.Count == 0)
            {
                RefreshCharacterListEditorOnly();
            }

            if (editorCharacterCache.Count > 0)
            {
                PlayerSO random = editorCharacterCache[Random.Range(0, editorCharacterCache.Count)];
                Debug.Log("Selected from editor assets: " + random);
                return random;
            }
#else
            Debug.LogWarning("GetData() is trying to load editor-only assets in a build. Use runtime-safe data.");
#endif
        }
        return null;
    }

    public void Select(PlayerSO character)
    {
        selected = character;

        characterFullName.text = character.FullName;
        characterDescription.text = character.Description;
        selectedCharacterIcon.sprite = character.Icon;
        BaseWeapon controller = character.StartingWeapon.GetComponent<BaseWeapon>();
        if (controller != null && controller.weaponsSO != null)
        {
            selectedCharacterWeapon.sprite = controller.weaponsSO.Icon;
        }
        else
        {
            Debug.LogWarning("WeaponController or weaponData missing on StartingWeapon prefab.");
        }
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
