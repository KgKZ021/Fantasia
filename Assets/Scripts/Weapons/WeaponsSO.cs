using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponSO" , menuName = "ScriptableObjects/Weapon")]
public class WeaponsSO : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get => prefab;private set => prefab = value; }

    [SerializeField] private string weaponName;
    public string WeaponName { get => weaponName; private set => weaponName = value; }

    [SerializeField] private float duration;
    public float Duration { get => duration; private set => duration = value; }

    [SerializeField] private float baseDamage;
    public float BaseDamage { get => baseDamage; private set => baseDamage = value; }

    [SerializeField] private float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField] private int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }
    
}
