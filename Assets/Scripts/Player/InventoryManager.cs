using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<BaseWeapon> weaponSlots = new List<BaseWeapon>(6); //Maximum 6 slots
    public List<PassiveItems> passiveItemSlots = new List<PassiveItems>(6);

    public int[] weaponLevels = new int[6];
    public int[] passiveItemLevels = new int[6];

    public List<Image> weaponUISlots = new List<Image>(6);
    public List<Image> passiveItemUISlots = new List<Image>(6);

    public void AddWeapon(int slotIndex, BaseWeapon weapon)
    {
        weaponSlots[slotIndex] = weapon;

        weaponLevels[slotIndex] = weapon.weaponsSO.Level;

        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponsSO.Icon;
    }

    public void AddPassiveItems(int slotIndex, PassiveItems passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;

        passiveItemLevels[slotIndex] = passiveItem.passiveItemsSO.Level;

        passiveItemUISlots[slotIndex].enabled = true;
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemsSO.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            BaseWeapon weapon = weaponSlots[slotIndex];

            if(!weapon.weaponsSO.NextLevelPrefab)
            {
                Debug.LogError("No next level for" + weapon.name);
                return;
            }

            GameObject upgradedWeapon = Instantiate(weapon.weaponsSO.NextLevelPrefab, transform.position,Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<BaseWeapon>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<BaseWeapon>().weaponsSO.Level; //to make sure the we have the correct level
        }
    }

    public void LevelUpPassiveItems(int slotIndex)
    {
        if (passiveItemSlots.Count > slotIndex)
        {
            PassiveItems passiveItems = passiveItemSlots[slotIndex];
            if (!passiveItems.passiveItemsSO.NextLevelPrefab)
            {
                Debug.LogError("No next level for" + passiveItems.name);
                return;
            }

            GameObject upgradedPassiveItem = Instantiate(passiveItems.passiveItemsSO.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPassiveItems(slotIndex, upgradedPassiveItem.GetComponent<PassiveItems>());
            Destroy(passiveItems.gameObject);
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItems>().passiveItemsSO.Level; //to make sure the we have the correct level
        }
    }
}
