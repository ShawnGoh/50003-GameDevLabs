using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public TMP_Text text;

    public void UpdateScore()
    {
        text.text = "Score: " + marioScore.Value.ToString();
    }

    public void Start()
    {
        UpdateScore();
    }
}
