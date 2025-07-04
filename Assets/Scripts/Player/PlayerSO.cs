using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChracterSO", menuName = "ScriptableObjects/Character")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private GameObject startingWeapon;
    public GameObject StartingWeapon { get =>  startingWeapon; set => startingWeapon = value;}

    [SerializeField] private float maxHealth;
    public float MaxHealth{ get => maxHealth; set => maxHealth = value; }

    [SerializeField] private float recovery;
    public float Recovery { get => recovery; set => recovery = value; }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField] private float might;
    public float Might { get => might; set => might = value; }

    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }

    [SerializeField] private float magnet;
    public float Magnet { get => magnet; set => magnet = value; }

    [SerializeField] private Sprite icon;
    public Sprite Icon { get => icon; set => icon = value; }

    [SerializeField] private new string name;
    public string Name { get => name;   set => name = value; }

    [SerializeField] private string fullName;
    public string FullName { get => fullName; set => fullName = value; }

    [SerializeField] private string description;
    public string Description { get => description; set => description = value; }
}
