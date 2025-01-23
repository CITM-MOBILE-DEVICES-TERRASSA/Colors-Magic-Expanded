using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public Enemy enemy;

    public void ResetEnemyColor()
    {
        if(enemy != null)
        {
            enemy.ResetColor();
        }
    }
}
