using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;   
    private Transform player;
    public float EnemyHP = 5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

       
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon1"))
        {

            EnemyHP -= 1;
        }
        if (other.CompareTag("weapon2"))
        {

            EnemyHP -= 3;
        }
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
