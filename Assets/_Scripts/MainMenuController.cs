using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Tooltip("Escribe aquí el nombre EXACTO de tu escena de juego")]
    public string nombreEscenaJuego = "GameScene";

    // Esta función la llamará el botón de Jugar
    public void Jugar()
    {
        // Carga la escena por su nombre
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    // Esta función la llamará el botón de Salir
    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); // Mensaje para ver en el Editor
        Application.Quit();
    }
}