using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health;
    public float attackCooldown;
    public float attackCounter;
    public bool canAttack = true;
    public int damage;
    public PlayerMovement pm;

    public NavMeshAgent agent;
    public GameObject deathEffect;

    public float detectionRange;
    public float currentDistanceApart;
    public Vector3 aimDirection;

    public state currentState;

    public Barrier myBarrier;

    [Header("Ranged Enemy Info")]
    public bool isRangedEnemy;
    public float rangedAttackRange;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public enum state
    {
        idle,
        moving,
        attacking
    }

    private void Start()
    {
        currentState = state.idle;
    }

    void Update()
    {
        currentDistanceApart = Vector3.Distance(transform.position, pm.transform.position);


        if(!canAttack)
        {
            attackCounter += Time.deltaTime;
            if (attackCounter >= attackCooldown)
            {
                canAttack = true;
                attackCounter = 0f;
            }
        }

        StateMachine();

        
        
    }

    public void StateMachine()
    {
        switch (currentState)
        {
            case (state.idle):
                if (currentDistanceApart <= detectionRange)
                {
                    currentState = state.moving;
                }

                break;

            case (state.moving):

                if (currentDistanceApart > detectionRange)
                {
                    currentState = state.idle;
                } else if(currentDistanceApart <= rangedAttackRange && isRangedEnemy)
                {
                    currentState = state.attacking;
                }
                agent.SetDestination(pm.transform.position);

                break;

            case (state.attacking):
                if(currentDistanceApart > rangedAttackRange)
                {
                    currentState = state.moving;
                }
                var lookPos = pm.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                aimDirection = pm.transform.position;
                aimDirection.y = 0f;
                if (aimDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
                }
                if (canAttack)
                {
                    canAttack = false;
                    Fire();
                }


                break;

            default:

                break;
        }
    }
    public void Hit(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Death();
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        if(myBarrier != null)
        {
            myBarrier.anEnemyDied();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isRangedEnemy)
        {
            AttackCheck(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isRangedEnemy)
        {
            AttackCheck(other);
        }
    }


    public void AttackCheck(Collider oth)
    {
        if (oth.tag == "Player" && canAttack)
        {
            oth.GetComponent<PlayerMovement>().GotHit(damage);
            canAttack = false;
        }
    }
}
