using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMatch : MonoBehaviour
{
    [Header("UI Elements")]
    public Image targetColorDisplay;    // Cuadro para mostrar el color objetivo
    public RGBSliders playerRGBSlides;  // Referencia al script del jugador
    public float matchThreshold = 0.1f; // Umbral para considerar que los colores coinciden

    [Header("Color Data")]
    private Color targetColor;          // Color objetivo generado

    // Propiedad pública para obtener el color objetivo
    public Color TargetColor => targetColor;

    public void GenerateTargetColor()
    {
        // Generar un color aleatorio
        targetColor = new Color(Random.value, Random.value, Random.value);

        // Mostrar el color objetivo en pantalla
        if (targetColorDisplay != null)
        {
            targetColorDisplay.color = targetColor;
        }

        Debug.Log("Color objetivo generado: " + targetColor);
    }

    public float CalculatePrecision(Color color)
    {
        // Calcular la distancia entre el color del jugador y el color objetivo
        float maxDistance = Mathf.Sqrt(3); // Distancia máxima posible (1,1,1) a (0,0,0)
        float distance = Mathf.Sqrt(
            Mathf.Pow(targetColor.r - color.r, 2) +
            Mathf.Pow(targetColor.g - color.g, 2) +
            Mathf.Pow(targetColor.b - color.b, 2)
        );

        // Convertir la distancia en un porcentaje de precisión
        float precision = Mathf.Clamp(1 - (distance / maxDistance), 0, 1) * 100;
        return precision;
    }

    public float CalculateDamage(float precision)
    {
        float damage = 25;

        if(precision == 100)
        {
            damage += (damage * 0.5f); // 50% del daño base
        }
        else if (precision >= 98)
        {
            damage += (damage * 0.3f); // 30% del daño base
        }
        else if (precision >= 80)
        {
            damage += (damage * 0.15f); // 15% del daño base
        }
        else if (precision >= 60)
        {
            damage -= (damage * 0.1f); // -10% del daño base
        }
        else
        {
            damage = 0; // Sin daño si la precisión es inferior al 60%
        }

        return damage;
    }
}

