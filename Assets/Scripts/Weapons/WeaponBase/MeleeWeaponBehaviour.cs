using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Script of all melee behaviours
/// </summary>
public class MeleeWeaponBehaviour : MonoBehaviour
{
    private float destoryAfterSeconds = 2;

    protected virtual void Start()
    {
        Destroy(gameObject, destoryAfterSeconds);
    }   
}
