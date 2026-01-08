using UnityEngine;

public class Espora : MonoBehaviour
{
    [Header("Movimiento")]
    public float distanciaMovimiento = 2f; // Cuánto se mueve de lado a lado
    public float velocidad = 2f; // Qué tan rápido va
    public bool movimientoHorizontal = true; // ¿Se mueve en X o en Y?

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // MOVIMIENTO (Loop Ping-Pong)
        // Usamos la función Seno (Sin) para un movimiento suave de vaivén
        float offset = Mathf.Sin(Time.time * velocidad) * distanciaMovimiento;

        if (movimientoHorizontal)
        {
            transform.position = new Vector3(posicionInicial.x + offset, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, posicionInicial.y + offset, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detectamos si chocamos con el jugador
        if (other.CompareTag("Player")) // Asegúrate que tu Semilla tiene el Tag "Player"
        {
            // Buscamos el script del jugador
            SeedCollision jugador = other.GetComponent<SeedCollision>();

            if (jugador != null)
            {
                // Intentamos curar. La función nos devuelve TRUE si se curó, FALSE si estaba lleno.
                bool seCuro = jugador.RecuperarVida();

                if (seCuro)
                {
                    // Solo desaparecemos si realmente curamos al jugador
                    // Opcional: Poner un sonido aquí antes de destruir
                    Destroy(gameObject);
                }
            }
        }
    }
}