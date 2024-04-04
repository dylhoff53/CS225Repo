using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PlayerMovement pm;
    public float abilityDuration;
    public float abilityTimer;
    public bool abilityInUse;
    private void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
    }

    public virtual void StartAttack()
    {
        abilityInUse = true;
    }

    public void FinishAttacking()
    {
        abilityTimer = 0f;
        abilityInUse = false;
    }
}
