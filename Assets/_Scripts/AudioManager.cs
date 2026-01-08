using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Configuración")]
    public Transform jugador;
    public float alturaMaxima = 0f;
    public float alturaMeta = -1250f;

    [Header("Efectos")]
    [Range(0f, 1f)] public float volumenMinimo = 0.2f;
    [Range(22000f, 1000f)] public float lowPassInicial = 1000f;

    private AudioSource audioSource;
    private AudioLowPassFilter lowPassFilter;

    void Start()
    {
        // Forzamos la búsqueda de componentes al inicio
        InicializarComponentes();
    }

    void Update()
    {
        // 1. SEGURIDAD: Si no hay jugador asignado, no hacemos nada y evitamos el error
        if (jugador == null) return;

        // 2. SEGURIDAD: Si por alguna razón el filtro no existe, lo creamos
        if (lowPassFilter == null) InicializarComponentes();

        // Calcular progreso
        float progreso = Mathf.InverseLerp(alturaMaxima, alturaMeta, jugador.position.y);

        // Aplicar efectos (Con chequeo de seguridad extra)
        if (lowPassFilter != null)
        {
            lowPassFilter.cutoffFrequency = Mathf.Lerp(lowPassInicial, 22000f, progreso);
        }

        if (audioSource != null)
        {
            audioSource.volume = Mathf.Lerp(volumenMinimo, 1f, progreso);
        }
    }

    void InicializarComponentes()
    {
        audioSource = GetComponent<AudioSource>();

        lowPassFilter = GetComponent<AudioLowPassFilter>();
        if (lowPassFilter == null)
        {
            // Si no existe, lo añadimos
            lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
        }
    }
}