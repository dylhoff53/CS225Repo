using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    public int myEnemyCount;
    public EnemySpawner nextSpawner;
    public bool isBossBarrier;
    public EnemyBossSpawner eBS;

    public void anEnemyDied()
    {
        myEnemyCount--;
        if(myEnemyCount <= 0)
        {
            Die();
        } 
        
        if(isBossBarrier && myEnemyCount == 1)
        {
            eBS.StartSpawningBoss();
        }
    }

    public void StartNextSpawner()
    {
        nextSpawner.Spawn();
    }
    public void Die()
    {
       // Invoke("StartNextSpawner", 4.0f);
        Destroy(gameObject);
    }
}
