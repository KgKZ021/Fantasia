using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : PickUp
{
    private PlayerCollector collector;
    public int coins = 1;

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
        collector = player.GetComponentInChildren<PlayerCollector>();
        if (collector != null) collector.AddCoins(coins);
        collector.SaveCoinsToStash();
    }
}
