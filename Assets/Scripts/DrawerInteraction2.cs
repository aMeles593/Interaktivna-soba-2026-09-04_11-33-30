using UnityEngine;

public class DrawerInteraction2 : MonoBehaviour, IInteractable
{
    public float openDistance = 1.0f;
    public float openSpeed = 2.0f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.localPosition;

        openPosition = closedPosition + new Vector3(openDistance, 0f, 0f);
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