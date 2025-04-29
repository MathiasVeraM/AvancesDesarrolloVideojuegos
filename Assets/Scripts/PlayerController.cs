using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    private float moveInput;

    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Si está atacando, no permite moverse ni cambiar animación
        if (isAttacking)
            return;

        // Movimiento horizontal
        moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isRunning", moveInput != 0);

        MoveCharacter();

        // Ataque con barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
    }

    void MoveCharacter()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // Corrutina para manejar el ataque
    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        // Esperar el tiempo que dura la animación (ajusta este valor al de tu animación)
        yield return new WaitForSeconds(0.5f); // duración del ataque en segundos

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
