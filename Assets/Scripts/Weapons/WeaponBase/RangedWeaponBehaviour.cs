using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all ranged weapon behaviour.
/// </summary>
public class RangedWeaponBehaviour : MonoBehaviour
{
    [SerializeField]protected WeaponsSO weaponSO;

    protected Vector3 weaponDir;
    private float destoryAfterSeconds = 3;

    //Current Stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentDuration;
    protected float currentPierce;

    private void Awake()
    {
        if (weaponSO == null)
        {
            Debug.LogError($"{gameObject.name}: weaponSO is not assigned!");
            return;
        }
        currentDamage = weaponSO.BaseDamage;
        currentSpeed = weaponSO.Speed;
        currentDuration = weaponSO.Duration;
        currentPierce = weaponSO.Pierce;
    }
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterStats monster = other.GetComponent<MonsterStats>();
            monster.TakeDamage(GetCurrentDamage());

            ReducePierce();
        }

        else if(other.CompareTag("Prop"))
        {
            if(other.gameObject.TryGetComponent(out BreakableProps breakableProps))
            {
                breakableProps.TakeDamage(GetCurrentDamage());

                ReducePierce();
            }
        }
    }

    private void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
}
