using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public TextMeshProUGUI interactionText;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();

        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }

                return;
            }
        }

        interactionText.gameObject.SetActive(false);
    }
}