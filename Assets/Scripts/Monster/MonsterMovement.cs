using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private MonsterStats monsterStats;
    private Transform player;


    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        monsterStats = GetComponent<MonsterStats>();
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, player.position, monsterStats.currentMoveSpeed * Time.deltaTime);
        transform.LookAt(player);
    }
}
