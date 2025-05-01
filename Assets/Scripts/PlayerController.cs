using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public Transform spawnPoint;
    public int PlayerLife = 100;


    public float moveSpeed = 5f;
    public float fallThreshold = -10f;
    private float moveInputX;
    private float moveInputY;

    public float jumpForce = 7f;
    private bool isAttacking = false;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Saltar solo si está en el suelo
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask whatIsGround;
    private bool isGrounded;

    // Hitbox
    public GameObject HitBox;
    public GameObject Hit;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isAttacking)
            return;

        // Detectar si está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Movimiento horizontal y vertical
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        animator.SetBool("isRunning", moveInputX != 0);
        animator.SetBool("isJumping", !isGrounded); // Para animación

        MoveCharacter();

        // Saltar con W o Flecha Arriba solo si está en el suelo
        if (moveInputY > 0 && isGrounded)
        {
            Jump();
        }

        // Ataque con espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
            GenerateHitBox();
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (transform.position.y < fallThreshold)
        {
            Die();
        }

        // Verificar que la vida del personaje baja a 0
        CheckLife();
    }

    private void Die()
    {
        // Reinicia la posición al punto de aparición, para checkpoints
        // transform.position = spawnPoint.position;

        SceneManager.LoadScene(0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "HitEnemy(Clone)")
        {
            PlayerLife -= 2;
        }
    }

    void MoveCharacter()
    {
        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, rb.linearVelocity.y);

        if (moveInputX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInputX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public void GenerateHitBox()
    {
        if(GameObject.Find("Hit(Clone)"))
        {
            return;
        }else 
        {
            Vector3 PossitionHit = new Vector3(HitBox.transform.position.x, HitBox.transform.position.y, 0);
            GameObject tempHit = Instantiate(Hit, PossitionHit, Quaternion.identity);
            Destroy(tempHit, 0.4f);
        }
    }

    public void CheckLife()
    {
        if (PlayerLife <= 0)
        {
            animator.SetBool("isDeath", true);
            Die();
        }
    }

}