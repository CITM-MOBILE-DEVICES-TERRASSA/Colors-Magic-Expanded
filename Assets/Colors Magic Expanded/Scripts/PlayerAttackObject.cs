using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackObject : MonoBehaviour
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
        // Verificar si es el collider del player
        if (other.CompareTag("EnemyUI"))
        {
            //Debug.Log("Collider del enemigo tocado. Ejecutando lógica.");

            EnemyUI enemyUI = other.GetComponent<EnemyUI>();
            enemyUI.ResetEnemyColor();
            Destroy(gameObject); // Destruir el ataque especial
            return;
        }

        // Comprobar si toca el escudo
        if (other.CompareTag("EnemyShield"))
        {
            Debug.Log("Escudo tocado. Destruyendo objeto del ataque.");

            Destroy(gameObject); // Destruir el objeto si toca el escudo
            return;
        }
    }
}
