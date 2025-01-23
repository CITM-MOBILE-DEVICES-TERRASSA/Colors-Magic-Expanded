using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private Transform target; // Objetivo del objeto
    private float speed; // Velocidad del movimiento

    public void Initialize(Transform enemy, float moveSpeed)
    {
        target = enemy;
        speed = moveSpeed;
    }

    private void Update()
    {
        // Moverse hacia el enemigo
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si el objeto tiene el script EnemyUI
        EnemyUI enemyUI = other.GetComponent<EnemyUI>();
        if (enemyUI != null)
        {
            Debug.Log("UI del enemigo tocada. Reiniciando color...");
            enemyUI.ResetEnemyColor();
            Destroy(gameObject); // Destruir el objeto del ataque
            return;
        }

        // Comprobar si toca el escudo
        if (other.CompareTag("Shield"))
        {
            Debug.Log("Escudo tocado. Destruyendo objeto del ataque.");
            Destroy(gameObject); // Destruir el objeto si toca el escudo
            return;
        }
    }
}
