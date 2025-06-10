using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    [SerializeField] List<GameObject> propSpawnPoint;
    [SerializeField] List<GameObject> propPrefab;

    private void Start()
    {
        SpawnProps();
    }
    private void SpawnProps()
    {
        foreach (GameObject spawnPoint in propSpawnPoint)
        {
            int random = Random.Range(0, propPrefab.Count);
            GameObject prop = Instantiate(propPrefab[random], spawnPoint.transform.position, Quaternion.identity);

           prop.transform.parent = spawnPoint.transform;
        }
    }
}
