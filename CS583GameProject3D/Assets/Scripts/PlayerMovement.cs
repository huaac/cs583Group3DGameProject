using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input in Update for better responsiveness
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(moveX, 0f, moveZ).normalized * speed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent double jumping
        }
    }

    void FixedUpdate()
    {
        // Apply velocity in FixedUpdate for consistent physics
        Vector3 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(moveInput.x, currentVelocity.y, moveInput.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Ensure your floor objects actually have the tag "Ground" (case sensitive!)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}