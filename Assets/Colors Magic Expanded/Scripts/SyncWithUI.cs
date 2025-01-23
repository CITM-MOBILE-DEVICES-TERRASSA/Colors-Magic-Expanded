using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncWithUI : MonoBehaviour
{
    public RectTransform uiElement; // El elemento de UI
    public Canvas canvas; // El canvas al que pertenece la UI

    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(uiElement.position);
        worldPosition.z = 0; // Asegúrate de que esté en el plano 2D
        transform.position = worldPosition;
    }
}
