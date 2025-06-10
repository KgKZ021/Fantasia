using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : BaseWeapon
{
    BaseWeapon pickedWeapon;
    public override void Collected(Player player)
    {
        if (player.HasPickedWeapon())
        {
            //player is carrying
            BaseWeapon oldWeapon = player.GetSelectedWeapon();
            if (oldWeapon != null)
            {
                oldWeapon.DestorySelf();
            }

        }
        SetWeaponParent(player);

    }

   
}
