using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float walkSpeed = 10f;


    Vector2 moveInput;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (isLocalPlayer)
        {
            Vector3 playerVelocity = new Vector3(moveInput.x * walkSpeed, rb.linearVelocity.y, moveInput.y * walkSpeed);
            rb.linearVelocity = transform.TransformDirection(playerVelocity);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (isLocalPlayer && Application.isFocused)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }
}
