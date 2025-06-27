using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Script of all melee behaviours
/// </summary>
public class MeleeWeaponBehaviour : MonoBehaviour
{
    [SerializeField] WeaponsSO weaponSO;

    private float destoryAfterSeconds = 2;

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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterStats monster = other.GetComponent<MonsterStats>();
            monster.TakeDamage(GetCurrentDamage());
        }
        else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakableProps))
            {
                breakableProps.TakeDamage(GetCurrentDamage());
            }
        }
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
}
