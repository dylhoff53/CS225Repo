using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{
    public int health;
    public int damage;
    public PlayerMovement pm;
    public Barrier myBarrier;

    public NavMeshAgent agent;
    public GameObject deathEffect;

    public float detectionRange;
    public float currentDistanceApart;
    public Vector3 aimDirection;

    public state currentState;

    [Header("Ranged Enemy Info")]
    public GameObject leftBulletPrefab;
    public GameObject rightBulletPrefab;
    public Transform leftFirePoint;
    public Transform rightFirePoint;

    public float CannonBuffer;
    public GameObject rightCannon;
    public GameObject leftCannon;

    public bool canAttackLeft = true;
    public bool canAttackRight = true;
    public float attackCounterLeft;
    public float attackCounterRight;
    public float leftAttackCooldown;
    public float rightAttackCooldown;

    public float chargeCounter;
    public float chargeBuildUp;
    public float chargeSpeed;
    public float chargingCounter;
    public float chargingDuration;
    public bool isCharging;

    public bool canAttack = true;
    public float attackCooldown;
    public float attackCounter;
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

        StateMachine();

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

    public void StateMachine()
    {
        switch (currentState)
        {
            case (state.moving):
                agent.SetDestination(pm.transform.position);
                TryShoot();
                Invoke("StateSwap", 5.0f);
                break;

            case (state.charging):
                if (!isCharging)
                {
                    var lookPos = pm.transform.position - transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    aimDirection = pm.transform.position;
                    aimDirection.y = 0f;
                    if (aimDirection != Vector3.zero)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
                    }

                    chargeCounter += Time.deltaTime;
                    if (chargeCounter >= chargeBuildUp && !isCharging)
                    {
                        chargeCounter = 0f;
                        isCharging = true;
                    }
                }

                if (isCharging)
                {
                    Vector3 chargeVector = transform.forward * chargeSpeed * Time.deltaTime;
                    chargeVector.y = 0;
                    transform.Translate(chargeVector, Space.World);
                    chargingCounter += Time.deltaTime;
                    if(chargingCounter >= chargingDuration)
                    {
                        chargingCounter = 0f;
                        isCharging = false;
                        currentState = state.moving;
                    }
                    TryShoot();
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

    public void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        myBarrier.anEnemyDied();
        Destroy(gameObject);
    }

    public void StateSwap()
    {
        currentState = state.charging;
    }

    public void TryShoot()
    {
        Vector3 localDir = Quaternion.Inverse(transform.rotation) * (pm.transform.position - transform.position);

        var lookPos = pm.transform.position - rightCannon.transform.position;
        var lookPosLeft = pm.transform.position - leftCannon.transform.position;
        //lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        var rotationLeft = Quaternion.LookRotation(lookPosLeft);
        aimDirection = pm.transform.position;
        //aimDirection.y = 0f;
        bool isForward = localDir.z > 0;
        bool isRight = localDir.x > 0;
        if (aimDirection != Vector3.zero && isRight)
        {
            rightCannon.transform.rotation = Quaternion.Slerp(rightCannon.transform.rotation, rotation, 0.02f);
            if (!canAttackRight)
            {
                attackCounterRight += Time.deltaTime;
                if (attackCounterRight >= rightAttackCooldown)
                {
                    canAttackRight = true;
                    attackCounterRight = 0f;
                }
            }
        }
        if (aimDirection != Vector3.zero && !isRight)
        {
            leftCannon.transform.rotation = Quaternion.Slerp(leftCannon.transform.rotation, rotationLeft, 0.02f);
            if (!canAttackLeft)
            {
                attackCounterLeft += Time.deltaTime;
                if (attackCounterLeft >= leftAttackCooldown)
                {
                    canAttackLeft = true;
                    attackCounterLeft = 0f;
                }
            }
        }

        if (canAttackLeft)
        {
            canAttackLeft = false;
            GameObject bullet = Instantiate(leftBulletPrefab, leftFirePoint.position, leftFirePoint.rotation);
        }
        if (canAttackRight)
        {
            canAttackRight = false;
            GameObject bullet = Instantiate(rightBulletPrefab, rightFirePoint.position, rightFirePoint.rotation);
        }


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
        if (oth.tag == "Player" && canAttack && currentState == state.charging && isCharging)
        {
            oth.GetComponent<PlayerMovement>().GotHit(damage);
            canAttack = false;
        }
    }
}
