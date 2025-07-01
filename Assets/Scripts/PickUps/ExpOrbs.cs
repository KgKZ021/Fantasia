using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbs : PickUp
{
    public static event EventHandler OnExpOrbPickUp;

    [SerializeField] private int expGranted;
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
        player.IncreaseExperience(expGranted);

        OnExpOrbPickUp?.Invoke(this,EventArgs.Empty);
        
        
    }

}
