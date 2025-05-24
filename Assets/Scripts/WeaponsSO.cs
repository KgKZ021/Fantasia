using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    MELEE,
    RANGED
}

public enum DamageType
{
    MAGIC,
    PHYSICAL,
    TRUE_DAMAGE
}

[CreateAssetMenu]
public class WeaponsSO : ScriptableObject
{
    public Transform prefab;
    public string weaponName;
    public float duration;
    public float baseDamage;
    public WeaponType weaponType;
    public DamageType damageType;
}
