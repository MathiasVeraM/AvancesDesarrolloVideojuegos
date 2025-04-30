using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 2f;
    public int EnemyLife = 100;
    public float MinDistance = 1.5f;
    public Transform player;

    private Animator animator;
    private bool isFacingRight = true;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > MinDistance)
        {
            // Moverse hacia el jugador
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
            isAttacking = false;

            transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
        }
        else
        {
            // Atacar si está cerca
            animator.SetBool("isRunning", false);

            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }

        Flip(player.position.x > transform.position.x);
        CheckLife();
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        // Espera el tiempo de ataque (ajústalo según tu animación)
        yield return new WaitForSeconds(0.7f);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    void Flip(bool faceRight)
    {
        if (faceRight != isFacingRight)
        {
            isFacingRight = faceRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1; // Solo invierte X
            transform.localScale = scale;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hit(Clone)")
        {
            EnemyLife -= 20;
        }
    }

    void CheckLife()
    {
        if (EnemyLife <= 0)
        {
            Destroy(gameObject, 0.4f);
        }
    }
}
