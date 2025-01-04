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
        // Comprobar si el objeto toca al enemigo
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().ResetColor();
            Destroy(gameObject); // Destruir el objeto del ataque
        }

        // Comprobar si toca el escudo
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject); // Destruir el objeto si toca el escudo
        }
    }
}
