using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SeedCollision : MonoBehaviour
{
    [Header("UI de Vidas")]
    public GameObject[] iconosVidas; // <-- AQUÍ ARRASTRAREMOS LAS IMÁGENES DEL UI

    [Header("Configuración")]
    public int vidaMaxima = 3;
    private int vidaActual;

    [Header("Físicas")]
    public float fuerzaReboteObstaculo = 10f;
    public float fuerzaRebotePared = 8f;
    public float tiempoInvencible = 1.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool esInvencible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        vidaActual = vidaMaxima; // Empezamos con 3
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Borde"))
        {
            RebotarEnPared(collision);
        }

        if (collision.gameObject.CompareTag("Obstaculo") && !esInvencible)
        {
            RecibirDaño(collision);
        }
    }

    void RebotarEnPared(Collision2D collision)
    {
        Vector2 direccionRebote = collision.contacts[0].normal;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        rb.AddForce(direccionRebote * fuerzaRebotePared, ForceMode2D.Impulse);
    }

    void RecibirDaño(Collision2D collision)
    {
        // 1. Restamos vida
        vidaActual--;

        // 2. ACTUALIZAMOS EL UI (apagamos el sprite correspondiente)
        // Si teníamos 3 y bajamos a 2, apagamos el índice 2 (que es el tercer corazón)
        if (vidaActual >= 0 && vidaActual < iconosVidas.Length)
        {
            iconosVidas[vidaActual].SetActive(false);
        }

        // 3. Empuje físico
        Vector2 posicionJugador = (Vector2)transform.position;
        Vector2 puntoGolpe = collision.contacts[0].point;
        Vector2 direccionGolpe = (posicionJugador - puntoGolpe).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direccionGolpe * fuerzaReboteObstaculo, ForceMode2D.Impulse);

        if (vidaActual <= 0)
        {
            Morir();
        }
        else
        {
            StartCoroutine(RutinaInvencibilidad());
        }
    }

    IEnumerator RutinaInvencibilidad()
    {
        esInvencible = true;

        if (spriteRenderer != null)
        {
            Color colorOriginal = spriteRenderer.color;
            spriteRenderer.color = new Color(1f, 0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(tiempoInvencible);
            spriteRenderer.color = colorOriginal;
        }
        else
        {
            yield return new WaitForSeconds(tiempoInvencible);
        }

        esInvencible = false;
    }

    void Morir()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool RecuperarVida()
    {
        // Si ya tenemos la vida al máximo, no hacemos nada y devolvemos "false"
        if (vidaActual >= vidaMaxima)
        {
            return false;
        }

        // Si nos falta vida:
        // 1. Encendemos el corazón que estaba apagado (El que corresponde a la vida actual)
        // Ejemplo: Si tengo 2 vidas (índice 0 y 1), el siguiente es el índice 2.
        if (vidaActual < iconosVidas.Length)
        {
            iconosVidas[vidaActual].SetActive(true);
        }

        // 2. Sumamos la vida numérica
        vidaActual++;
        Debug.Log("¡Vida recuperada! Total: " + vidaActual);

        return true; // Devolvemos "true" para confirmar que sí se curó
    }
} 