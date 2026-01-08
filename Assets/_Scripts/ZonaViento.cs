using UnityEngine;

public class ZonaViento : MonoBehaviour
{
    [Header("Configuración del Viento")]
    public Vector2 direccionFuerza = new Vector2(5f, 0f); // X=5 empuja a la derecha
    public float variacion = 1f; // Para que no sea un empuje robótico perfecto


    private void OnDrawGizmos()
    {
        // Dibuja una caja verde transparente donde está el viento
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position, transform.localScale);

        // Dibuja una línea indicando la dirección
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direccionFuerza * 0.5f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Solo afectamos al jugador
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Calculamos una pequeña turbulencia para que se sienta orgánico
                float turbulencia = Random.Range(-variacion, variacion);

                // Aplicamos la fuerza (ForceMode2D.Force es para empuje continuo como viento)
                Vector2 fuerzaFinal = new Vector2(direccionFuerza.x + turbulencia, direccionFuerza.y);
                rb.AddForce(fuerzaFinal, ForceMode2D.Force);
            }
        }
    }
}