using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<MonsterGroup> monsterGroup;
        public int waveQuota;//total number of enemies to spawn in this wave
        public float spawnInterval;
        public int spawnCount;//number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class MonsterGroup
    {
        public string monsterName;
        public int monsterCount;//number of enemies spawned in this wave
        public int spawnCount;//numbr of enemies of this type already spawned in this wave
        public GameObject monsterPrefab;
    }

    [SerializeField] private List<Wave> waves;
    [SerializeField] private int currentWaveCount; //index of current wave

    [Header("Spawner Attributes")]
    private float spawnTimer;
    [SerializeField] private int maxMonstersAllowed;
    [SerializeField] private int monstersAlive;
    [SerializeField] private bool maxMonstersReached = false;
    [SerializeField] private float waveInterval;
    private bool isWaveActive = false;

    [Header("Spawned Positions")]
    [SerializeField] public List<Transform> relativeSpawnPoints;


    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    private void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            isWaveActive = false ;
            spawnTimer = 0f;
            SpawnMonsters();
        }
    }

    IEnumerator BeginNextWave()
    {
        isWaveActive = true;

        yield return new WaitForSeconds(waveInterval);

        if(currentWaveCount<waves.Count-1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    private void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var monsterGroup in waves[currentWaveCount].monsterGroup)
        {
            currentWaveQuota += monsterGroup.monsterCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }


/// <summary>
/// This method will stop spawning if the amount of enem,ies on the map is max.
/// The method will only spawn enemies in particular waves until it is time for the next waves's enemies to be spawned
/// </summary>
    private void SpawnMonsters()
    {
        //Check if min number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxMonstersReached)
        {
            //Spawned each type of enemy till the quota is filled
            foreach (var monsterGroup in waves[currentWaveCount].monsterGroup)
            {
                //Check if the min number of enemies of thistype has been spawned
                if(monsterGroup.spawnCount < monsterGroup.monsterCount)
                {
                    Instantiate(monsterGroup.monsterPrefab, player.position + relativeSpawnPoints[UnityEngine.Random.Range(0,relativeSpawnPoints.Count)].position,Quaternion.identity);

                    monsterGroup.spawnCount++;
                    waves[currentWaveCount ].spawnCount++;
                    monstersAlive++;

                    if (monstersAlive >= maxMonstersAllowed)
                    {
                        maxMonstersReached = true;
                        return;
                    }
                }
            }
        }
        
    }

    //Call when an enemy is killed
    public void OnMonsterKilled()
    {
        monstersAlive--;
        if (monstersAlive < maxMonstersAllowed)
        {
            maxMonstersReached = false;
        }
    }
}
