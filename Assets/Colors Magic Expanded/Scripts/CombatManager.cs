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
    ColorsMagicExpanded ColorsMagic;
    public TextMeshProUGUI playerDamageText; // Texto de da�o del jugador
    public TextMeshProUGUI enemyDamageText;  // Texto de da�o del enemigo

    [Header("Game State")]
    public bool gameOver = false;       // Indica si el juego ha terminado
    public string winner = "";          // Nombre del ganador

    [Header("Timer")]
    public float roundTime = 25f;       // Tiempo para ajustar los colores antes de comparar
    private float timer;                // Temporizador para contar el tiempo restante
    public TextMeshProUGUI timerText;   // Referencia al texto del temporizador

    public float baseDamage = 25f;         // Da�o infligido al perder una ronda

    private void Start()
    {
        ColorsMagic = FindAnyObjectByType<ColorsMagicExpanded>();

        // Iniciar el temporizador al comienzo de cada ronda
        timer = roundTime;
        colorMatch.GenerateTargetColor(); // Generar un nuevo color objetivo

        UpdateTimerText();

        playerDamageText.gameObject.SetActive(false);
        enemyDamageText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameOver) return;           // Si el juego ya termin�, no hacemos nada m�s

        timer -= Time.deltaTime;        // Reducir el temporizador
        UpdateTimerText();

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
            if (player.GetHealth() <= 0)
            {
                winner = "Enemigo";
            }
            else winner = "Player";
            //winner = player.GetHealth() <= 0 ? "Enemigo" : "Jugador";
            Debug.Log($"�El ganador es: {winner}!");

            End();
        }
        else
        {
            // Reiniciar la ronda si nadie ha perdido
            timer = roundTime;
            colorMatch.GenerateTargetColor();
            UpdateTimerText();
        }
    }

    private void End()
    {
        if (winner == "Enemigo")
        {
            ColorsMagic.ChangeToScene("DefeatCME");
        }
        else
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
            if (currentLevel >= levelsUnlocked)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", currentLevel + 1);
                PlayerPrefs.Save();
            }
            ColorsMagic.ChangeToScene("VictoryCME");
        }
    }

    private void CompareColors()
    {
        Color playerColor = player.GetCurrentColor();
        Color enemyColor = enemy.GetCurrentColor();

        // Calcular precisi�n y da�o para el jugador
        float playerPrecision = colorMatch.CalculatePrecision(playerColor);
        float playerDamage = colorMatch.CalculateDamage(playerPrecision);

        // Calcular precisi�n y da�o para el enemigo
        float enemyPrecision = colorMatch.CalculatePrecision(enemyColor);
        float enemyDamage = colorMatch.CalculateDamage(enemyPrecision);

        // Aplicar da�o
        if (playerPrecision >= enemyPrecision)
        {
            playerAnimator.SetTrigger("isAttacking");
            enemy.TakeDamage(Mathf.RoundToInt(playerDamage));
            ShowDamageText(playerDamageText, Mathf.RoundToInt(playerDamage));
            Debug.Log($"Jugador inflige {playerDamage} de da�o (Precisi�n: {playerPrecision}%). Vida del enemigo: {enemy.GetHealth()}");
        }
        else
        {
            enemyAnimator.SetTrigger("isAttacking");
            player.TakeDamage(Mathf.RoundToInt(enemyDamage));
            ShowDamageText(enemyDamageText, Mathf.RoundToInt(enemyDamage));
            Debug.Log($"Enemigo inflige {enemyDamage} de da�o (Precisi�n: {enemyPrecision}%). Vida del jugador: {player.GetHealth()}");
        }
    }

    private void ShowDamageText(TextMeshProUGUI damageText, int damage)
    {
        // Mostrar el texto con el da�o
        damageText.text = $"{damage}";
        damageText.gameObject.SetActive(true);

        // Desactivar el texto despu�s de 1 segundo
        StartCoroutine(HideDamageTextAfterDelay(damageText));
    }

    private IEnumerator HideDamageTextAfterDelay(TextMeshProUGUI damageText)
    {
        yield return new WaitForSeconds(3f);
        damageText.gameObject.SetActive(false);
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

