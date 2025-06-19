using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DaggerController : BaseWeapon
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        GameObject spawnedDagger = Instantiate(GetWeaponSO().prefab);
        spawnedDagger.transform.position = transform.position;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        spawnedDagger.GetComponent<DaggerBehaviour>().DirectionChecker(new Vector3(gameInput.lastMovedVector.x,0,gameInput.lastMovedVector.y));
    }
}
