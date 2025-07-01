using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : BaseWeapon
{

    public static event EventHandler OnShieldAttack;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnAxe = Instantiate(GetWeaponSO().Prefab);
        spawnAxe.transform.position = transform.position;
        spawnAxe.transform.parent = transform;

        OnShieldAttack?.Invoke(this,EventArgs.Empty);
    }
}
