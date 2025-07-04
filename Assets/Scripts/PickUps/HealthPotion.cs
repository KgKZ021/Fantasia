using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUp
{
    public static event EventHandler OnHealthPotionPickUp;

    [SerializeField] private int healthToRestore;
    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }

        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);

        OnHealthPotionPickUp?.Invoke(this,EventArgs.Empty);
        
    }
}
