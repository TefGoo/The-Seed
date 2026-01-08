using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [Header("Configuración")]
    public string tagJugador = "Player";
    public Animator animatorFinal;
    public bool centrarSemilla = true;

    private bool nivelTerminado = false;
    private int animCrecerID = Animator.StringToHash("Crecer");

    // CAMBIO IMPORTANTE: Ahora usamos Collision2D en lugar de Collider2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificamos el tag desde collision.gameObject
        if (collision.gameObject.CompareTag(tagJugador) && !nivelTerminado)
        {
            nivelTerminado = true;
            PlantarSemilla(collision.gameObject);
        }
    }

    void PlantarSemilla(GameObject semilla)
    {
        Debug.Log("¡Golpe de tierra! Iniciando crecimiento...");

        Rigidbody2D rb = semilla.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Frenamos todo
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (centrarSemilla)
        {
            // Ajustamos la posición para que quede bonito sobre el suelo
            semilla.transform.position = new Vector3(0, transform.position.y + 0.5f, semilla.transform.position.z);
            semilla.transform.rotation = Quaternion.identity;
        }

        Animator animSemilla = semilla.GetComponent<Animator>();
        if (animSemilla != null) animSemilla.SetTrigger(animCrecerID);

        if (animatorFinal != null)
        {
            animatorFinal.gameObject.SetActive(true);
            animatorFinal.SetTrigger(animCrecerID);
        }
    }
}