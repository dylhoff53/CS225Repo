using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartSceneManager : MonoBehaviour
{
    public bool clicked;
    public float counter;
    public float multi;
    public GameObject fadeInObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            counter -= multi * Time.deltaTime;
            fadeInObject.GetComponent<CanvasGroup>().alpha = counter;
            if (counter <= 0f)
            {
                Invoke("Change", 1.5f);
            }
        }
    }

    public void GotClicked()
    {
        clicked = true;
        fadeInObject.SetActive(true);
    }

    public void Change()
    {
        SceneManager.LoadScene(1);
    }
}
