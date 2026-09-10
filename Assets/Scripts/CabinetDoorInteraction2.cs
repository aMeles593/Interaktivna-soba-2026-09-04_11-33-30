using UnityEngine;

public class CabinetDoorInteraction2 : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 3f;

    [Header("Ako pivot ispadne na krivoj strani, uključi ovo")]
    public bool invertPivotSide = false;

    private bool isOpen = false;
    private float currentAngle = 0f;

    private Vector3 pivotPoint;

    void Start()
    {
        RecalculatePivot();
    }

    void RecalculatePivot()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        if (box != null)
        {
            float sign = invertPivotSide ? -1f : 1f;

            // Desni brid u LOKALNOM prostoru (uzima local center + pola size po X)
            Vector3 localEdge = new Vector3(
                box.center.x + sign * (box.size.x / 2f),
                box.center.y,
                box.center.z
            );

            pivotPoint = transform.TransformPoint(localEdge);

            Debug.Log($"[{name}] Local edge: {localEdge}, World pivot: {pivotPoint}");
        }
        else
        {
            Debug.LogWarning($"[{name}] Vrata nemaju BoxCollider! Koristim transform.position.");
            pivotPoint = transform.position;
        }
    }

    void Update()
    {
        float targetAngle = isOpen ? openAngle : 0f;

        if (!Mathf.Approximately(currentAngle, targetAngle))
        {
            float step = openSpeed * 50f * Time.deltaTime;
            float newAngle = Mathf.MoveTowards(currentAngle, targetAngle, step);
            float delta = newAngle - currentAngle;

            transform.RotateAround(pivotPoint, transform.up, delta);
            currentAngle = newAngle;
        }
    }

    public void Interact()
    {
        isOpen = !isOpen;
        Debug.Log(isOpen ? "Vrata ormara otvorena" : "Vrata ormara zatvorena");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 point = Application.isPlaying ? pivotPoint : transform.position;
        Gizmos.DrawSphere(point, 0.03f);
    }
}