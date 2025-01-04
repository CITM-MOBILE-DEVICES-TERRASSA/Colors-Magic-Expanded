using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBSliders : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider redSlider;   // Slider para el canal rojo
    public Slider greenSlider; // Slider para el canal verde
    public Slider blueSlider;  // Slider para el canal azul
    public Image colorDisplay; // Cuadro que muestra el color actual

    [Header("Color Data")]
    public Color currentColor; // Color actual del jugador

    void Start()
    {
        // Inicializar sliders y listeners
        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);

        // Configurar sliders para valores entre 0 y 1
        redSlider.minValue = 0;
        redSlider.maxValue = 1;
        greenSlider.minValue = 0;
        greenSlider.maxValue = 1;
        blueSlider.minValue = 0;
        blueSlider.maxValue = 1;

        // Inicializar el color al principio
        UpdateColor(0);
    }

    void UpdateColor(float _)
    {
        // Obtener los valores actuales de los sliders
        float r = redSlider.value;
        float g = greenSlider.value;
        float b = blueSlider.value;

        // Actualizar el color actual
        currentColor = new Color(r, g, b);

        // Actualizar el cuadro de visualización
        if (colorDisplay != null)
        {
            colorDisplay.color = currentColor;
        }
    }

    public Color GetCurrentColor()
    {
        return currentColor; // Retorna el color actual del player
    }
}


