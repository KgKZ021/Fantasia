using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all ranged weapon behaviour.
/// </summary>
public class RangedWeaponBehaviour : MonoBehaviour
{
    protected Vector3 weaponDir;
    private float destoryAfterSeconds = 3;

    protected virtual void Start()
    {
        Destroy(gameObject, destoryAfterSeconds);
    }

    public void DirectionChecker(Vector3 direction)
    {
        weaponDir = direction;

        float dirX = weaponDir.x;
        float dirZ = weaponDir.z;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

     

        // Left
        if (dirX < 0 && dirZ == 0)
        {
            scale.y = scale.y * -1;
        }
        // Up
        else if (dirX == 0 && dirZ > 0)
        {
            rotation.y = -90;
        }
        // Down
        else if (dirX == 0 && dirZ < 0)
        {
            scale.y = scale.y * -1;
            rotation.y = -90;
        }
        // Up-Left
        else if (dirX < 0 && dirZ > 0)
        {
            scale.y = scale.y * -1;
            rotation.y = 45;
        }
        // Down-Left
        else if (dirX < 0 && dirZ < 0)
        {
            scale.y = scale.y * -1;
            rotation.y = -45;
        }
        // Up-Right
        else if (dirX > 0 && dirZ > 0)
        {
            rotation.y = -45;
        }
        // Down-Right
        else if (dirX > 0 && dirZ < 0)
        {
            rotation.y = 45;

        }

        transform.localScale = scale;


        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

}
