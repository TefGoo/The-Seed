using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [Header("Configuración")]
    public string tagJugador = "Player";
    public Animator animatorFinal;
    public bool centrarSemilla = true;

    private bool nivelTerminado = false;

    // OPTIMIZACIÓN: Convertimos el nombre "Crecer" a ID numérico una sola vez.
    // Esto es mucho más rápido para el procesador que leer texto cada vez.
    private int animCrecerID = Animator.StringToHash("Crecer");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagJugador) && !nivelTerminado)
        {
            nivelTerminado = true;
            PlantarSemilla(other.gameObject);
        }
    }

    void PlantarSemilla(GameObject semilla)
    {
        Debug.Log("¡Tierra alcanzada! Iniciando crecimiento...");

        Rigidbody2D rb = semilla.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 1. Frenado total (Unity 6 usa linearVelocity)
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // 2. CORRECCIÓN DEL WARNING
            // En lugar de isKinematic = true, cambiamos el tipo de cuerpo:
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // 3. Alinear
        if (centrarSemilla)
        {
            // Mantenemos la Z original para no perderla de vista en la cámara
            semilla.transform.position = new Vector3(0, transform.position.y, semilla.transform.position.z);
            semilla.transform.rotation = Quaternion.identity;
        }

        // 4. Activar Animación (Usando el ID optimizado)
        Animator animSemilla = semilla.GetComponent<Animator>();

        // Si la animación está en la semilla misma
        if (animSemilla != null)
        {
            animSemilla.SetTrigger(animCrecerID);
        }

        // Si la animación está en un objeto externo (la flor final)
        if (animatorFinal != null)
        {
            animatorFinal.gameObject.SetActive(true);
            animatorFinal.SetTrigger(animCrecerID);
        }
    }
}