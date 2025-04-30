using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    private float moveInputX;
    private float moveInputY;

    public float jumpForce = 7f;
    private bool isAttacking = false;
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isAttacking)
            return;

        // Movimiento en eje X e Y
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        animator.SetBool("isRunning", moveInputX != 0);

        MoveCharacter();

        // Saltar con W o Flecha Arriba (↑)
        if (moveInputY > 0 && !isJumping)
        {
            Jump();
        }

        // Ataque con espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
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
        animator.SetBool("isJumping", true);
        isJumping = true;

        StartCoroutine(EndJumpAnimation());
    }

    System.Collections.IEnumerator EndJumpAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isJumping", false);
        isJumping = false;
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}