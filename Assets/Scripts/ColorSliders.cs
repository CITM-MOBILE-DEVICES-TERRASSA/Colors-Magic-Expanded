using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class ColorSliders : MonoBehaviour
{
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private Image colorDisplay;
    private Color color;
    
    private void Awake()
    {
        redSlider.minValue = 0;
        redSlider.maxValue = 1;

        greenSlider.minValue = 0;
        greenSlider.maxValue = 1;

        blueSlider.minValue = 0;
        blueSlider.maxValue = 1;

        UpdateColor();
        
        redSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
    }

    private void UpdateColor()
    {
        color = new Color(
            redSlider.value,   
            greenSlider.value, 
            blueSlider.value   
        );

        colorDisplay.color = color;
    }

    public Color GetColor() => color;
}