using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int storedScore;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        storedScore = PlayerPrefs.GetInt("Stored score");

        scoreText.text = "X  " + storedScore.ToString();
    }

    private void OnEnable()
    {
        UpdateScoreText();
    }
}
