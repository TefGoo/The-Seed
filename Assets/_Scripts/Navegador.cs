using UnityEngine;
using UnityEngine.SceneManagement;

public class Navegador : MonoBehaviour
{
    // Función para cambiar de escena (Escribes el nombre en el botón)
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    // Función extra: Para abrir links web (útil para "Mi Itch.io")
    public void AbrirLink(string url)
    {
        Application.OpenURL(url);
    }
}