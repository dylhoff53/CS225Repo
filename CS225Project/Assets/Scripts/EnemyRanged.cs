using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRanged : MonoBehaviour
{
    public int health;
    public float attackCooldown;
    public float attackCounter;
    public bool canAttack = true;
    public int damage;
    public bool isAggro;
    public PlayerMovement pm;

    public NavMeshAgent agent;
    public GameObject deathEffect;

    public float detectionRange;
    public float currentDistanceApart;

    private void Start()
    {
        attackCooldown = 0f;
    }

    void Update()
    {
        currentDistanceApart = Vector3.Distance(transform.position, pm.transform.position);
        if (currentDistanceApart <= detectionRange)
        {
            AggroSwap();
        }
        else if (isAggro && currentDistanceApart >= detectionRange)
        {
            AggroSwap();
        }
        if (isAggro)
        {
            agent.SetDestination(pm.transform.position);
        }

        if (!canAttack)
        {
            attackCounter += Time.deltaTime;
            if (attackCounter >= attackCooldown)
            {
                canAttack = true;
                attackCounter = 0f;
            }
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

    public void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        Destroy(gameObject);
    }

    public void AggroSwap()
    {
        isAggro = !isAggro;
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
