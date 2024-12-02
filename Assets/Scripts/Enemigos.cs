using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    [SerializeField] private float velocidad; // Velocidad del enemigo
    [SerializeField] private GameObject disparoPrefab; // Prefab de los disparos
    [SerializeField] private GameObject spawnPoint; // Punto de aparición de los disparos
    [SerializeField] private GameObject vidaPowerUpPrefab; //Prefab del powerUp de vida
    [SerializeField] private GameObject disparoPowerUpPrefab; //Prefab de powerUp de disparo
    [SerializeField] private int vidaInicialEnemigo = 30; // Vida inicial para enemigos con el tag "Enemigo"
    [SerializeField] private int vidaInicialEnemigo2 = 45; // Vida inicial para enemigos con el tag "Enemigo2"
    [SerializeField] private Sprite spriteMuerte; // Sprite Muerte

    private float limiteX = -10.58f;
    private int vida; // Vida actual del enemigo
    private SpriteRenderer spriteRenderer; //Sprite del enemigo
    private UI_MESSAGES uiMessages; // Referencia al script de UI_MESSAGES
    void Start()
    {
        uiMessages = FindObjectOfType<UI_MESSAGES>();
        // Asignar la vida inicial según el tag del enemigo
        if (CompareTag("Enemigo"))
        {
            vida = vidaInicialEnemigo;
        }
        else if (CompareTag("Enemigo2"))
        {
            vida = vidaInicialEnemigo2;
        }

        //Obtiene el sprite del enemigo
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Iniciar la rutina de disparo
        StartCoroutine(SpawnearDisparos());
    }

    void Update()
    {
        // Mover al enemigo hacia la izquierda
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        if (transform.position.x < limiteX)
        {
            Destroy(gameObject); // Destruir el enemigo al pasar el límite
        }
    }


    private IEnumerator SpawnearDisparos()
    {
        while (true)
        {
            // Instanciar disparos desde el punto de spawn
            Instantiate(disparoPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3f); // Espera entre cada disparo
        }
    }
    private IEnumerator CambiarSpriteYDestruir()
    {
        // Cambiar el sprite a "spriteMuerte"
        if (spriteRenderer != null && spriteMuerte != null)
        {
            spriteRenderer.sprite = spriteMuerte;
        }

        // Esperar
        yield return new WaitForSeconds(0.3f);

        // Actualizar puntaje y destruir el enemigo
        if (CompareTag("Enemigo"))
        {
            uiMessages.UpdateScore(100); // Si es "Enemigo", sumar 100 puntos
        }
        else if (CompareTag("Enemigo2"))
        {
            uiMessages.UpdateScore(200); // Si es "Enemigo2", sumar 200 puntos
        }
        // power-up de vida con un 20% de probabilidad
        if (Random.value < 0.15f) // Probabilidad del 15%
        {
            Instantiate(vidaPowerUpPrefab, transform.position, Quaternion.identity);
        }
        if (Random.value < 0.1f) // Probabilidad del 10%
        {
            Instantiate(disparoPowerUpPrefab, transform.position, Quaternion.identity);
        }

        // Destruir el enemigo después del retraso
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        // Reducir vida si recibe un disparo del jugador
        if (elOtro.gameObject.CompareTag("DisparoPlayer"))
        {
            vida -= 20;
            // Destruir el enemigo si su vida llega a 0
            if (vida <= 0)
            {
                StartCoroutine(CambiarSpriteYDestruir());
                //Destroy(gameObject);
            }
        }
        if (elOtro.gameObject.CompareTag("DisparoPlayer2"))
        {
            vida -= 50;
            // Destruir el enemigo si su vida llega a 0
            if (vida <= 0)
            {
                StartCoroutine(CambiarSpriteYDestruir());
                //Destroy(gameObject);
            }
        }
        if (elOtro.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CambiarSpriteYDestruir());
            //Destroy(gameObject);
        }
}
}

