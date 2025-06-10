using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> envChunk;
    [SerializeField] private Player player;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float checkRadius;

    public GameObject currentChunk;
    private Vector3 noEnvPosition;


    [Header("Optimization")]
    [SerializeField] private List<GameObject> spawnedChunks;
    [SerializeField] private float maxOptimizationDistance;
    [SerializeField] private float optimizerCooldownDuration;

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
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        if (inputVector.x > 0 && inputVector.y == 0) // right
        {
            TrySpawnChunkAtOffset(Direction.Right);
        }
        else if (inputVector.x < 0 && inputVector.y == 0) // left
        {
            TrySpawnChunkAtOffset(Direction.Left);
        }
        else if (inputVector.x == 0 && inputVector.y > 0) // up
        {
            TrySpawnChunkAtOffset(Direction.Up);
        }
        else if (inputVector.x == 0 && inputVector.y < 0) // down
        {
            TrySpawnChunkAtOffset(Direction.Down);
        }
        else if (inputVector.x > 0 && inputVector.y > 0) // right up
        {
            TrySpawnChunkAtOffset(Direction.RightUp);
        }
        else if (inputVector.x > 0 && inputVector.y < 0) // right down
        {
            TrySpawnChunkAtOffset(Direction.RightDown);
        }
        else if (inputVector.x < 0 && inputVector.y > 0) // left up
        {
            TrySpawnChunkAtOffset(Direction.LeftUp);
        }
        else if (inputVector.x < 0 && inputVector.y < 0) // left down
        {
            TrySpawnChunkAtOffset(Direction.LeftDown);
        }

    }

    void TrySpawnChunkAtOffset(Direction direction)
    {
        string directionString = direction.ToString();
        Vector3 targetPos = currentChunk.transform.Find("StaticPoints/"+directionString).position;
        Debug.Log("target pos"+targetPos);

        Collider[] hits = Physics.OverlapSphere(targetPos, checkRadius, layerMask);
        if (hits.Length == 0)
        {
            noEnvPosition = targetPos;
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        int random = Random.Range(0, envChunk.Count);
        latestChunk = Instantiate(envChunk[random],noEnvPosition, Quaternion.identity);
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
