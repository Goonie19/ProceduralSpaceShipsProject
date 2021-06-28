using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public int storedScore = 0;
    public int currentScore = 0;
    public GameObject losePanel;
    public GameObject finalScoreText;
    public GameObject currentScoreText;

    private void Start()
    {
        EraseCurrentScore();
        currentScoreText.GetComponent<TextMeshProUGUI>().text = "X " + currentScore.ToString();
    }

    public void SaveScore()
    {
        storedScore += currentScore;
        PlayerPrefs.SetInt("Stored score", storedScore);
    }

    public void AddScore()
    {
        currentScore++;
        currentScoreText.GetComponent<TextMeshProUGUI>().text = "X " + currentScore.ToString();
    }

    public void EraseCurrentScore()
    {
        currentScore = 0;
        currentScoreText.GetComponent<TextMeshProUGUI>().text = "X " + currentScore.ToString();
    }

    public void GameOver()
    {
        SetFinalScoreText();
        losePanel.SetActive(true);
        SaveScore();
        EraseCurrentScore();
        Time.timeScale = 0f;
    }

    public void SetFinalScoreText()
    {
        finalScoreText.GetComponent<TextMeshProUGUI>().text = "SCORE: " + currentScore.ToString();
    }
}
