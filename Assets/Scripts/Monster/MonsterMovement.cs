using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Transform player;


    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        transform.LookAt(player);
    }
}
