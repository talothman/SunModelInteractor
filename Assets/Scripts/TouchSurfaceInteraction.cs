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
    
    private ModelLaserPointer laserPointer;
    private float pointerLength = 5f;
    private MeshRenderer cursorObjectRenderer;

    private int layerMask = 1 << 8;
    private Ray controllerToModelRay;
    private RaycastHit controllerToModelRaycastHit;

    private void Start()
    {
        cursorObjectRenderer = cursorObject.GetComponent<MeshRenderer>();
        cursorObjectRenderer.enabled = false;
        laserPointer = GetComponentInChildren<ModelLaserPointer>();
    }

    private void Update() {
        //OVRInput.Axis1D()
        CheckRaycastToObject();
        laserPointer.SetPointerVisability(true);

    }

    private void CheckRaycastToObject() {      
        controllerToModelRay = new Ray(raySource.position, raySource.TransformDirection(Vector3.forward));
        laserPointer.SetLinePositions(controllerToModelRay.origin, transform.position + transform.forward * pointerLength);
        
        if (Physics.Raycast(controllerToModelRay, out controllerToModelRaycastHit, 10f, layerMask)) {
            laserPointer.SetLinePositions(raySource.position, controllerToModelRaycastHit.point);
            
            if (Vector3.Distance(controllerToModelRaycastHit.point, raySource.position) > 0.1f) {
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
