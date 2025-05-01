using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;
    // Reiniciar puntos totales
    public void Start()
    {
        puntosTotales = 0;
    }

    // Funcion donde se suman los puntos obtenidos
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
    }
}
