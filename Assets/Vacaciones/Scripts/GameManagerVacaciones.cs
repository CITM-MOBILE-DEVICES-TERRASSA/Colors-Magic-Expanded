using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerVacaciones : MonoBehaviour
{
    #region Singleton
    private static GameManagerVacaciones _instance;
    public static GameManagerVacaciones Instance => _instance;

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
}
