using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : NetworkBehaviour
{
    [SerializeField] float minViewDistance = 25f;
    [SerializeField] float mouseSensitivity = 100f;

    [SerializeField] Transform playerBody;

    private float xRotation = 0;

    private Vector2 mousePos = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!isLocalPlayer)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        float mouseX = mousePos.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mousePos.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, minViewDistance);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (isLocalPlayer && Application.isFocused)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }
}
