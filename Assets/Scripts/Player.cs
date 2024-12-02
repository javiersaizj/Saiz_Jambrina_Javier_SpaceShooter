using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject disparoPrefab2; //Disparo powerUp
    [SerializeField] private GameObject spawnPoint1;
    [SerializeField] private GameObject spawnPoint2;

    private GameObject disparoPrefabOriginal;
    private Vector2 direccion;
    private PlayerVisual playerVisual;
    private float temporizador = 0.3f;
    private int vida = 150;
    private int vidaMaxima = 150; // Vida máxima del jugador
    private Spawner spawner;
    private UI_MESSAGES uiMessages; // Referencia al script UI_MESSAGES
    private bool disparoAlternativoActivo = false; //Disparo alternativo
    private float tiempoAlternativo = 10f;

    void Start()
    {
        // Obtener la referencia al script Spawner
        spawner = FindObjectOfType<Spawner>();

        // Referencia al componente PlayerVisual
        playerVisual = GetComponentInChildren<PlayerVisual>();

        // Obtener la referencia a UI_MESSAGES
        uiMessages = FindObjectOfType<UI_MESSAGES>();

        // Actualizar la UI con la vida inicial del jugador
        uiMessages.UpdateVida(vida, vidaMaxima); // Pasamos la vida inicial y la máxima

        //Guardar el disparo original:
        disparoPrefabOriginal = disparoPrefab;
    }

    void Update()
    {
        Movimiento();
        DelimitarMovimiento();
        Disparar();
    }

    void Movimiento()
    {
        // Capturamos las entradas del jugador
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        // Movemos al jugador
        transform.Translate(direccion * velocidad * Time.deltaTime);
        // Normalizamos la dirección
        direccion = new Vector2(inputH, inputV).normalized;

        // Informamos a la clase PlayerVisual sobre la dirección
        if (playerVisual != null)
        {
            playerVisual.UpdateVisual(direccion);
        }
    }

    void DelimitarMovimiento()
    {
        float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        float xClamped = Mathf.Clamp(transform.position.x, -8.58f, 8.58f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    void Disparar()
    {
        temporizador += 1 * Time.deltaTime;
        if ((Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space)) && temporizador > ratioDisparo)
        {
            Instantiate(disparoPrefab, spawnPoint1.transform.position, Quaternion.identity);
            Instantiate(disparoPrefab, spawnPoint2.transform.position, Quaternion.identity);
            temporizador = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DisparoEnemigo"))
        {
            vida -= 20;
            uiMessages.UpdateVida(vida, vidaMaxima); // Actualizar la vida en el Slider
        }

        if (elOtro.gameObject.CompareTag("VidaPowerUp"))
        {
            vida = Mathf.Min(vida + 50, vidaMaxima); // Asegurar que la vida no exceda la máxima
            uiMessages.UpdateVida(vida, vidaMaxima); // Actualizar la vida en el Slider
            Destroy(elOtro.gameObject);
        }
        if (elOtro.gameObject.CompareTag("DisparoPowerUp"))
        {
            StartCoroutine(CambiarDisparoPorTiempo());
            Destroy(elOtro.gameObject);
        }

        if (elOtro.gameObject.CompareTag("Enemigo") || elOtro.gameObject.CompareTag("Enemigo2"))
        {
            vida -= 20;
            uiMessages.UpdateVida(vida, vidaMaxima); // Actualizar la vida en el Slider
            Destroy(elOtro.gameObject);
        }

        if (vida <= 0)
        {
            Destroy(gameObject);
            spawner.OnPlayerDeath();
        }
    }
    private IEnumerator CambiarDisparoPorTiempo()
    {
        disparoPrefab = disparoPrefab2; // Cambiar al disparo con PowerUp
        disparoAlternativoActivo = true;
        float tiempoRestante = tiempoAlternativo;

        while (tiempoRestante > 0)
        {
            yield return null; // Esperar un frame
            tiempoRestante -= Time.deltaTime; // Reducir el tiempo restante
        }

        // Restaurar el disparo original
        disparoAlternativoActivo = false;
        disparoPrefab = disparoPrefabOriginal;
    }
}


/* //NO conseguido con collision, puse triggers a los enemigos.

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemigo"))
    {
        vida -= 20;
        Destroy(collision.gameObject);
    }
    if (collision.gameObject.CompareTag("Enemigo2"))
    {
        vida -= 20;
        Destroy(collision.gameObject);
    }
}
*/


