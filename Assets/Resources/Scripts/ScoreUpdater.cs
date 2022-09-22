using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        int score = gameManager.GetHighScore();

        TextMeshProUGUI textHighScore = GameObject.Find("TextHighScore").GetComponent<TextMeshProUGUI>();
        textHighScore.text = $"Highscore: {score}";
    }

    private void LateUpdate()
    {
        int actualScore = gameManager.score;

        TextMeshProUGUI textScore = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();

        textScore.text = $"Score: {actualScore}";
    }
}
