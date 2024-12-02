using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab; // Prefab del enemigo
    [SerializeField] private GameObject enemigoPrefab2; // Prefab del enemigo alternativo
    [SerializeField] private TextMeshProUGUI textoNiveles; // Texto que muestra el nivel y oleada
    [SerializeField] private GameObject imagenOleada; // GameObject que muestra la imagen de START
    [SerializeField] private GameObject imagenGameOver; // GameObject que muestra la imagen de GAME OVER
    [SerializeField] private Button retryButton; //boton de retry
    [SerializeField] private GameObject playerPrefab; // Prefab del jugador
    [SerializeField] private Transform playerSpawnPoint; // Punto de aparición del jugador

    private bool jugadorMuerto = false; // Controla si el jugador está muerto
    private UI_MESSAGES uiMessages;

    void Start()
    {
        // Poner en invisible GAME OVER
        if (imagenGameOver != null)
        {
            SetGameObjectVisibility(imagenGameOver, false); 
        }

        // Poner en invisible RETRY
        if (retryButton != null)
        {
            SetButtonVisibility(retryButton, false);
        }

        // Poner en invisible START
        if (imagenOleada != null)
        {
            SetGameObjectVisibility(imagenOleada, false);
        }

        // Iniciar la rutina de spawn solo si el jugador no está muerto
        if (!jugadorMuerto)
        {
            StartCoroutine(SpawnearEnemigos());
        }
    }

    IEnumerator SpawnearEnemigos()
    {
        for (int i = 1; i < 6; i++) // Niveles
        {
            for (int j = 1; j < 4; j++) // Oleadas
            {
                if (jugadorMuerto) yield break; // Si el jugador está muerto, salir del bucle

                // Mostrar imagen y texto
                if (imagenOleada != null)
                {
                    SetGameObjectVisibility(imagenOleada, true); // Hacer visible el objeto START
                }
                textoNiveles.text = "Nivel " + i + " - Oleada " + j;

                // Esperar unos segundos para que el jugador vea la información
                yield return new WaitForSeconds(2f);

                // Ocultar la imagen de oleada y limpiar el texto
                if (imagenOleada != null)
                {
                    SetGameObjectVisibility(imagenOleada, false); // Hacer invisible el objeto START
                }
                textoNiveles.text = "";

                for (int k = 1; k < 11; k++) // Enemigos
                {
                    if (jugadorMuerto) yield break; // Salir si el jugador está muerto

                    Vector3 puntoAleatorio = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);

                    // Seleccionar aleatoriamente el prefab de los enemigos
                    GameObject prefabSeleccionado = Random.value < 0.5f ? enemigoPrefab : enemigoPrefab2;

                    Instantiate(prefabSeleccionado, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(1f); // Esperar entre cada enemigo
                }

                // Esperar al final de la oleada
                yield return new WaitForSeconds(2f);
            }

            // Esperar al final del nivel
            yield return new WaitForSeconds(3f);
        }

        // Poner invisible START
        if (imagenOleada != null)
        {
            SetGameObjectVisibility(imagenOleada, false);
        }
        textoNiveles.text = "";
    }

    // Método para cambiar la visibilidad del GameObject
    private void SetGameObjectVisibility(GameObject obj, bool isVisible)
    {
        CanvasRenderer canvasRenderer = obj.GetComponent<CanvasRenderer>();

        if (canvasRenderer != null)
        {
            canvasRenderer.SetAlpha(isVisible ? 1f : 0f); 
        }
    }

    private void SetButtonVisibility(Button button, bool isVisible)
    {
        button.gameObject.SetActive(isVisible);
    }

    // Metodo para cuando muera el jugador:
    public void OnPlayerDeath()
    {
        jugadorMuerto = true; // Cambiar el estado de jugador muerto a true

        // Mostrar la imagen de Game Over
        if (imagenGameOver != null)
        {
            SetGameObjectVisibility(imagenGameOver, true);
        }
        SetButtonVisibility(retryButton, true); //boton de retry

        // Detener la rutina de spawn de enemigos
        StopAllCoroutines();
        retryButton.onClick.AddListener(RetryGame);
        

    }
    public void RetryGame()
    {
        jugadorMuerto = false;

        // Ocultar Game Over y el botón de Retry
        SetGameObjectVisibility(imagenGameOver, false);
        SetButtonVisibility(retryButton, false);

        // Reiniciar al jugador
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        } 

        // Reiniciar el spawn de enemigos
        StartCoroutine(SpawnearEnemigos());
        
        
    }
}
