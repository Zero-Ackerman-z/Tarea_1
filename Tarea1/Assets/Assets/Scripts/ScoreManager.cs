using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Variable para almacenar el puntaje
    public TMP_Text scoreText; // Referencia al objeto TextMeshPro para mostrar el puntaje

    // Método para actualizar el puntaje y mostrarlo en la UI
    public void UpdateScore(int points)
    {
        score += points; // Sumar los puntos al puntaje actual
        scoreText.text = "Puntaje: " + score.ToString(); // Actualizar el TextMeshPro en la UI
    }
}
