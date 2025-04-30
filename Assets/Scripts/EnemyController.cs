using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int EnemyLife;
    public GameObject enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckLife();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Hit(Clone)")
        {
            EnemyLife -= 20;
        }
    }

    public void CheckLife()
    {
        if(EnemyLife <= 0)
        {
            Destroy(enemy, 0.4f);
        }else
        {
            return;
        }
    }

}
