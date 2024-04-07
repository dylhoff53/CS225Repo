using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossDetector : MonoBehaviour
{
    public int dectectorIndex;
    public EnemyBoss eB;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
        }
    }
}
