using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponInventory : MonoBehaviour
{
    private int maxSlots = 3;
    public List<WeaponsSO> storedWeapons;

    public event EventHandler OnInventoryChanged;


    private void Awake()
    {
        storedWeapons = new List<WeaponsSO>();
    }

    public bool TryAddPowerUp(WeaponsSO weaponSO)
    {
        if (storedWeapons.Count >= maxSlots)
        {
            return false;
        }

        storedWeapons.Add(weaponSO);
        OnInventoryChanged?.Invoke(this,EventArgs.Empty);

        return true;
    }

    public void TryUsePowerUp(int index, GameObject player)
    {
        if (index < 0 || index >= storedWeapons.Count)
        {
            return;
        }
            

        WeaponsSO weapon = storedWeapons[index];

        //weapon.Activate(player);
        storedWeapons.RemoveAt(index);
        OnInventoryChanged?.Invoke(this,EventArgs.Empty);
    }
}

