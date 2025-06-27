using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<BaseWeapon> weaponSlots = new List<BaseWeapon>(6); //Maximum 6 slots
    public List<PassiveItems> passiveItemSlots = new List<PassiveItems>(6);

    public int[] weaponLevels = new int[6];
    public int[] passiveItemLevels = new int[6];

    public List<Image> weaponUISlots = new List<Image>(6);
    public List<Image> passiveItemUISlots = new List<Image>(6);

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponsSO weaponsSO;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public int passiveItemUpgradeIndex;
        public GameObject initialPassiveItem;
        public PassiveItemsSO passiveItemsSO;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI upgradeNameDisplay;
        public TextMeshProUGUI upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }


    public void AddWeapon(int slotIndex, BaseWeapon weapon)
    {
        weaponSlots[slotIndex] = weapon;

        weaponLevels[slotIndex] = weapon.weaponsSO.Level;

        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponsSO.Icon;

        if(GameManager.Instance != null && GameManager.Instance.choosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void AddPassiveItems(int slotIndex, PassiveItems passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;

        passiveItemLevels[slotIndex] = passiveItem.passiveItemsSO.Level;

        passiveItemUISlots[slotIndex].enabled = true;
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemsSO.Icon;

        if (GameManager.Instance != null && GameManager.Instance.choosingUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
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

            weaponUpgradeOptions[upgradeIndex].weaponsSO = upgradedWeapon.GetComponent <BaseWeapon>().weaponsSO;

            if (GameManager.Instance != null && GameManager.Instance.choosingUpgrade)
            {
                GameManager.Instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItems(int slotIndex, int upgradeIndex)
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

            passiveItemUpgradeOptions[upgradeIndex].passiveItemsSO = upgradedPassiveItem.GetComponent <PassiveItems>().passiveItemsSO;

            if (GameManager.Instance != null && GameManager.Instance.choosingUpgrade)
            {
                GameManager.Instance.EndLevelUp();
            }
        }
    }

    private void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrade = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<PassiveItemUpgrade> availablePassiveItemsUpgrades = new List<PassiveItemUpgrade>(passiveItemUpgradeOptions);

        foreach (var upgradeOptions in upgradeUIOptions)
        {
            if(availableWeaponUpgrade.Count == 0 && availablePassiveItemsUpgrades.Count == 0)
            {
                return;
            }

            int upgradeType;

            if(availableWeaponUpgrade.Count == 0)
            {
                upgradeType = 2;
            }
            else if(availablePassiveItemsUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3); //Choose between weapon and items
            }

            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrade[Random.Range(0, availableWeaponUpgrade.Count)];

                availableWeaponUpgrade.Remove(chosenWeaponUpgrade);

                if (chosenWeaponUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOptions);

                    bool newWeapon = false;

                    for (int i = 0; i < weaponSlots.Count; i++)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponsSO == chosenWeaponUpgrade.weaponsSO)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                if (!chosenWeaponUpgrade.weaponsSO.NextLevelPrefab) //at max level
                                {
                                    DisableUpgradeUI(upgradeOptions);
                                    break;
                                }

                                upgradeOptions.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex));

                                upgradeOptions.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponsSO.NextLevelPrefab.GetComponent<BaseWeapon>().weaponsSO.Description;
                                upgradeOptions.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponsSO.NextLevelPrefab.GetComponent<BaseWeapon>().weaponsSO.Name;

                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon)
                    {
                        upgradeOptions.upgradeButton.onClick.AddListener(() => playerStats.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));

                        upgradeOptions.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponsSO.Description;
                        upgradeOptions.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponsSO.Name;
                    }

                    upgradeOptions.upgradeIcon.sprite = chosenWeaponUpgrade.weaponsSO.Icon;
                }
            }
            else if (upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade = availablePassiveItemsUpgrades[Random.Range(0, availablePassiveItemsUpgrades.Count)];

                availablePassiveItemsUpgrades.Remove(chosenPassiveItemUpgrade);

                if (chosenPassiveItemUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOptions);

                    bool newPassiveItems = false;

                    for (int i = 0; i < passiveItemSlots.Count; i++)
                    {
                        if (passiveItemSlots[i] != null && passiveItemSlots[i].passiveItemsSO == chosenPassiveItemUpgrade.passiveItemsSO)
                        {
                            newPassiveItems = false;

                            if (!newPassiveItems)
                            {
                                if(!chosenPassiveItemUpgrade.passiveItemsSO.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOptions);
                                    break;
                                }

                                upgradeOptions.upgradeButton.onClick.AddListener(() => LevelUpPassiveItems(i, chosenPassiveItemUpgrade.passiveItemUpgradeIndex));

                                upgradeOptions.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemsSO.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemsSO.Description;
                                upgradeOptions.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemsSO.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemsSO.Name;

                            }
                            break;
                        }
                        else
                        {
                            newPassiveItems = true;
                        }
                    }
                    if (newPassiveItems)
                    {
                        upgradeOptions.upgradeButton.onClick.AddListener(() => playerStats.SpawnPassiveItems(chosenPassiveItemUpgrade.initialPassiveItem));

                        upgradeOptions.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemsSO.Description;
                        upgradeOptions.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemsSO.Name;
                    }

                    upgradeOptions.upgradeIcon.sprite = chosenPassiveItemUpgrade.passiveItemsSO.Icon;
                }
            }
        }
    }
    private void RemoveUpgradeOptions()
    {
        foreach(var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption);
        }
    }

    public void RemoveAndApplyUpgrade()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    private void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }

    private void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }
}
