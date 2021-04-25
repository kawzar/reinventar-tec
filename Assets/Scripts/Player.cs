using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;
    public float distanceToGround = 0.5f;
    public float jumpForce = 10;
    public LayerMask floorLayer;

    private bool isGrounded;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.right * Input.GetAxis("Horizontal") * speed;
        CheckGround();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckGround()
    {
        var hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, distanceToGround, floorLayer);
        isGrounded = hit.collider != null;
    }
}
