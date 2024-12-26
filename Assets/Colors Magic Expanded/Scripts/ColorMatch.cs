using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatch : MonoBehaviour
{
    public int EvaluateMatch(Color playerColor, Color targetColor)
    {
        Vector3 playerVector = new Vector3(playerColor.r, playerColor.g, playerColor.b);
        Vector3 targetVector = new Vector3(targetColor.r, targetColor.g, targetColor.b);

        int score = Mathf.RoundToInt((1 - Vector3.Distance(playerVector, targetVector)) * 100);
        Debug.Log("Color Match Score: " + score);
        return score;
    }
}
