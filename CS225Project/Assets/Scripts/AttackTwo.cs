using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTwo : Attack
{
    public override void StartAttack()
    {
        Debug.Log("AttackTwo!");
        base.StartAttack();
    }
}
