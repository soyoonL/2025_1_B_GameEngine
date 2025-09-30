using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack,runAway }
    public EnemyState state = EnemyState.Idle;

    public float moveSpeed = 2f;
    public float traceRange = 15f; // 추적 시작 거리
    public float attackRange = 6f; // 공격 시작 거리
    
    public float attackCooldown = 1.5f;

    public Slider hpslider;
   

    public GameObject projectilePrefab; // 투사체 프리팹
    public Transform firePoint; // 발사 위치

    private Transform player;
    private float lastAttackTime;
    public float maxHP = 10;
    private float currentHP;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = attackCooldown;
        currentHP = maxHP;
        hpslider.value = 1f;
    }

    void Update()
    {
        if (player == null) return;

       
        float dist = Vector3.Distance(player.position, transform.position);

        

        switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;
            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;

            case EnemyState.Attack:
                if (dist < attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;
            case EnemyState.runAway:
                runAwayPlayer();
                if (dist>traceRange+5)
                    state = EnemyState.Idle;
                break;

        }

        
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position);
        
    }

    void runAwayPlayer()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
    

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();

            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon1"))
        {

            currentHP -= 1f;
            hpslider.value= (float)currentHP/maxHP;
        }
        if (other.CompareTag("weapon2"))
        {

            currentHP -= 3f;
            hpslider.value = (float)currentHP / maxHP;
        }
        if (currentHP <= maxHP * 0.2)
        {
            Debug.LogWarning("채력 없음");
            state = EnemyState.runAway;
            hpslider.value = (float)currentHP / maxHP;
        }
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
   

}
