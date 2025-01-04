using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject attackObjectPrefab; // Prefab del objeto del ataque
    public int numberOfObjects = 3; // Número de objetos que spawnearán
    public float spawnRadius = 5f; // Radio mínimo para spawnear los objetos
    public float speed = 5f; // Velocidad de los objetos
    public float cooldown = 10f; // Cooldown del ataque especial

    [Header("References")]
    public Transform enemy; // Referencia al enemigo
    private bool canUseSpecialAttack = true; // Control del cooldown

    public void TriggerSpecialAttack()
    {
        if (canUseSpecialAttack)
        {
            StartCoroutine(ExecuteSpecialAttack());
        }
        else
        {
            Debug.Log("Ataque especial en cooldown.");
        }
    }

    private IEnumerator ExecuteSpecialAttack()
    {
        canUseSpecialAttack = false;

        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnAttackObject();
        }

        yield return new WaitForSeconds(cooldown);
        canUseSpecialAttack = true;
    }

    private void SpawnAttackObject()
    {
        // Generar posición aleatoria alrededor del enemigo
        Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = enemy.position + new Vector3(randomPosition.x, randomPosition.y, 0);

        // Instanciar el objeto del ataque
        GameObject attackObject = Instantiate(attackObjectPrefab, spawnPosition, Quaternion.identity);

        // Asignar el comportamiento para moverse hacia el enemigo
        attackObject.GetComponent<AttackObject>().Initialize(enemy, speed);
    }
}

