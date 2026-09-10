using UnityEngine;

public class FrontDoorInteraction : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 2f;

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
        MeshFilter mf = GetComponent<MeshFilter>();

        if (mf != null && mf.sharedMesh != null)
        {
            Bounds meshBounds = mf.sharedMesh.bounds; // LOKALNI prostor mesha (ne collidera!)

            float sign = invertPivotSide ? -1f : 1f;

            Vector3 localEdge = new Vector3(
                meshBounds.center.x + sign * (meshBounds.size.x / 2f),
                meshBounds.center.y,
                meshBounds.center.z
            );

            pivotPoint = transform.TransformPoint(localEdge);

            Debug.Log($"[{name}] Mesh local edge: {localEdge}, World pivot: {pivotPoint}");
        }
        else
        {
            Debug.LogWarning($"[{name}] Nema MeshFilter/mesh! Koristim transform.position.");
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

            transform.RotateAround(pivotPoint, Vector3.up, delta);
            currentAngle = newAngle;
        }
    }

    public void Interact()
    {
        isOpen = !isOpen;
        Debug.Log(isOpen ? "Ulazna vrata otvorena" : "Ulazna vrata zatvorena");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 point = Application.isPlaying ? pivotPoint : transform.position;
        Gizmos.DrawSphere(point, 0.1f);
    }
}