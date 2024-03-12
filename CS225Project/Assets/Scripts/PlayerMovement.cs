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
    public CharacterController controller;

    public float xAxis;
    public float zAxis;
    public Transform Cam;
    private Vector3 rotationTarget;
    private Vector2 mouseLook;
    public LayerMask mask;

    public enum state
    {
        idle,
        moving,
        attacking
    }

    public void UpdateInput(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
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
        switch (currentState)
        {
            case (state.idle):
                if (playerInput.magnitude > 0)
                {
                    currentState = state.moving;
                }

                break;

            case (state.moving):

                if (playerInput.magnitude == 0)
                {
                    currentState = state.idle;
                }

                xAxis = PlayerInputs.x * speed * Time.deltaTime;
                zAxis = PlayerInputs.y * speed * Time.deltaTime;

                break;
            default:

                break;
        }

        Aim();

        Vector3 movementVector = new Vector3(xAxis, 0, zAxis);
       /* if (currentState != state.idle)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementVector), 0.15f);
        } */

        transform.Translate(movementVector, Space.World);

        //Move the player after any modifications from the state
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
}
