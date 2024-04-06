using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTwo : Attack
{
    public Vector2 dashDirection;
    public float dashSpeed;
    public float originalSpeed;
    public override void StartAttack()
    {
        originalSpeed = pm.speed;
        pm.speed = dashSpeed;
        Debug.Log("AttackTwo!");
        base.StartAttack();
    }


    public override void FinishAttacking()
    {
        pm.speed = originalSpeed;
        pm.currentState = PlayerMovement.state.idle;
        base.FinishAttacking();
    }
}
