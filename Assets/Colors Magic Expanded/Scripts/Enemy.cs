using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public ColorMatch colorMatch;           // Referencia al script ColorMatch para obtener el color objetivo
    public Image enemyColorDisplay;         // Cuadro para mostrar el color actual de la IA
    public EnemySpecialAttack specialAttack;     // Referencia al script de Special Attack
    public Slider healthSlider;              // Referencia al slider de vida
    public Animator animator;

    [Header("Settings")]
    public float adjustmentSpeed = 0.002f;  // Velocidad de ajuste por movimiento
    public float moveInterval = 0.1f;       // Tiempo entre movimientos en segundos
    public float specialAttackInterval = 5f; // Tiempo entre intentos de ataque especial
    public float maxHealth = 100f;           // Máxima cantidad de vida
    public float damageAmount = 20f;         // Cantidad de daño recibido por el ataque especial

    [Header("Color Data")]
    private Color currentColor;             // Color actual de la IA
    private Color targetColor;              // Referencia al color objetivo
    private Color defaultColor = Color.black;
    private float currentHealth;            // Vida actual del enemigo

    private bool isReady = false;
    private bool isDead = false;

    private void Start()
    {
        // Inicializar el color de la IA
        currentColor = defaultColor;
        currentHealth = maxHealth;

        UpdateEnemyUI();

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

    private void Update()
    {
        CheckForTargetColorChange(); // Verificar si el color objetivo ha cambiado
    }

    private void CheckForTargetColorChange()
    {
        if (colorMatch != null && colorMatch.TargetColor != Color.black)
        {
            // Si el color objetivo cambia o el enemigo no está listo, actualizar el objetivo
            if (!isReady || targetColor != colorMatch.TargetColor)
            {
                targetColor = colorMatch.TargetColor;
                isReady = true;

                Debug.Log("Nuevo color objetivo asignado: " + targetColor);

                // Reiniciar el movimiento hacia el nuevo objetivo
                CancelInvoke(nameof(MoveTowardsTarget));
                InvokeRepeating(nameof(MoveTowardsTarget), moveInterval, moveInterval);
            }
        }
    }

    public void TakeDamage(int damage = 0)
    {
        // Reducir la vida del jugador
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        animator.SetTrigger("isHurt");

        if (currentHealth <= 0)
        {
            Die();
        }

        ResetColor();
        ResetHealth();

        Debug.Log("Jugador golpeado por el ataque especial. Vida restante: " + currentHealth);
    }

    private void Die()
    {
        isDead = true;

        // Activar la animación de muerte
        animator.SetBool("isDead", true);
        Debug.Log("Enemigo ha muerto.");
    }

    public void ResetColor()
    {
        // Resetear el color de la IA
        currentColor = defaultColor;

        Debug.Log("El color del enemigo ha sido reseteado.");
    }

    private void ResetHealth()
    {
        // Reiniciar los sliders al máximo
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("HealthSlider no asignado.");
        }
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

    private void UpdateEnemyUI()
    {
        // Actualizar los sliders y el color inicial
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}

