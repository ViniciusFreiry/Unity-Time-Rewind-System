using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float gravityScale = 2.5f;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private float move;
    private int isWalkingHash = Animator.StringToHash("isWalking");

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        move = Input.GetAxis("Horizontal");
        move = Mathf.Abs(move) < 0.01f ? 0f : move;

        isGrounded = Physics2D.OverlapCircle(foot.position, 0.1f, groundLayer);

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        animator.SetBool(isWalkingHash, move != 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        bool isWalking = rb.velocity.x != 0;

        if (rb.velocity.x > 0 && transform.localScale.x == -1)
        {
            animator.Play("Player_Turn_Around");
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0 && transform.localScale.x == 1)
        {
            animator.Play("Player_Turn_Around");
            transform.localScale = new Vector3(-1, 1, 1);
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (isGrounded)
        {
            if (stateInfo.IsName("Player_Jump_Up") || stateInfo.IsName("Player_Jump_Down"))
            {
                if (isWalking)
                {
                    animator.Play("Player_Run");
                }
                else
                {
                    animator.Play("Player_Idle");
                }
            }
        }
        else
        {
            if (rb.velocity.y > 0 && !stateInfo.IsName("Player_Jump_Up"))
            {
                animator.Play("Player_Jump_Up");
            }
            else if (rb.velocity.y < 0 && !stateInfo.IsName("Player_Jump_Down"))
            {
                animator.Play("Player_Jump_Down");
            }
        }
    }

    public float GetGravityScale()
    {
        return gravityScale;
    }
}