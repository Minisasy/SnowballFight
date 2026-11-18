using UnityEngine;
using UnityEngine.InputSystem;

public class SnowballController : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] float interactRange = 2f;
    [SerializeField] float maxSize = 0.4f;
    [SerializeField] GameObject snowball;
    [SerializeField] float speed;

    private RaycastHit hit;
    private bool snowballMax = false;
    private bool lookingAtSnow = false;
    private bool held = false;
    private float newScale;

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
            snowball.transform.localScale = Vector3.zero;
            newScale += speed * Time.deltaTime;
            if (newScale >= maxSize)
            {
                newScale = maxSize;
            }
            snowball.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
