using UnityEngine;

public class GameManagerLoader : MonoBehaviour
{
    public GameObject gameManagerPrefab;

    void Awake()
    {
        if (GameManagerScript.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }
    }
}
