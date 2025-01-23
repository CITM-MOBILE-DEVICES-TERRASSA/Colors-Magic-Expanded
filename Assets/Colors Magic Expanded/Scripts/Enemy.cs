using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public ColorMatch colorMatch;           // Referencia al script ColorMatch para obtener el color objetivo
    public Image enemyColorDisplay;         // Cuadro para mostrar el color actual de la IA
    public SpecialAttack specialAttack;     // Referencia al script de Special Attack

    [Header("Settings")]
    public float adjustmentSpeed = 0.002f;  // Velocidad de ajuste por movimiento
    public float moveInterval = 0.1f;       // Tiempo entre movimientos en segundos
    public float specialAttackInterval = 5f; // Tiempo entre intentos de ataque especial

    [Header("Color Data")]
    private Color currentColor;             // Color actual de la IA
    private Color targetColor;              // Referencia al color objetivo
    private Color defaultColor = Color.black;

    private bool isReady = false;

    private void Start()
    {
        // Inicializar el color de la IA
        currentColor = defaultColor;

        // Comenzar el ciclo de movimientos de la IA
        InvokeRepeating(nameof(WaitForColorGeneration), 0.1f, 0.1f);

        // Comenzar el ciclo para el ataque especial
        if (specialAttack != null)
        {
            InvokeRepeating(nameof(TrySpecialAttack), specialAttackInterval, specialAttackInterval);
        }
        else
        {
            Debug.LogWarning("SpecialAttack no asignado al enemigo.");
        }
    }

    public void ResetColor()
    {
        // Resetear el color de la IA
        currentColor = defaultColor;

        Debug.Log("El color del enemigo ha sido reseteado.");
    }

    // Método que se ejecuta en cada cuadro para verificar si el color objetivo ya fue generado
    private void WaitForColorGeneration()
    {
        if (colorMatch != null && colorMatch.TargetColor != Color.black && !isReady)
        {
            // Si el color objetivo no es negro (asegurándonos de que se generó) y no hemos recibido la notificación aún
            targetColor = colorMatch.TargetColor;
            isReady = true;                                                             // Ya está listo
            Debug.Log("Color objetivo asignado: " + targetColor);
            CancelInvoke(nameof(WaitForColorGeneration));                               // Detener la espera
            InvokeRepeating(nameof(MoveTowardsTarget), moveInterval, moveInterval);     // Ahora mover hacia el objetivo
        }
    }

    private void MoveTowardsTarget()
    {
        // Ajustar el color actual de la IA para acercarse al objetivo
        currentColor.r = Mathf.MoveTowards(currentColor.r, targetColor.r, adjustmentSpeed);
        currentColor.g = Mathf.MoveTowards(currentColor.g, targetColor.g, adjustmentSpeed);
        currentColor.b = Mathf.MoveTowards(currentColor.b, targetColor.b, adjustmentSpeed);

        // Actualizar el cuadro de visualización
        UpdateEnemyColorDisplay();
    }

    private void UpdateEnemyColorDisplay()
    {
        if (enemyColorDisplay != null)
        {
            enemyColorDisplay.color = currentColor;
        }
        else
        {
            Debug.LogError("EnemyColorDisplay no asignado.");
        }
    }

    private void TrySpecialAttack()
    {
        if (specialAttack != null)
        {
            specialAttack.TriggerSpecialAttack();
        }
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }
}

