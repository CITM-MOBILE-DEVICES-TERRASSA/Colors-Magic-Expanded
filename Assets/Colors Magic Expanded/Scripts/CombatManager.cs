using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("References")]
    public ColorMatch colorMatch;       // Referencia al script ColorMatch para obtener el color objetivo
    public Player player;               // Referencia al jugador
    public Enemy enemy;                 // Referencia al enemigo

    [Header("Game State")]
    public bool gameOver = false;       // Indica si el juego ha terminado
    public string winner = "";          // Nombre del ganador

    [Header("Timer")]
    public float roundTime = 25f;        // Tiempo para ajustar los colores antes de comparar
    private float timer;                // Temporizador para contar el tiempo restante

    public int damage = 10;       // Da�o infligido al perder una ronda

    private void Start()
    {
        // Iniciar el temporizador al comienzo de cada ronda
        timer = roundTime;
        colorMatch.GenerateTargetColor(); // Generar un nuevo color objetivo
    }

    private void Update()
    {
        if (gameOver) return;           // Si el juego ya termin�, no hacemos nada m�s

        timer -= Time.deltaTime;        // Reducir el temporizador
        if (timer <= 0)
        {
            EndRound();                 // Finalizar la ronda si el tiempo se agota
        }
    }

    public void Attack()
    {
        if (gameOver) return;           // No permitir atacar si el juego termin�
        EndRound();                     // Finalizar la ronda antes de tiempo
    }

    private void EndRound()
    {
        // Comparar los colores y aplicar da�o
        CompareColors();

        // Verificar si alguien se qued� sin vida
        if (player.GetHealth() <= 0 || enemy.GetHealth() <= 0)
        {
            gameOver = true;
            winner = player.GetHealth() <= 0 ? "Enemigo" : "Jugador";
            Debug.Log($"�El ganador es: {winner}!");
        }
        else
        {
            // Reiniciar la ronda si nadie ha perdido
            timer = roundTime;
            colorMatch.GenerateTargetColor();
        }
    }

    private void CompareColors()
    {
        // Obtener los colores actuales del jugador y del enemigo
        Color playerColor = player.GetCurrentColor();
        Color enemyColor = enemy.GetCurrentColor();
        Color targetColor = colorMatch.TargetColor;

        // Calcular la distancia entre los colores del jugador y del enemigo con respecto al objetivo
        float playerDistance = CalculateColorDistance(playerColor, targetColor);
        float enemyDistance = CalculateColorDistance(enemyColor, targetColor);

        if (playerDistance < enemyDistance)
        {
            // El jugador inflige da�o al enemigo
            enemy.TakeDamage(damage);
            Debug.Log($"�El jugador ha hecho da�o! Vida del enemigo: {enemy.GetHealth()}");
        }
        else
        {
            // El enemigo inflige da�o al jugador
            player.TakeDamage(damage);
            Debug.Log($"�El enemigo ha hecho da�o! Vida del jugador: {player.GetHealth()}");
        }
    }

    // M�todo para calcular la distancia entre dos colores usando la distancia euclidiana en el espacio RGB
    private float CalculateColorDistance(Color color1, Color color2)
    {
        return Mathf.Sqrt(Mathf.Pow(color1.r - color2.r, 2) +
                         Mathf.Pow(color1.g - color2.g, 2) +
                         Mathf.Pow(color1.b - color2.b, 2));
    }
}

