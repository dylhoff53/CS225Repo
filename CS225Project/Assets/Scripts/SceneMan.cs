using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    private bool gameEnded = false;
    public static bool died;
    public static bool win;
    public CanvasGroup blue;
    public CanvasGroup red;
    private float alphaCounter = 0;
    public float endingTime;

    // Update is called once per frame
    void Update()
    {
        if (gameEnded && win)
        {
            blue.alpha += Time.deltaTime * (1f / endingTime);
        }
        else if (gameEnded && died)
        {
            red.alpha += Time.deltaTime * (1f / endingTime);
        }

        if (died && !gameEnded)
        {
            BadEnd();
        }
        else if (win && !gameEnded)
        {
            GoodEnd();
        }
    }


    public void BadEnd()
    {
        gameEnded = true;
        red.gameObject.SetActive(true);
        Debug.Log("Game Over!");
        Invoke("SwitchScene", endingTime);
    }

    public void GoodEnd()
    {
        gameEnded = true;
        blue.gameObject.SetActive(true);
        Debug.Log("You Win!");
        Invoke("SwitchScene", endingTime);
    }

    public void SwitchScene()
    {
        //SceneManager.LoadScene(0);
    }
}
