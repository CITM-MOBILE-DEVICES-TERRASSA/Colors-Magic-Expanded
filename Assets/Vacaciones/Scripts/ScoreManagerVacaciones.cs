using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerVacaciones : MonoBehaviour
{
    #region Singleton
    private static ScoreManagerVacaciones _instance;
    public static ScoreManagerVacaciones Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    #endregion


    private int score;
    private int totalScore;

    public int score1;
    public int score2;
    public int MaxScore1;
    public int MaxScore2;
    public int MaxTotalLevels;
    public int MaxTotalGame;
 
    
    

}
