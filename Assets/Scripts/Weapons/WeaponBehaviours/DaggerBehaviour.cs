using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerBehaviour : RangedWeaponBehaviour
{
    private DaggerController daggerController;

    protected override void Start()
    {
        base.Start();
        daggerController = FindObjectOfType<DaggerController>();
    }

    private void Update()
    {
        transform.position += weaponDir * daggerController.GetWeaponSO().speed * Time.deltaTime;
    }
}
