using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [SerializeField] private MonsterSO monsterSO;

    //current stats
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentDamage;

    private void Awake()
    {
        currentMoveSpeed = monsterSO.MoveSpeed;
        currentHealth = monsterSO.MaxHealth;
        currentDamage = monsterSO.Damage;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Killed();
        }
    }

    public void Killed()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}
