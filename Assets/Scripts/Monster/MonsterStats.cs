using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public static event EventHandler OnMonsterBite;
    public static event EventHandler OnMonsterKilledSound;

    [SerializeField] private MonsterSO monsterSO;

    //current stats
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentDamage;

    public float despawnDistance = 50f;
    Transform player;

    private float biteCooldown = 1f; // Cooldown between bites
    private float biteTimer = 0f;

    private void Awake()
    {
        currentMoveSpeed = monsterSO.MoveSpeed;
        currentHealth = monsterSO.MaxHealth;
        currentDamage = monsterSO.Damage;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        biteTimer += Time.deltaTime;

        Vector3 flatMonster = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatPlayer = new Vector3(player.position.x, 0f, player.position.z);
        float distance = Vector3.Distance(flatMonster, flatPlayer);

       
        if (distance >= despawnDistance)
        {
            ReturnMonster();
        }
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
        OnMonsterKilledSound?.Invoke(this, EventArgs.Empty);
        
        Destroy(gameObject);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && biteTimer >= biteCooldown)
        {
            biteTimer = 0f;
            OnMonsterBite?.Invoke(this, EventArgs.Empty);

          

            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);

            
        }
    }

    private void OnDestroy()
    {
        MonsterSpawner monsterSpawner = FindObjectOfType<MonsterSpawner>();
        if (monsterSpawner != null)
        {
            monsterSpawner.OnMonsterKilled();
        }
        else
        {
            Debug.LogWarning("MonsterSpawner not found in scene during OnDestroy.");
        }
    }

    //return monster to a spawn positon if the player is too far away
    private void ReturnMonster()
    {
        MonsterSpawner monsterSpawner = FindObjectOfType<MonsterSpawner>();
        transform.position = player.position + monsterSpawner.relativeSpawnPoints[UnityEngine.Random.Range(0, monsterSpawner.relativeSpawnPoints.Count)].position;
    }
}
