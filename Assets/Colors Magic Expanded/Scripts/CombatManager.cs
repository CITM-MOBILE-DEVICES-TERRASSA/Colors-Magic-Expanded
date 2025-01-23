using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("References")]
    public ColorMatch colorMatch;       // Referencia al script ColorMatch para obtener el color objetivo
    public Player player;               // Referencia al jugador
    public Enemy enemy;                 // Referencia al enemigo
    public Animator playerAnimator;
    public Animator enemyAnimator;

    [Header("Game State")]
    public bool gameOver = false;       // Indica si el juego ha terminado
    public string winner = "";          // Nombre del ganador

    [Header("Timer")]
    public float roundTime = 25f;       // Tiempo para ajustar los colores antes de comparar
    private float timer;                // Temporizador para contar el tiempo restante
    public TextMeshProUGUI timerText;   // Referencia al texto del temporizador

    public float baseDamage = 25f;         // Daño infligido al perder una ronda

    private void Start()
    {
        // Iniciar el temporizador al comienzo de cada ronda
        timer = roundTime;
        colorMatch.GenerateTargetColor(); // Generar un nuevo color objetivo

        UpdateTimerText();
    }

    private void Update()
    {
        if (gameOver) return;           // Si el juego ya terminó, no hacemos nada más

        timer -= Time.deltaTime;        // Reducir el temporizador
        UpdateTimerText();

        if (timer <= 0)
        {
            EndRound();                 // Finalizar la ronda si el tiempo se agota
        }
    }

    public void Attack()
    {
        if (gameOver) return;           // No permitir atacar si el juego terminó
        EndRound();                     // Finalizar la ronda antes de tiempo
    }

    private void EndRound()
    {
        // Comparar los colores y aplicar daño
        CompareColors();

        // Verificar si alguien se quedó sin vida
        if (player.GetHealth() <= 0 || enemy.GetHealth() <= 0)
        {
            gameOver = true;
            winner = player.GetHealth() <= 0 ? "Enemigo" : "Jugador";
            Debug.Log($"¡El ganador es: {winner}!");
        }
        else
        {
            // Reiniciar la ronda si nadie ha perdido
            timer = roundTime;
            colorMatch.GenerateTargetColor();
            UpdateTimerText();
        }
    }

    private void CompareColors()
    {
        Color playerColor = player.GetCurrentColor();
        Color enemyColor = enemy.GetCurrentColor();

        // Calcular precisión y daño para el jugador
        float playerPrecision = colorMatch.CalculatePrecision(playerColor);
        float playerDamage = colorMatch.CalculateDamage(playerPrecision);

        // Calcular precisión y daño para el enemigo
        float enemyPrecision = colorMatch.CalculatePrecision(enemyColor);
        float enemyDamage = colorMatch.CalculateDamage(enemyPrecision);

        // Aplicar daño
        if (playerPrecision >= enemyPrecision)
        {
            playerAnimator.SetTrigger("isAttacking");
            enemy.TakeDamage(Mathf.RoundToInt(playerDamage));
            Debug.Log($"Jugador inflige {playerDamage} de daño (Precisión: {playerPrecision}%). Vida del enemigo: {enemy.GetHealth()}");
        }
        else
        {
            enemyAnimator.SetTrigger("isAttacking");
            player.TakeDamage(Mathf.RoundToInt(enemyDamage));
            Debug.Log($"Enemigo inflige {enemyDamage} de daño (Precisión: {enemyPrecision}%). Vida del jugador: {player.GetHealth()}");
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timer); // Redondear hacia arriba para evitar mostrar 0 demasiado pronto
            timerText.text = $"{seconds}";       // Mostrar solo los segundos
        }
    }
}

