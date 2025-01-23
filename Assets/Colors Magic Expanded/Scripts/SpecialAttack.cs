using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject attackObjectPrefab; // Prefab del objeto del ataque
    public int numberOfObjects = 3; // N�mero de objetos que spawnear�n
    public float spawnRadiusMin = 1f;
    public float spawnRadiusMax = 2f; // Radio m�nimo para spawnear los objetos
    public float speed = 5f; // Velocidad de los objetos
    public float cooldown = 10f; // Cooldown del ataque especial

    [Header("References")]
    public Transform enemy; // Referencia al enemigo
    public Canvas uiCanvas; // Referencia al Canvas principal
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
        // Generar un �ngulo aleatorio
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Generar una distancia aleatoria dentro del rango permitido
        float randomDistance = Random.Range(spawnRadiusMin, spawnRadiusMax);

        // Calcular la posici�n de spawn en base al �ngulo y la distancia
        Vector3 spawnOffset = new Vector3(
            Mathf.Cos(randomAngle) * randomDistance,
            Mathf.Sin(randomAngle) * randomDistance,
            0
        );

        // Calcular la posici�n final de spawn
        Vector3 spawnPosition = enemy.position + spawnOffset;

        // Convertir la posici�n al espacio local del Canvas (UI)
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.GetComponent<RectTransform>(),
            Camera.main.WorldToScreenPoint(spawnPosition),
            Camera.main,
            out canvasPosition
        );

        // Instanciar el objeto del ataque como hijo del Canvas
        GameObject attackObject = Instantiate(attackObjectPrefab, uiCanvas.transform);

        // Asignar la posici�n local dentro del Canvas
        RectTransform attackObjectRect = attackObject.GetComponent<RectTransform>();
        attackObjectRect.anchoredPosition = canvasPosition;

        // Asegurarse de que est� en el layer correcto
        attackObject.layer = LayerMask.NameToLayer("UI");

        // Asignar el comportamiento para moverse hacia el enemigo
        attackObject.GetComponent<AttackObject>().Initialize(enemy, speed);
    }
}

