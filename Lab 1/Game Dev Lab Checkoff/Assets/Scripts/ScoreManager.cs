using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public TMP_Text scoreText;
    public TMP_Text hiScoreText;

    int score = 0;
    int hiscore = 0;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {   
        hiscore = PlayerPrefs.GetInt("HISCORE",0);
        scoreText.text = "SCORE: "+ score.ToString();
        hiScoreText.text = "HI-SCORE: "+ hiscore.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePoint(int currentscore)
    {
        scoreText.text = "SCORE: "+ currentscore.ToString();
        score = currentscore;
        if(hiscore<currentscore)
        {
            PlayerPrefs.SetInt("HISCORE", currentscore);
            hiscore = currentscore;
            hiScoreText.text = "HI-SCORE: "+ hiscore.ToString();
        }
    }
}
