using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Detectar el movimiento del dedo en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
            transform.position = touchPosition;
        }

        // Alternativa para probar con el mouse en el editor
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            transform.position = mousePosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si el escudo toca un objeto del ataque especial
        if (other.CompareTag("AttackObject"))
        {
            Destroy(other.gameObject); // Destruir el objeto del ataque
        }
    }
}
