using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public PlayerMovement pm;
    public float abilityDuration;
    public float abilityTimer;
    public bool abilityInUse;
    public float abilityCD;
    public Slider uiSlider;

    private void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
    }

    public void Update()
    {
        if (abilityInUse)
        {
            abilityTimer += Time.deltaTime;
            if (abilityTimer >= abilityDuration)
            {
                FinishAttacking();
                //pm.isSecondAtttacking = false;
            }
        }

        if(abilityInUse == false && uiSlider.value > 0)
        {
            Debug.Log(abilityInUse);
            float percent = Time.deltaTime / abilityCD;
            uiSlider.value -= percent;

        } else if(!abilityInUse && uiSlider.value <= 0)
        {
            uiSlider.value = 0f;
        }
    }

    public virtual void StartAttack()
    {
        abilityInUse = true;
        uiSlider.value = 1f;
    }

    public void AttemptAttack()
    {
        if(!abilityInUse && uiSlider.value == 0)
        {
            StartAttack();
        }
    }

    public virtual void FinishAttacking()
    {
        abilityTimer = 0f;
        uiSlider.value = 1f;
        abilityInUse = false;
    }
}
