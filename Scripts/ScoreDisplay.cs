using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public int score;
    TextMeshProUGUI scoreText;

    public int collectableScore = 0;
    public int collectableTotal;

    public int artifactScore = 0;
    public int artifactTotal;

    public int artifactPoints;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString(); 
    }

    public void UpdateCollectablesScore(int points)
    {
        collectableScore += points;
    }

    public void UpdateArtifactScore(int points)
    {
        artifactScore += points;
    }
}
