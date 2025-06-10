using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedWeaponVisual : MonoBehaviour
{
    [SerializeField] private BaseWeapon baseWeapon;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {

        Player.Instance.OnSelectedWeaponChanged += Instance_OnSelectedWeaponChanged;
    }

    private void Instance_OnSelectedWeaponChanged(object sender, Player.OnSelectedWeaponChangedEventArgs e)
    {
        if (e.selectedWeapon == null || baseWeapon == null)
        {
            Hide();
            return;
        }

        if (e.selectedWeapon == baseWeapon)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObj in visualGameObjectArray)
        {
            if (visualGameObj != null)
            {
                visualGameObj.SetActive(true);
            }
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObj in visualGameObjectArray)
        {
            if (visualGameObj != null)
            {
                visualGameObj.SetActive(false);
            }

        }
    }
}
