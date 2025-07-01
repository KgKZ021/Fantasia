using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private PlayerStats playerStats;
    private SphereCollider playerCollectRange;
    public float pullSpeed;

    public delegate void OnCoinCollected();
    public OnCoinCollected onCoinCollected;

    float coins;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerCollectRange = GetComponent<SphereCollider>();

        coins = 0;
    }


    public float GetCoins()
    {
        return coins;
    }

    public float AddCoins(float amount)
    {
        coins += amount;
        onCoinCollected();
        Debug.Log("Coins:"+ coins);
        return coins;
    }

    public void SaveCoinsToStash()
    {
        SaveManager.LastLoadedGameData.coins += coins;
        //coins = 0;
        SaveManager.Save();
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

            if (other.TryGetComponent(out BobbingAnimation bob))
            {
                bob.enabled = false;
            }
            Vector3 forceDir = (transform.position - other.transform.position).normalized;
            rigidbody.AddForce(forceDir * pullSpeed);

            Debug.Log("pulled");

            collectible.Collect();
        }
    }
}
