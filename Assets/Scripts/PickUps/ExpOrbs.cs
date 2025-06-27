using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbs : PickUp, ICollectible
{
    [SerializeField] private int expGranted;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(expGranted);
        
    }

}
