using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbs : MonoBehaviour, ICollectible
{
    [SerializeField] private int expGranted;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(expGranted);
        Destroy(gameObject);
    }
}
