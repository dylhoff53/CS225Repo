using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSpawner : MonoBehaviour
{
    public GameObject boss;
    public bool bossSpawned;

    public void StartSpawningBoss()
    {
        if (!bossSpawned)
        {
            bossSpawned = true;
            Invoke("SpawnBoss", 3.0f);
        }
    }
    public void SpawnBoss()
    {
        boss.SetActive(true);
    }
}
