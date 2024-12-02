using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Sprite spriteUp; // Sprite para mirar hacia arriba
    [SerializeField] private Sprite spriteDown; // Sprite para mirar hacia abajo
    [SerializeField] private Sprite spriteIdle; // Sprite neutral
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtenemos el SpriteRenderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateVisual(Vector2 direccion)
    {
        // Cambiamos el sprite basado en la dirección

        if (direccion.y > 0)
        {
            spriteRenderer.sprite = spriteUp; // Mirando hacia arriba
        }
        else if (direccion.y < 0)
        {
            spriteRenderer.sprite = spriteDown; // Mirando hacia abajo
        }
        else
        {
            spriteRenderer.sprite = spriteIdle; // Neutral
        }
    }
}

