using UnityEngine;

public class FallingSeed : MonoBehaviour
{
    [Header("Configuración de Flote")]
    public float swaySpeed = 2f;
    public float swayForce = 5f;

    [Header("Control del Jugador")]
    public float moveSpeed = 3f;

    [Header("Efectos Visuales (Rotación)")]
    public float tiltAmount = 10f;    // Cuánto se inclina (Prueba entre 5 y 15)
    public float rotationSmoothness = 5f; // Qué tan rápido reacciona la rotación (High = Rápido)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // --- 1. FISICAS (MOVIMIENTO) ---

        // Péndulo automático (Seno)
        float automaticSway = Mathf.Sin(Time.time * swaySpeed) * swayForce;
        rb.AddForce(Vector2.right * automaticSway);

        // Input del Jugador
        float inputX = Input.GetAxis("Horizontal"); // Requiere modo "Both" o "Input Manager"
        rb.AddForce(Vector2.right * inputX * moveSpeed);


        // --- 2. JUICE (ROTACIÓN PROCEDURAL) ---

        // Calculamos el ángulo objetivo basado en la velocidad horizontal actual.
        // El signo negativo (-) hace que se incline "hacia" donde va, como planeando.
        // Nota: En Unity 6 usamos 'linearVelocity' en lugar de 'velocity'.
        float targetAngle = rb.linearVelocity.x * -tiltAmount;

        // Usamos LerpAngle para suavizar la transición entre el ángulo actual y el objetivo
        float smoothedAngle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * rotationSmoothness);

        // Aplicamos la rotación al Rigidbody (es mejor que usar transform.rotation)
        rb.MoveRotation(smoothedAngle);
    }
}