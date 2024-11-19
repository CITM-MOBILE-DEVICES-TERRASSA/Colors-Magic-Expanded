using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : MonoBehaviour
{
    [SerializeField] private float minChance = 0.4f;
    [SerializeField] private float maxChance = 0.6f;
    private float chance;

    public void CalculateChance()
    {
        chance = Random.Range(minChance, maxChance);
    }

    public float GetPrecision() => chance * 100;
    public float GetChance() => chance;
}