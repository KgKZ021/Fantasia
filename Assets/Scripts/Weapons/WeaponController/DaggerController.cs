using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DaggerController : BaseWeapon
{
    public static event EventHandler OnDaggerAttack;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        GameObject spawnedDagger = Instantiate(GetWeaponSO().Prefab);
        spawnedDagger.transform.position = transform.position;

        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        spawnedDagger.GetComponent<DaggerBehaviour>().DirectionChecker(
            new Vector3(GameInput.Instance.lastMovedVector.x, 0, GameInput.Instance.lastMovedVector.y)
        );
        OnDaggerAttack?.Invoke(this, EventArgs.Empty);
    }
}
