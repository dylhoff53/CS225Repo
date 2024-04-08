using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public SceneMan sceneMan;
    private void Start()
    {
        try
        {
            sceneMan = FindAnyObjectByType<SceneMan>();
        }
        catch (Exception sM)
        {
            Debug.LogException(sM, this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            sceneMan.GoodEnd();
        }
    }
}
