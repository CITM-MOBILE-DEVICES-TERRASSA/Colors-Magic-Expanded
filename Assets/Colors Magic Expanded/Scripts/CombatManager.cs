using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("References")]
    public ColorMatch colorMatch;       // Referencia al script ColorMatch para obtener el color objetivo
    public RGBSliders playerRGBSliders; // Referencia al jugador
    public Enemy enemy;                 // Referencia al enemigo

    [Header("Game State")]
    public bool gameOver = false;       // Indica si el juego ha terminado
    public string winner = "";          // Nombre del ganador

    [Header("Timer")]
    public float roundTime = 5f;        // Tiempo para ajustar los colores antes de comparar
    private float timer;                // Temporizador para contar el tiempo restante

    private void Start()
    {
        // Iniciar el temporizador al comienzo de cada ronda
        timer = roundTime;
    }

    private void Update()
    {
        if (gameOver) return;           // Si el juego ya terminó, no hacemos nada más

        // Reducir el temporizador en cada cuadro
        timer -= Time.deltaTime;
        //Debug.Log("Tiempo restante: " + Mathf.Ceil(timer));

        if (timer <= 0)
        {
            // Si el temporizador llega a cero, comparar los colores
            CompareColors();
        }
    }

    private void CompareColors()
    {
        // Obtener los colores actuales del jugador y del enemigo
        Color playerColor = playerRGBSliders.GetCurrentColor();
        Color enemyColor = enemy.GetCurrentColor();
        Color targetColor = colorMatch.TargetColor;

        // Calcular la distancia entre los colores del jugador y del enemigo con respecto al objetivo
        float playerDistance = CalculateColorDistance(playerColor, targetColor);
        float enemyDistance = CalculateColorDistance(enemyColor, targetColor);

        // Determinar quién está más cerca del objetivo
        if (playerDistance < enemyDistance)
        {
            winner = "Jugador";
            gameOver = true;
            Debug.Log("¡El jugador ha ganado!");
        }
        else if (enemyDistance < playerDistance)
        {
            winner = "Enemigo";
            gameOver = true;
            Debug.Log("¡El enemigo ha ganado!");
        }
        else
        {
            // Si están igual de cerca, podemos considerar que el juego sigue en marcha
            Debug.Log("El juego sigue...");
        }
    }

    // Método para calcular la distancia entre dos colores usando la distancia euclidiana en el espacio RGB
    private float CalculateColorDistance(Color color1, Color color2)
    {
        return Mathf.Sqrt(Mathf.Pow(color1.r - color2.r, 2) +
                         Mathf.Pow(color1.g - color2.g, 2) +
                         Mathf.Pow(color1.b - color2.b, 2));
    }
}

