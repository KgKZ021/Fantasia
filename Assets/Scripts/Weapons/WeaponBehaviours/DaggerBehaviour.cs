using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerBehaviour : RangedWeaponBehaviour
{
    

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.position += weaponDir * weaponSO.Speed * Time.deltaTime;
    }
}
