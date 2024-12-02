using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float velocidad; // Velocidad del enemigo
    private float limiteX = -10.58f;
    private int vida; // Vida actual del enemigo
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mover al enemigo hacia la izquierda
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        if (transform.position.x < limiteX)
        {
            Destroy(gameObject); // Destruir el enemigo al pasar el límite
        }
    }
}
