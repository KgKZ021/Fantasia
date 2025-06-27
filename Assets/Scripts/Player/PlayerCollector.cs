using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private PlayerStats playerStats;
    private SphereCollider playerCollectRange;
    public float pullSpeed;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerCollectRange = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        playerCollectRange.radius = playerStats.CurrentMagnet;
    }
    private void OnTriggerEnter(Collider other)
    {
        //check other object has ICollectible interface
        if(other.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 forceDir = (transform.position - other.transform.position).normalized;
            rigidbody.AddForce(forceDir * pullSpeed);

            collectible.Collect();
        }
    }
}
