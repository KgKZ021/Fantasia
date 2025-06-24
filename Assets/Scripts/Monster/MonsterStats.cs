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

    public float despawnDistance = 50f;
    Transform player;

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
        Vector3 flatMonster = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatPlayer = new Vector3(player.position.x, 0f, player.position.z);
        float distance = Vector3.Distance(flatMonster, flatPlayer);

        Debug.Log(distance);
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

    private void OnDestroy()
    {
        MonsterSpawner monsterSpawner = FindObjectOfType<MonsterSpawner>();
        monsterSpawner.OnMonsterKilled();
    }

    //return monster to a spawn positon if the player is too far away
    private void ReturnMonster()
    {
        MonsterSpawner monsterSpawner = FindObjectOfType<MonsterSpawner>();
        transform.position = player.position + monsterSpawner.relativeSpawnPoints[Random.Range(0, monsterSpawner.relativeSpawnPoints.Count)].position;
    }
}
