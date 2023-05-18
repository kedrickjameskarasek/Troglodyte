using UnityEngine;

public class playerController : MonoBehaviour
{
    //Ridgidbody
    private Rigidbody rb;

    //movement speeds
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float lookSpeed = 3f;

    //camera movement
    public Transform cameraTransform;
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;
    private float cameraAngle = 0f;

    //ground detection
    public LayerMask groundLayer;
    public float raycastDistance = 0.5f;
    private bool isGrounded = true;

    void Start()
    {
        //assigns ridgid body at start
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //detects left/right and forward/backwards movement (wasd)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //stores input change
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        //detects direction camera is facing 
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        //locks character upright
        moveDirection.y = 0f;

        //moves the player
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        // Perform raycast to check if player is grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        if (pauseMenu.gameIsPaused || pauseMenu.inventoryIsOpened)
        {
            lookSpeed = 0;
        }
        else
        {
            lookSpeed = 3;
        }

        //detecting input for jump and to see if player is grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        //getting mouse x movement
        float mouseX = Input.GetAxis("Mouse X");

        //vector rotating around
        transform.Rotate(Vector3.up, mouseX * lookSpeed);

        //detects movement on y direction 
        cameraAngle -= Input.GetAxis("Mouse Y") * lookSpeed;

        //max and min look height
        cameraAngle = Mathf.Clamp(cameraAngle, -60f, 60f);

        // keeps camera uniform distance from player
        Vector3 cameraOffset = Quaternion.Euler(cameraAngle, transform.eulerAngles.y, 0f) * new Vector3(0f, cameraHeight, -cameraDistance);

        //transforms camera with camera offset
        cameraTransform.position = transform.position + cameraOffset;

        //Rotates the transform so the forward vector points at the world position
        cameraTransform.LookAt(transform.position);
    }
}