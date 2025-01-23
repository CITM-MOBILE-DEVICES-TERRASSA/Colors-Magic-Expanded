using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Image playerColorDisplay;         // Referencia al cuadro que muestra el color actual del jugador
    public Slider healthSlider;              // Referencia al slider de vida
    public RGBSliders rgbSliders;            // Referencia al script de los sliders RGB
    public Animator animator;

    [Header("Settings")]
    public Color defaultColor = Color.black; // Color predeterminado del jugador
    public float maxHealth = 100f;           // Máxima cantidad de vida
    private float currentHealth;             // Vida actual del jugador
    
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        ResetHealth();
    }

    public void TakeDamage(int damage = 0)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        animator.SetTrigger("isHurt");

        if(currentHealth <= 0)
        {
            Die();
        }

        ResetColor();
        ResetHealth();

        Debug.Log("Jugador golpeado. Vida restante: " + currentHealth);
    }

    private void Die()
    {
        isDead = true;

        // Activar la animación de muerte
        animator.SetBool("isDead", true);
        Debug.Log("Jugador ha muerto.");
    }

    private void ResetColor()
    {
        if (rgbSliders != null)
        {
            rgbSliders.ResetColor();
        }
        else
        {
            Debug.LogError("RGBSliders no asignado.");
        }
    }

    private void ResetHealth()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("HealthSlider no asignado.");
        }
    }

    public Color GetCurrentColor()
    {
        return rgbSliders.GetCurrentColor();
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
