using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> envChunk;
    [SerializeField] private Player player;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float checkRadius;

    public GameObject currentChunk;
    

    private Vector3 playerLastPosition;


    [Header("Optimization")]
   
    [SerializeField] private float maxOptimizationDistance;
    [SerializeField] private float optimizerCooldownDuration;

    public List<GameObject> spawnedChunks;
    private GameObject latestChunk;
    private float optimizationDistance;
    private float optimizerCooldown;
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down,
        RightUp,
        RightDown,
        LeftUp,
        LeftDown
    }

    private void Start()
    {
        playerLastPosition = player.transform.position;
    }

    private void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

   
    private void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }
        
        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        Vector3 targetPos = currentChunk.transform.Find("StaticPoints/" + directionName).position;

        Collider[] hits = Physics.OverlapSphere(targetPos, checkRadius, layerMask);
        
        if (hits.Length == 0)
        {
            SpawnChunk(targetPos);

            //adjecent chunks of diagonal direction

            CheckAndSpawnChunk(directionName);

            if(directionName.Contains("Up"))
            {
                CheckAndSpawnChunk("Up");
            }
            if (directionName.Contains("Down"))
            {
                CheckAndSpawnChunk("Down");
            }
            if (directionName.Contains("Right"))
            {
                CheckAndSpawnChunk("Right");
            }
            if (directionName.Contains("Left"))
            {
                CheckAndSpawnChunk("Left");
            }
        }

    }

    private void CheckAndSpawnChunk(string direction)
    {
        Collider[] upCheck = Physics.OverlapSphere(currentChunk.transform.Find("StaticPoints/" + direction).position, checkRadius, layerMask);
        if (upCheck.Length == 0)
        {
            SpawnChunk(currentChunk.transform.Find("StaticPoints/" + direction).position);
        }
    }
    private string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            if(direction.z > 0.5f)
            {
                return direction.x > 0 ? "RightUp" : "LeftUp";
            }
            else if(direction.z < -0.5f)
            {
                return direction.x > 0 ? "RightDown" : "LeftDown";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.z > 0 ? "RightUp" : "RightDown";
            }
            else if (direction.x < -0.5f)
            {
                return direction.z > 0 ? "LeftUp" : "LeftDown";
            }
            else
            {
                return direction.z > 0 ? "Up" : "Down";
            }
        }
    }

    //void TrySpawnChunkAtOffset(Direction direction)
    //{
    //    string directionString = direction.ToString();
    //    Vector3 targetPos = currentChunk.transform.Find("StaticPoints/"+directionString).position;
        

    //    Collider[] hits = Physics.OverlapSphere(targetPos, checkRadius, layerMask);
    //    if (hits.Length == 0)
    //    {
    //        noEnvPosition = targetPos;
    //        SpawnChunk();
    //    }
    //}

    private void SpawnChunk(Vector3 spawnPosition)
    {
        int random = Random.Range(0, envChunk.Count);
        latestChunk = Instantiate(envChunk[random], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);

    }

    private void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown < 0)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            optimizationDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (optimizationDistance > maxOptimizationDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
