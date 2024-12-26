using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public void StartCombat()
    {
        Debug.Log("Combat Started");
    }

    public void ResolveTurn()
    {
        Debug.Log("Resolving Turn");
    }
}
