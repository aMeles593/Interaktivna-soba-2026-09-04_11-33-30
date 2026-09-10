using UnityEngine;

public class CabinetDoorInteraction : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 3f;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private bool isOpen = false;

    void Start()
    {
        closedRotation = transform.localRotation;

        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            openSpeed * 50f * Time.deltaTime
        );
    }

    public void Interact()
    {
        isOpen = !isOpen;

        Debug.Log(isOpen ? "Vrata ormara otvorena" : "Vrata ormara zatvorena");
    }
}