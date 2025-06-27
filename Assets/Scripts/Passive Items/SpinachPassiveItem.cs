using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItems
{
    protected override void ApplyModifier()
    {
        playerStats.CurrentMight *= 1 + passiveItemsSO.Mulltiplier / 100f;
        

    }
}