using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite sprite1; // Primer sprite
    [SerializeField] private Sprite sprite2; // Segundo sprite
    [SerializeField] private Sprite sprite3; // Tercer sprite

    [SerializeField] private float switchInterval; // Intervalo de cambio en segundos

    private SpriteRenderer spriteRenderer;
    private float timer = 0f;

    private int currentSpriteIndex = 0; // Para llevar el índice del sprite actual

    private Sprite[] sprites; // Array para almacenar los tres sprites

    void Start()
    {
        // Obtener el SpriteRenderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializar el array de sprites
        sprites = new Sprite[] { sprite1, sprite2, sprite3 };

        // Iniciar el primer sprite
        spriteRenderer.sprite = sprite1;
    }

    void Update()
    {
        // Incrementar el temporizador
        timer += Time.deltaTime;

        // Verificar si ha pasado el tiempo para cambiar de sprite
        if (timer >= switchInterval)
        {
            // Reiniciar el temporizador
            timer = 0f;

            // Cambiar al siguiente sprite cíclicamente
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;

            // Cambiar el sprite según el índice
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }
}

