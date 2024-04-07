using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawner : MonoBehaviour
{
    public float currentDistanceApart;
    public PlayerMovement pm;
    public float range;
    public bool notSpawned;
    public GameObject child;

    // Update is called once per frame
    void Update()
    {
        currentDistanceApart = Vector3.Distance(transform.position, pm.transform.position);
        if(currentDistanceApart <= range && notSpawned)
        {
            notSpawned = false;
            child.SetActive(true);
        }
    }
}
