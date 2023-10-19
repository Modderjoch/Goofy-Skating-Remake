using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    public Transform cameraTransform;
    public Animator animator;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Player Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (cameraForward * verticalInput + cameraTransform.right * horizontalInput).normalized;

        // Character Movement
        Vector3 movement = moveDirection * moveSpeed;

        // Rotate the player towards the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Apply movement to the Rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Animation control
        float moveSpeedPercent = movement.magnitude / moveSpeed;
        animator.SetFloat("MoveSpeed", moveSpeedPercent);
    }
}
