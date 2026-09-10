using UnityEngine;

public class HeadphonesInteraction : MonoBehaviour, IInteractable
{
    [Header("Camera")]
    public Camera playerCamera;

    [Header("Držanje slušalica")]
    public float holdDistance = 1.2f;
    public float moveSpeed = 12f;

    [Header("Pomicanje mišem")]
    public float mouseSensitivity = 3f;

    [Header("Spuštanje na površinu")]
    public float maxDropDistance = 3f;      // koliko daleko traži površinu
    public float surfaceOffset = 0.02f;     // mali razmak da ne "utone" u površinu
    public LayerMask surfaceLayerMask = ~0; // koje slojeve smatra "podlogom" (default: sve)

    private Rigidbody rb;
    private Collider col;

    private bool isHeld = false;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        if (!isHeld)
            return;

        Vector3 targetPosition =
            playerCamera.transform.position +
            playerCamera.transform.forward * holdDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * moveSpeed
        );

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationY += mouseX;
            rotationX -= mouseY;

            rotationX = Mathf.Clamp(rotationX, -80f, 80f);

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }

    public void Interact()
    {
        if (!isHeld)
        {
            PickUp();
        }
        else
        {
            Drop();
        }
    }

    void PickUp()
    {
        isHeld = true;

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (col != null)
        {
            col.isTrigger = true;
        }

        rotationX = 0f;
        rotationY = playerCamera.transform.eulerAngles.y;

        Debug.Log("Slušalice uzete");
    }

    void Drop()
    {
        isHeld = false;

        if (col != null)
        {
            col.isTrigger = false;
        }

        SnapToNearestSurface();

        Debug.Log("Slušalice puštene");
    }

    void SnapToNearestSurface()
    {
        // Traži podlogu ispod slušalica (ravno prema dolje)
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, maxDropDistance, surfaceLayerMask, QueryTriggerInteraction.Ignore))
        {
            // Postavi točno na pronađenu površinu (+ mali offset da ne utone)
            transform.position = hit.point + hit.normal * surfaceOffset;

            // Poravnaj "gore" smjer slušalica s normalom površine,
            // zadrži trenutačni yaw (smjer u koji gledaju) ako je moguće
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;
            if (forward.sqrMagnitude < 0.001f)
            {
                forward = Vector3.ProjectOnPlane(transform.right, hit.normal).normalized;
            }
            transform.rotation = Quaternion.LookRotation(forward, hit.normal);
        }
        else
        {
            // Nije pronađena površina ispod - pusti da normalno padnu s fizikom
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            return;
        }

        // Ostaju kinematic (mirno stoje na površini, ne padaju dalje)
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}