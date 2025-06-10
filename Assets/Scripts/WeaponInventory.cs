using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponInventory : MonoBehaviour
{
    public static WeaponInventory Instance {  get; private set; }

    private int maxSlots = 3;
    private float weaponDuration;

    [SerializeField]private List<WeaponsSO> storedWeapons;

    public event EventHandler OnInventoryChanged;


    private void Awake()
    {
        Instance = this;
        storedWeapons = new List<WeaponsSO>();
    }

    private void Update()
    {
        //weaponDuration -= Time.deltaTime;

        //if (weaponDuration <= 0f)
        //{
        //    foreach(WeaponsSO weaponSO in storedWeapons)
        //    {
        //        weaponDuration = weaponSO.duration;
        //    }
        //}
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

    public List<WeaponsSO> GetWeaponSOList()
    {
        return storedWeapons;
    }
}

