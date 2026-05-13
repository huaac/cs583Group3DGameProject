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

    private Animator animController;

    //for the reveal button script
    public bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        animController = GetComponentInChildren<Animator>();

        rb.freezeRotation = true;
        readyToJump = true;

        isMoving = false;
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f), grounded ? Color.green : Color.red);
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.6f, whatIsGround);
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

        // Jump animation
        if (!grounded)
        {
            animController.SetInteger("playeranims", 2);
        }
        // Idle
        else if (horizontalInput == 0 && verticalInput == 0)
        {
            animController.SetInteger("playeranims", 0);
            isMoving = false;
        }
        // Run
        else
        {
            animController.SetInteger("playeranims", 1);
            isMoving = true;
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