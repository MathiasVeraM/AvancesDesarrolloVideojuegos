using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaFinal : MonoBehaviour
{
    public TextMeshProUGUI textoMonedas;
    public GameManagerScript gameManager;

    void Start()
    {
        Debug.Log("¿GameManager está vivo?: " + (GameManagerScript.Instance != null));
        Debug.Log("Total de monedas: " + gameManager.PuntosTotales);
        // Usa singleton si lo tienes
        if (GameManagerScript.Instance != null)
        {
            textoMonedas.text = "Monedas recogidas: " + gameManager.PuntosTotales.ToString();

        }
        else
        {
            textoMonedas.text = "Monedas recogidas: 0";
        }
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene(0); // Cambia el nombre si tu escena de menú tiene otro nombre
    }
}
