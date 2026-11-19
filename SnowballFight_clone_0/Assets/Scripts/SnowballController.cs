using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnowballController : NetworkBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] float interactRange = 2f;
    [SerializeField] GameObject snowball;

    [Header("Size")]
    [SerializeField] float maxSize = 0.4f;
    [SerializeField] float medSize = 0.2f;
    [SerializeField] float sizeSpeed;
    private float newScale;

    private RaycastHit hit;
    private bool snowballMax = false;
    private bool lookingAtSnow = false;
    private bool held = false;

    [Header("Size")]
    [SerializeField] float rotSpeed = 5f;
    [SerializeField] float rotAmount = 90f;
    private float time;
    private bool rotateNegative = false;
    private float newRotation;

    private void Start()
    {
        snowball.transform.localScale = Vector3.zero;
    }
    void Update()
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactRange, interactLayer) && snowballMax == false)
        {
            lookingAtSnow = true;
        }
        else
        {
            lookingAtSnow = false;
        }

        MakeSnowball();
        RotateSnowball();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            held = true;
        }
        if (context.canceled)
        {
            held = false;
        }
    }

    private void MakeSnowball()
    {
        if (lookingAtSnow && held)
        {
            ActivateSnowball(true);
            snowball.transform.localScale = Vector3.zero;
            newScale += sizeSpeed * Time.deltaTime;
            if (newScale >= maxSize)
            {
                newScale = maxSize;
                snowballMax = true;
            }
            snowball.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    private void RotateSnowball()
    {
        if (lookingAtSnow && held)
        {
            time += Time.deltaTime * rotSpeed;

            float x = Mathf.Sin(time) * rotAmount;
            float y = Mathf.Sin(time * 1.5f) * rotAmount;

            snowball.transform.localRotation = Quaternion.Euler(x, y, 0);
        }
    }

    [ClientRpc]
    private void ActivateSnowball(bool state)
    {
        snowball.SetActive(state);
    }
}
