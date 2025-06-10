using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform inventoryTemplate;

    private void Awake()
    {
        
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == inventoryTemplate) continue;
            Destroy(child.gameObject);
                
            foreach (WeaponsSO weaponsSO in WeaponInventory.Instance.GetWeaponSOList())
            {
                Transform inventoryTransform = Instantiate(inventoryTemplate,container);
                inventoryTransform.gameObject.SetActive(true);
                inventoryTransform.GetComponent<InventroySingleUI>().SetWeaponSO(weaponsSO);
            }
        }
    }
}
