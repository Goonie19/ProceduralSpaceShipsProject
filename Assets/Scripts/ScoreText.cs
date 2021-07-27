using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int storedScore;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();       
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        storedScore = ShopManager.Instance.Stars;

        scoreText.text = "X  " + storedScore.ToString();
    }

    private void OnEnable()
    {
        UpdateScoreText();
    }
}
