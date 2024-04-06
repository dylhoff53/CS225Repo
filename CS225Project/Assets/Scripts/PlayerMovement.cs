using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 playerInput;
    public Vector2 lastInput;
    public state currentState;
    public float speed;
    //public CharacterController controller;

    public float xAxis;
    public float zAxis;
    public Transform Cam;
    private Vector3 rotationTarget;
    private Vector2 mouseLook;
    public Vector2 dashDirection;
    public LayerMask mask;
    public Attack firstAttack;
    public Attack secondAttack;
    public int attackCount1;
    public int attackCount2;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float attackCooldown;
    public float attackTimer;

    public Transform playerBase;

    public int bulletDamage;
    public int health;
    public GameObject deathEffect;
    public bool isAlive = true;
    public GameObject hitEffect;

    public enum state
    {
        idle,
        moving,
        stone
    }

    public void UpdateInput(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnLeftMousePress(InputAction.CallbackContext context)
    {
        attackCount1++;
        if(attackCount1 != 3)
        {
            firstAttack.AttemptAttack();
        }
        if(attackCount1 >= 3)
        {
            attackCount1 = 0;
        }
    }

    public void OnRightMousePress(InputAction.CallbackContext context)
    {
        attackCount2++;
        if (attackCount2 != 3)
        {
            dashDirection = playerInput;
            secondAttack.AttemptAttack();
        }
        if(attackCount2 >= 3)
        {
            attackCount2 = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.idle;
        lastInput = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 PlayerInputs = new Vector2(playerInput.x, playerInput.y);
        float xAxis = 0f;
        float zAxis = 0f;

        attackTimer += Time.deltaTime;
        if(attackTimer >= attackCooldown)
        {
            Fire();
            attackTimer = 0f;
        }

        switch (currentState)
        {
            case (state.idle):
                if (playerInput.magnitude > 0)
                {
                    currentState = state.moving;
                }
                Aim();

                break;

            case (state.moving):
                
                if (playerInput.magnitude == 0)
                {
                    currentState = state.idle;
                }

                xAxis = PlayerInputs.x * speed * Time.deltaTime;
                zAxis = PlayerInputs.y * speed * Time.deltaTime;
                Aim();

                break;

            case (state.stone):
                xAxis = dashDirection.x * speed * Time.deltaTime;
                zAxis = dashDirection.y * speed * Time.deltaTime;

                break;

            default:

                break;
        }


        Vector3 movementVector = new Vector3(xAxis, 0, zAxis);

        playerBase.transform.Translate(movementVector, Space.World);
    }

    public void Aim()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);

        if (Physics.Raycast(ray, out hit, 1000f, mask))
        {
            rotationTarget = hit.point;
        }

        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);
        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void GotHit(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            if(health <= 0)
            {
                PlayerDied();
            }
            else
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                effect.transform.parent = transform.parent;
                Destroy(effect, 2.0f);
            }
        }
    }

    public void PlayerDied()
    {
        isAlive = false;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        effect.transform.parent = transform.parent;
        Destroy(effect, 2.0f);
        gameObject.SetActive(false);
        SceneMan.died = true;
    }
}
