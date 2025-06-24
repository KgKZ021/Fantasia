using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUp, ICollectible
{
    [SerializeField] private int healthToRestore;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
        
    }
}
