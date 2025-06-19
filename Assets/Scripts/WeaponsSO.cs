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
    public GameObject prefab;
    public string weaponName;
    public float duration;
    public float baseDamage;
    public float speed;
    public int pierce;
    public WeaponType weaponType;
    public DamageType damageType;
}
