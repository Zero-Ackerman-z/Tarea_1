using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool juegoTerminado = false;
    private bool jugadorGano = false; // Variable para indicar si el jugador ganó
    private int score = 0; // Variable para almacenar el puntaje
    public ScoreManager scoreManager; // Referencia al UIManager para actualizar la UI

    public GameObject panelResultado;
    public Image imagenVictoria;
    public Image imagenDerrota;

    private void Start()
    {
        panelResultado.SetActive(false); // Desactivar el panel de resultados al iniciar el juego
    }
    public void UpdateScore(int points)
    {
        score += points; // Sumar los puntos al puntaje actual
        scoreManager.scoreText.text = "Puntaje: " + score.ToString(); // Actualizar el TextMeshPro en la UI

        // Verificar si el jugador alcanzó la cantidad necesaria para ganar
        if (score >= 30)
        {
            jugadorGano = true;
            Ganar(); // Llamar al método para mostrar la pantalla de victoria
        }
    }

    public void Ganar()
    {
        juegoTerminado = true;
        panelResultado.SetActive(true); // Activar el panel de resultado

        // Mostrar la imagen de victoria si el jugador ganó, de lo contrario, mostrar la de derrota
        imagenVictoria.gameObject.SetActive(jugadorGano);
        imagenDerrota.gameObject.SetActive(!jugadorGano);

        Time.timeScale = 0f; // Pausar el juego al mostrar el panel de resultados
    }

    public void Perder()
    {
        juegoTerminado = true;
        panelResultado.SetActive(true); // Activar el panel de resultado

        // Mostrar la imagen de derrota
        imagenDerrota.gameObject.SetActive(true);
        imagenVictoria.gameObject.SetActive(false);

        Time.timeScale = 0f; // Pausar el juego al mostrar el panel de resultados
    }


}
