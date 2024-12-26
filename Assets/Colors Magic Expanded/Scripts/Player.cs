using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public RGBBars rgbBars;
    public int health;

    public void PerformAction(string action)
    {
        Debug.Log("Player performs action: " + action);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player takes damage: " + amount);
    }
}
