using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackDetection : MonoBehaviour
{
    public GameObject destructionEffect;
    public float delay = 3f; // puedes hacer 0 si quieres que sea inmediato
    public bool triggerOnce = true;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOnce && triggered) return;

        if (collision.gameObject.name.Contains("Hit")) // detecta Hit(Clone)
        {
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, transform.position, Quaternion.identity);
            }
            triggered = true;
            Debug.Log("Recibió el Hit. Cargando siguiente escena...");
            StartCoroutine(LoadNextScene());
        }
    }

    private System.Collections.IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delay);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("No hay más escenas en Build Settings.");
        }
    }
}
