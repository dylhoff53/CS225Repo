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

    public void Update()
    {
        if (abilityInUse)
        {
            abilityTimer += Time.deltaTime;
            if (abilityTimer >= abilityDuration)
            {
                FinishAttacking();
                pm.isSecondAtttacking = false;
                pm.speed = originalSpeed;
            }
        }
    }
}
