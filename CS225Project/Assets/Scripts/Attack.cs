using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PlayerMovement pm;
    private void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
    }

    public virtual void StartAttack()
    {
        FinishAttacking();
    }

    public void FinishAttacking()
    {
        pm.isFirstAtttacking = false;
        pm.isSecondAtttacking = false;
        pm.isAttacking = false;
        pm.currentState = PlayerMovement.state.idle;
    }
}
