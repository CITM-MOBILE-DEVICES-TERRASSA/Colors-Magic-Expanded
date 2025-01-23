using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false; // Indica si el escudo está siendo arrastrado
    private Vector3 targetPosition; // Posición a la que se mueve el escudo
    public float moveSpeed = 0.005f; // Velocidad de movimiento del escudo

    private void Start()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Detectar el movimiento del dedo en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Comprobar si el toque inicial fue sobre el escudo
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                if (hitCollider != null && hitCollider.gameObject == gameObject)
                {
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Mover el escudo mientras se arrastra
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // Finalizar el arrastre cuando se suelta el dedo
                isDragging = false;
            }
        }

        // Alternativa para probar con el mouse en el editor
        if (Input.GetMouseButtonDown(0))
        {
            // Comprobar si el clic inicial fue sobre el escudo
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            // Mover el escudo mientras se arrastra
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Finalizar el arrastre cuando se suelta el botón del ratón
            isDragging = false;
        }

        // Mover el escudo hacia la posición objetivo con un suavizado
        if (isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
