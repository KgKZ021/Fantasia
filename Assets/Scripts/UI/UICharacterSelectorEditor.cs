using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

[DisallowMultipleComponent]
[CustomEditor(typeof(UICharacterSelector))]
public class UICharacterSelectorEditor : Editor
{
    private UICharacterSelector selector;

    private void OnEnable()
    {
        selector = target as UICharacterSelector;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate Selectable Characters"))
        {
            CreateTogglesForCharacterData();
        }

    }

    public void CreateTogglesForCharacterData()
    {
        if (!selector.toggleTemplate)
        {
            Debug.Log("Assign toggle template");
            return;
        }

        for(int i =  selector.toggleTemplate.transform.parent.childCount - 1; i >= 0; i--)
        {
            Toggle tog = selector.toggleTemplate.transform.parent.GetChild(i).GetComponent<Toggle>();
            if(tog == selector.toggleTemplate)
            {
                continue;
            }
            Undo.DestroyObjectImmediate(tog.gameObject);
        }

        Undo.RecordObject(selector, "Updates to UICharacterSelector");
        selector.selectableToggle.Clear();
        PlayerSO[] characters = UICharacterSelector.GetAllPlayerSOAssets();

        for (int i = 0; i < characters.Length; i++)
        {
            Toggle toggle;
            if (i == 0)
            {
                toggle = selector.toggleTemplate;
                Undo.RecordObject(toggle, "Modifying the Template");
            }
            else
            {
                toggle = Instantiate(selector.toggleTemplate, selector.toggleTemplate.transform.parent);
                Undo.RegisterCreatedObjectUndo(toggle.gameObject, "Created a anew toggle");
            }

            Transform characterName = toggle.transform.Find(selector.characterNamePath);
            if (characterName && characterName.TryGetComponent(out TextMeshProUGUI tmp))
            {
                tmp.text = toggle.gameObject.name = characters[i].Name;
            }

            Transform characterIcon = toggle.transform.Find(selector.characterIconPath);
            if (characterIcon && characterIcon.TryGetComponent(out Image chrIcon))
            {
                chrIcon.sprite = characters[i].Icon;
            }

            Transform weaponIcon = toggle.transform.Find(selector.weaponIconPath);
            if (weaponIcon && weaponIcon.TryGetComponent(out Image wpnIcon))
            {

                string prefabPath = AssetDatabase.GetAssetPath(characters[i].StartingWeapon);
                if (!string.IsNullOrEmpty(prefabPath))
                {
                    GameObject prefabRoot = PrefabUtility.LoadPrefabContents(prefabPath);

                    WeaponsSO foundWeaponSO = null;

                    // Go through all MonoBehaviours and check for a field of type WeaponsSO
                    foreach (var component in prefabRoot.GetComponents<MonoBehaviour>())
                    {
                        if (component == null) continue;

                        var fields = component.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                        foreach (var field in fields)
                        {
                            if (field.FieldType == typeof(WeaponsSO))
                            {
                                foundWeaponSO = field.GetValue(component) as WeaponsSO;
                                if (foundWeaponSO != null) break;
                            }
                        }

                        if (foundWeaponSO != null) break;
                    }

                    if (foundWeaponSO != null)
                    {
                        wpnIcon.sprite = foundWeaponSO.Icon;
                    }
                    else
                    {
                        Debug.LogWarning("No WeaponsSO found in prefab.");
                    }

                    PrefabUtility.UnloadPrefabContents(prefabRoot);
                }

            }

            selector.selectableToggle.Add(toggle);

            for (int j = 0; j < toggle.onValueChanged.GetPersistentEventCount(); j++)
            {
                if (toggle.onValueChanged.GetPersistentMethodName(j) == "Select")
                {
                    UnityEventTools.RemovePersistentListener(toggle.onValueChanged, j);
                }
            }
            UnityEventTools.AddObjectPersistentListener(toggle.onValueChanged, selector.Select, characters[i]);
        }
        EditorUtility.SetDirty(selector);

    }
}
