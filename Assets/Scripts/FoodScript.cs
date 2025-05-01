using UnityEngine;

public class Comida : MonoBehaviour
{
    public int cantidadCuracion = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController jugador = other.GetComponent<PlayerController>();
            if (jugador != null)
            {
                jugador.Heal(cantidadCuracion);
                Destroy(gameObject); // Destruye la comida al recogerla
            }
        }
    }
}
