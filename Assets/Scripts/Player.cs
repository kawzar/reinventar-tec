using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;
    public float distanceToGround = 0.5f;
    public float jumpForce = 10;
    public LayerMask floorLayer;
    public TextMeshProUGUI scoreText;

    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private int diamonds = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.position = transform.position + Vector3.right * horizontal * speed * Time.deltaTime;
        CheckGround();


        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jump");
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (horizontal != 0)
            {
                sprite.flipX = horizontal < 0;
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    private void CheckGround()
    {
        var hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, distanceToGround, floorLayer);
        isGrounded = hit.collider != null;
    }

    public void OnDiamondGrabbed()
    {
        diamonds++;
        scoreText.SetText(diamonds.ToString());
    }
}
