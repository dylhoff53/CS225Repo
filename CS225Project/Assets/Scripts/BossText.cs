using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class BossText : MonoBehaviour
{
    public TMP_Text text;

    private void Start()
    {
        Debug.Log("Doing The Thing!");
        string readFromFilePath = Application.streamingAssetsPath + "/FileIO/" + "BossText" + ".txt";

        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        foreach(string line in fileLines)
        {
            text.text = line;
        }
    }
}
