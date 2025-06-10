using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventroySingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform icon;

    private void Awake()
    {
        icon.gameObject.SetActive(false);
    }

    public void SetWeaponSO(WeaponsSO weaponsSO)
    {
        weaponNameText.text = weaponsSO.weaponName;

        Transform iconTransform = Instantiate(icon,iconContainer);
        iconTransform.gameObject.SetActive(true);
       // iconTransform.GetComponent<Image>().sprite= weaponsSO.weaponName;
    }
}
