using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    protected PlayerStats playerStats;
    public PassiveItemsSO passiveItemsSO;

    protected virtual void ApplyModifier()
    {

    }

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();  
        ApplyModifier();
    }


}
