using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOne : Attack
{
    public float orginialCD;
    public float quickenedAttackSpeedScale;
    public override void StartAttack()
    {
        Debug.Log("AttackOne!");
        orginialCD = pm.attackCooldown;
        pm.attackCooldown = orginialCD * quickenedAttackSpeedScale;
        base.StartAttack();
    }

    public void Update()
    {
        if (abilityInUse)
        {
            abilityTimer += Time.deltaTime;
            if(abilityTimer >= abilityDuration)
            {
                FinishAttacking();
                pm.attackCooldown = orginialCD;
                pm.isFirstAtttacking = false;
                pm.currentState = PlayerMovement.state.stone;
            }
        }
    }
}
