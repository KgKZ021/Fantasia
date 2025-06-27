using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : PassiveItems
{
    protected override void ApplyModifier()
    {
        playerStats.CurrentMoveSpeed *= 1 + passiveItemsSO.Mulltiplier / 100f; //50% movement increase is 1.5 times
    }
}
