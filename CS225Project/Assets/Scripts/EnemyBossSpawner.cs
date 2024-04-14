using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSpawner : MonoBehaviour
{
    public GameObject boss;
    public bool bossSpawned;
    public bool started;

    public GameObject alertText;

    public void StartSpawningBoss()
    {
        if (!started)
        {
            started = true;
            alertText.SetActive(true);
        }
        if (!bossSpawned)
        {
            bossSpawned = true;
            Invoke("SpawnBoss", 3.0f);
        }
    }
    public void SpawnBoss()
    {
        alertText.SetActive(false);
        boss.SetActive(true);
    }
}
