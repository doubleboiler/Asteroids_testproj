using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text scoreText, highScoreText;
    private int score, highScore;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = highScore.ToString();
        }

    }

    void Start()
    {
        score = 0;

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;

        UpdateHighScore();
        UpdateScore();
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();

            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

}
