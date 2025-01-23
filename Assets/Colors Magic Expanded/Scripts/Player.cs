using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Image playerColorDisplay;         // Referencia al cuadro que muestra el color actual del jugador
    public Slider healthSlider;              // Referencia al slider de vida
    public Slider energySlider;              // Referencia al slider de energía

    [Header("Settings")]
    public Color defaultColor = Color.white; // Color predeterminado del jugador
    public float maxHealth = 100f;           // Máxima cantidad de vida
    public float maxEnergy = 100f;           // Máxima cantidad de energía
    public float damageAmount = 20f;         // Cantidad de daño recibido por el ataque especial

    private Color currentColor;              // Color actual del jugador
    private float currentHealth;             // Vida actual del jugador
    private float currentEnergy;             // Energía actual del jugador

    private void Start()
    {
        // Inicializar valores
        currentColor = defaultColor;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;

        // Actualizar la interfaz al inicio
        UpdatePlayerUI();
    }

    public void TakeDamage()
    {
        // Reducir la vida del jugador
        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Reiniciar el color del jugador
        ResetColor();

        // Reiniciar los sliders
        ResetSliders();

        Debug.Log("Jugador golpeado por el ataque especial. Vida restante: " + currentHealth);
    }

    public void ResetColor()
    {
        // Reiniciar el color al predeterminado
        currentColor = defaultColor;

        // Actualizar el cuadro de visualización
        if (playerColorDisplay != null)
        {
            playerColorDisplay.color = currentColor;
        }
        else
        {
            Debug.LogError("PlayerColorDisplay no asignado.");
        }
    }

    private void ResetSliders()
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

        if (energySlider != null)
        {
            energySlider.value = currentEnergy / maxEnergy;
        }
        else
        {
            Debug.LogWarning("EnergySlider no asignado.");
        }
    }

    private void UpdatePlayerUI()
    {
        // Actualizar los sliders y el color inicial
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }

        if (energySlider != null)
        {
            energySlider.value = currentEnergy / maxEnergy;
        }

        if (playerColorDisplay != null)
        {
            playerColorDisplay.color = currentColor;
        }
    }
}
