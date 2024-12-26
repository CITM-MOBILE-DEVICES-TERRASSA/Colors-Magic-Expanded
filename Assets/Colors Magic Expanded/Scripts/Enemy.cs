using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public void PerformAction(string action)
    {
        Debug.Log("Enemy performs action: " + action);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy takes damage: " + amount);
    }
}
