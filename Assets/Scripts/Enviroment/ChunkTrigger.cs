using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController mapController;
    [SerializeField] GameObject targetMap;


    private void Start()
    {
        mapController = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mapController.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mapController.currentChunk == targetMap)
            {
                mapController.currentChunk = null;

                Debug.Log("Inside Ontrigger Exit");
            }
        }
    }
}
