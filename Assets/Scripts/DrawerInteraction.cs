using UnityEngine;

public class DrawerInteraction : MonoBehaviour, IInteractable
{
    public float openDistance = 2.0f;
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.localPosition;

        // Otvaranje ladice prema naprijed
        openPosition = closedPosition + new Vector3(0f, 0f, -openDistance);
    }

    void Update()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;

        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            targetPosition,
            openSpeed * Time.deltaTime
        );
    }

    public void Interact()
    {
        isOpen = !isOpen;

        Debug.Log(isOpen ? "Ladica otvorena" : "Ladica zatvorena");
    }
}