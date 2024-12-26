using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMEGameManager : MonoBehaviour
{
    public GameState gameState;
    public CMELevelSelector levelSelector;

    public void StartGame()
    {
        Debug.Log("Game Started");
        //gameState = new MainMenuState();
    }

    public void LoadLevel(Level level)
    {
        Debug.Log("Loading Level: " + level.name);
        levelSelector.SelectLevel(level);
    }
}
