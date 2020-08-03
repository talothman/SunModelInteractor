using UnityEngine;

public class ModelLaserPointer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] positions = new Vector3[2];

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void LateUpdate()
    {
        lineRenderer.SetPositions(positions);
    }

    public void SetLinePositions(Vector3 newStart, Vector3 newEnd) {
        positions[0] = newStart;
        positions[1] = newEnd;
    }

    public void SetPointerVisability(bool on) {
        lineRenderer.enabled = on;
    }
}
