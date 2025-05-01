using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 2f;
    public int EnemyLife = 100;
    public float MinDistance = 0.5f;
    public float detectionRadius = 3.5f;
    public Transform player;
    public GameObject Recompensa;

    private Animator animator;
    private bool isFacingRight = true;
    private bool isAttacking = false;

    // Hitbox
    public GameObject HitBox;
    public GameObject Hit;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);

            if (distanceToPlayer > MinDistance)
            {
                // Moverse hacia el jugador
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
                isAttacking = false;

                // Vector2 direction = (player.position - transform.position).normalized;
                // movement = new Vector2(direction.x, 0);
            }
            else
            {
                // Atacar si está cerca
                animator.SetBool("isRunning", false);
                GenerateHitBox();
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            transform.position = transform.position;
        }

        Flip(player.position.x > transform.position.x);
        CheckLife();
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        // Espera el tiempo de ataque (ajústalo según tu animación)
        yield return new WaitForSeconds(0.5f);

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
            Speed = 0;
            transform.position = transform.position;
            animator.SetBool("isDeath", true);
            Destroy(gameObject, 1f);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }

    public void GenerateHitBox()
    {
        if (GameObject.Find("HitEnemy(Clone)"))
        {
            return;
        }
        else
        {
            Vector3 PossitionHit = new Vector3(HitBox.transform.position.x, HitBox.transform.position.y, 0);
            GameObject tempHit = Instantiate(Hit, PossitionHit, Quaternion.identity);
            Destroy(tempHit, 0.3f);
        }
    }
}
