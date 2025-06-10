using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private List<WeaponsSO> weaponPool;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask layerMask;

    private float spawnInterval = 5f;
    private float minSpawnRadius = 8f;
    private float maxSpawnRadius = 15f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnWeapon();
            timer = 0f;
        }
    }

    void SpawnWeapon()
    {
        if (weaponPool.Count== 0) return;

        Vector2 circle = Random.insideUnitCircle.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector3 spawnPos = new Vector3(player.position.x + circle.x, 0f, player.position.z + circle.y);

        WeaponsSO randomWeapon = weaponPool[Random.Range(0, weaponPool.Count)];
        GameObject weapon = Instantiate(randomWeapon.prefab, spawnPos, Quaternion.identity);
        weapon.layer = 6;

        Destroy(weapon,10f);
    }
}
