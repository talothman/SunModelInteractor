using UnityEngine;

public class TouchSurfaceInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform cursorObject;
    [SerializeField]
    private Vector3 positionOffset;
    [SerializeField]
    private Transform modelTransform;
    [SerializeField]
    private Transform raySource;
    private MeshRenderer cursorObjectRenderer;

    private int layerMask = 1 << 8;
    private Ray controllerToModelRay;
    private RaycastHit controllerToModelRaycastHit;

    private void Start()
    {
        cursorObjectRenderer = cursorObject.GetComponent<MeshRenderer>();
        cursorObjectRenderer.enabled = false;
    }

    private void Update() {
        //OVRInput.Axis1D()
        CheckRaycastToObject();
    }

    // private void OnCollisionStay(Collision other) {
    //     if((1 << other.gameObject.layer) == layerMask) {
    //         cursorObjectRenderer.enabled = true;
    //         cursorObject.position = other.collider.ClosestPoint(transform.position) + Vector3.Project(positionOffset, other.GetContact(0).normal);
    //         Quaternion targetRotation = Quaternion.FromToRotation(-Vector3.forward, other.GetContact(0).normal);
    //         cursorObject.rotation = Quaternion.RotateTowards(cursorObject.rotation, targetRotation, 50);
    //     }
    // }

    private void CheckRaycastToObject() {      

        controllerToModelRay = new Ray(transform.position, modelTransform.position-transform.position);
        if (Physics.Raycast(controllerToModelRay, out controllerToModelRaycastHit, 10f, layerMask)) {
            if (Vector3.Distance(controllerToModelRaycastHit.point, transform.position) > 0.1f) {
                cursorObjectRenderer.enabled = false;
                return;
            }

            cursorObjectRenderer.enabled = true;
            cursorObject.position = controllerToModelRaycastHit.point + Vector3.Project(positionOffset, controllerToModelRaycastHit.normal);
            Quaternion targetRotation = Quaternion.FromToRotation(-Vector3.forward, controllerToModelRaycastHit.normal);
            cursorObject.rotation = Quaternion.RotateTowards(cursorObject.rotation, targetRotation, 50);
        }
    }
}
