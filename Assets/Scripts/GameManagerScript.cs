using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    public int PuntosTotales { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
