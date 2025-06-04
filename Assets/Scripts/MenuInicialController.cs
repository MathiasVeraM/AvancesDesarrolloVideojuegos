using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialController : MonoBehaviour
{

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
