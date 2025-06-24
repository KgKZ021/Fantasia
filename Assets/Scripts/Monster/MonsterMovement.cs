using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    
    [SerializeField] protected MonsterSO monsterSO;

    private Transform player;


    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, player.position, monsterSO.MoveSpeed * Time.deltaTime);
        transform.LookAt(player);
    }
}
