using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    public int PuntosTotales { get; private set; }

    private void Awake()
    {
        Debug.Log("GameManager creado y marcado como persistente.");
        if (Instance != null && Instance != this)
        {
            Debug.Log("GameManager duplicado, se destruye.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("GameManager creado.");
    }

    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
    }

    public void ReiniciarPuntos()
    {
        PuntosTotales = 0;
    }
}
