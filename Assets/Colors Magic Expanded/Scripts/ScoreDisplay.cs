using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        // Get the score from PlayerPrefs (default to 0 if not set)
        int currentScore = PlayerPrefs.GetInt("ScoreCME", 0);

        // Update the scoreText with the current score
        scoreText.text = "" + currentScore;
    }
}
