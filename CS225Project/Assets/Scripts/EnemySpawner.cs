using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject rangedEnemyPrefab;
    public EnemySpawnClass[] enemySpawns;

    public void Spawn()
    {
        foreach(EnemySpawnClass esc in enemySpawns)
        {
            Instantiate(esc.enemyType, spawnPoints[esc.spawnIndex].position, spawnPoints[esc.spawnIndex].rotation);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
