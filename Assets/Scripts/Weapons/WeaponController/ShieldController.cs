using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : BaseWeapon
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnAxe = Instantiate(GetWeaponSO().prefab);
        spawnAxe.transform.position = transform.position;
        spawnAxe.transform.parent = transform;
    }
}
