using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionDisparo : MonoBehaviour
{
    [SerializeField] private Sprite nuevoSpriteDisparoPlayer; // Sprite que se mostrará al impactar el disparo del jugador
    [SerializeField] private Sprite nuevoSpriteDisparoEnemigo; // Sprite que se mostrará al impactar el disparo del enemigo

    [SerializeField] private float velocidad = 5f; // Velocidad del proyectil
    [SerializeField] private float factorEscalado = 1.5f; // Factor de aumento de tamaño
    [SerializeField] private Vector3 direccion;

    private SpriteRenderer spriteRenderer;
    private bool colisionado = false; // Indica si ya hubo una colisión

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Mover el proyectil mientras no haya colisionado
        if (!colisionado)
        {
            transform.Translate(direccion * velocidad * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colisionado) return; // Evitar múltiples colisiones

        if (collision.CompareTag("DisparoPlayer") && CompareTag("Enemigo"))
        {
            Destroy(collision.gameObject); // Destruir el disparo del jugador
        }
        if (collision.CompareTag("DisparoPlayer") && CompareTag("Enemigo2"))
        {
            Destroy(collision.gameObject); // Destruir el disparo del jugador
        }
        else if (collision.CompareTag("DisparoEnemigo") && CompareTag("Player"))
        {
            Destroy(collision.gameObject); // Destruir el disparo del enemigo
        }
        else if (CompareTag("DisparoPlayer") && collision.CompareTag("Enemigo"))
        {
            CambiarSpriteYDestruir(nuevoSpriteDisparoPlayer, gameObject);
        }
        else if (CompareTag("DisparoPlayer") && collision.CompareTag("Enemigo2"))
        {
            CambiarSpriteYDestruir(nuevoSpriteDisparoPlayer, gameObject);
        }
        else if (CompareTag("DisparoEnemigo") && collision.CompareTag("Player"))
        {
            CambiarSpriteYDestruir(nuevoSpriteDisparoEnemigo, gameObject);
        }

    }

    private void CambiarSpriteYDestruir(Sprite nuevoSprite, GameObject objeto)
    {
        colisionado = true; // Marcar como colisionado para detener el movimiento

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = nuevoSprite; // Cambiar el sprite
        }

        // Aumentar el tamaño del objeto
        objeto.transform.localScale *= factorEscalado;

        // Destruir el objeto después de 0.5 segundos
        Destroy(objeto, 0.2f);
    }
}
