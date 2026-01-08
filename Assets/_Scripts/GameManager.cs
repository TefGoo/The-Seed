using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Pantallas UI")]
    public GameObject pantallaGameOver;
    public GameObject pantallaVictoria;

    [Header("Configuración")]
    public string nombreEscenaMenu = "MenuPrincipal"; // Asegúrate que coincida con tu escena

    // Singleton muy básico para poder llamarlo desde otros scripts fácilmente
    public static GameManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void ActivarGameOver()
    {
        Debug.Log("Game Over activado");
        pantallaGameOver.SetActive(true);

        // OPCIONAL: Detener el tiempo para que la semilla deje de caer
        // Time.timeScale = 0f; 
    }

    public void ActivarVictoria()
    {
        Debug.Log("¡Victoria! Mostrando menú...");
        // Aquí podrías esperar unos segundos si quieres ver la animación de la flor primero
        Invoke("MostrarPantallaVictoria", 2f); // Espera 2 segundos antes de mostrar el menú
    }

    void MostrarPantallaVictoria()
    {
        pantallaVictoria.SetActive(true);
        // Time.timeScale = 0f; // Pausar si quieres
    }

    // --- FUNCIONES PARA LOS BOTONES ---

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // IMPORTANTÍSIMO: Reactivar el tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f; // Reactivar tiempo
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}