using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target; // Arrastra aquí a tu Semilla

    [Header("Configuración")]
    public float smoothSpeed = 5f; // Qué tan rápido sigue al jugador (más bajo = más suave)
    public Vector3 offset;   // La distancia que mantiene (X, Y, Z)

    void LateUpdate() // Usamos LateUpdate para evitar temblores (jitter) con las físicas
    {
        if (target == null) return;

        // 1. Calculamos dónde debería estar la cámara
        // Mantenemos la Z de la cámara (normalmente -10) para no perder el 2D
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10) + offset;

        // NOTA: Puse '0' en X para que la cámara NO se mueva a los lados (solo baje).
        // Si quieres que siga a la semilla a los lados también, cambia el '0' por 'target.position.x'.

        // 2. Movemos la cámara suavemente hacia esa posición
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 3. Aplicamos la posición (La rotación nunca se toca, así que se queda quieta)
        transform.position = smoothedPosition;
    }
}