using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialController : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo de aplicación....");
        Application.Quit();
    }
}
