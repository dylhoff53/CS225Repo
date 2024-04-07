using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
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

    [Header("Ranged Enemy Info")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    public enum state
    {
        moving,
        still,
        charging,
        
    }

    private void Start()
    {
        currentState = state.moving;
    }

    void Update()
    {
        currentDistanceApart = Vector3.Distance(transform.position, pm.transform.position);

        if (!canAttack)
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

            case (state.moving):
                agent.SetDestination(pm.transform.position);
                TryShoot();
                break;

            case (state.charging):
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
        if (health <= 0)
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
        Destroy(gameObject);
    }

    public void TryShoot()
    {
        Vector3 localDir = Quaternion.Inverse(transform.rotation) * (pm.transform.position - transform.position);

        bool isForward = localDir.z > 0;
        bool isRight = localDir.x > 0;

        if (isForward)
        {
            Debug.Log("IsForward");
            Debug.Log(localDir.z);
        }
        else
        {
            Debug.Log("IsBehind");
            Debug.Log(localDir.z);

        }
        if (isRight)
        {
            Debug.Log("IsRight");
            Debug.Log(localDir.x);
        }
        else
        {
            Debug.Log("IsLeft");
            Debug.Log(localDir.x);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
            AttackCheck(other);
    }

    private void OnTriggerStay(Collider other)
    {
            AttackCheck(other);
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
