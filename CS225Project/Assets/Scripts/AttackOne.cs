using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOne : Attack
{
    public override void StartAttack()
    {
        Debug.Log("AttackOne!");
        base.StartAttack();
    }
}
