using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_MESSAGES : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Texto que muestra el puntaje
    [SerializeField] private Slider vidaSlider; // Texto que muestra la vida del jugador

    private int score = 0; // Puntaje del jugador

    // Método para actualizar el puntaje en la UI
    public void UpdateScore(int puntos)
    {
        score += puntos;
        scoreText.text = "Score: " + score;
    }
    /*
    public void RestartScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;

    }
    */

    // Método para actualizar la vida del jugador en la UI
    public void UpdateVida(int vidaJugador, int vidaMaxima)
    {
        vidaSlider.maxValue = vidaMaxima;
        vidaSlider.value = vidaJugador;          
        //vidaSlider.maxValue = vidaMaxima;
    }
}
