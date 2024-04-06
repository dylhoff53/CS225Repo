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


    public override void FinishAttacking()
    {
        pm.attackCooldown = orginialCD;
        base.FinishAttacking();
    }
}
