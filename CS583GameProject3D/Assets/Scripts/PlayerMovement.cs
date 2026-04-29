using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    public Animator animController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f), grounded ? Color.green : Color.red);
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);
        Debug.Log(grounded);
        MyInput();
        SpeedControl();

        //handle drag
        if(grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else {rb.linearDamping = 0;}
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Debug.Log(horizontalInput);

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(horizontalInput == 0 && verticalInput == 0)
        {
            animController.SetInteger("playeranims",0);
        }
        else
        {
            animController.SetInteger("playeranims",1);
        }
    }

    //walk in direction we are looking
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized*moveSpeed;
            rb.linearVelocity = new Vector3(limitVel.x, rb.linearVelocity.y, limitVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity to make sure we always jump at same height
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}
//     [SerializeField]
//     private float speed = 5f;

//     [SerializeField]
//     private float mouseSensitivity = 2f;

//     private Vector3 moveDirection;
//     private float rotationY;

//     void Update()
//     {
//         HandleMovement();
//         HandleRotation();
//     }

//     private void HandleMovement()
//     {
//         float horizontal = Input.GetAxis("Horizontal");
//         float vertical = Input.GetAxis("Vertical");

//         moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

//         transform.Translate(moveDirection * speed * Time.deltaTime);
//     }

//     private void HandleRotation()
//     {
//         float mouseX = Input.GetAxis("Mouse X");
//         rotationY += mouseX * mouseSensitivity;

//         transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
//     }
// }







// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     public float speed = 5f;
//     public float jumpForce = 5f;

//     private Rigidbody rb;
//     private bool isGrounded;
//     private Vector3 moveInput;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//     }

//     void Update()
//     {
//         // Get input in Update for better responsiveness
//         float moveX = Input.GetAxisRaw("Horizontal");
//         float moveZ = Input.GetAxisRaw("Vertical");
//         moveInput = new Vector3(moveX, 0f, moveZ).normalized * speed;

//         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//         {
//             rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//             isGrounded = false; // Prevent double jumping
//         }
//     }

//     void FixedUpdate()
//     {
//         // Apply velocity in FixedUpdate for consistent physics
//         Vector3 currentVelocity = rb.linearVelocity;
//         rb.linearVelocity = new Vector3(moveInput.x, currentVelocity.y, moveInput.z);
//     }

//     private void OnCollisionStay(Collision collision)
//     {
//         // Ensure your floor objects actually have the tag "Ground" (case sensitive!)
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = true;
//         }
//     }

//     private void OnCollisionExit(Collision collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = false;
//         }
//     }
// }