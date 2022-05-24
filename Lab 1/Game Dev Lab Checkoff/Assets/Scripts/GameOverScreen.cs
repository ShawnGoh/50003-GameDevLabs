using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public TMP_Text gameoverScoreText;


    public void Setup(int score)
    {
        gameObject.SetActive(true);
        gameoverScoreText.text = "SCORE: " + score.ToString() +"\n"+"HI-SCORE: "+PlayerPrefs.GetInt("HISCORE",0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RestartButton()
    {
        SceneManager.LoadScene("Mario Lab Scene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Mario Lab Main Menu Scene");
    }
}
