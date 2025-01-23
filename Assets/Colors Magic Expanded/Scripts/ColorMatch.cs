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

    public bool CheckMatch()
    {
        // Obtener el color actual del jugador
        Color playerColor = playerRGBSlides.currentColor;

        // Calcular la diferencia entre los colores (distancia euclidiana en RGB)
        float distance = Mathf.Sqrt(
            Mathf.Pow(targetColor.r - playerColor.r, 2) +
            Mathf.Pow(targetColor.g - playerColor.g, 2) +
            Mathf.Pow(targetColor.b - playerColor.b, 2)
        );

        // Comparar la distancia con el umbral
        return distance <= matchThreshold;
    }
}

