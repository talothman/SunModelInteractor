using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform cursorObject;

    private Camera mainCamera;
    private int layerMask = 1 << 8;
    
    private Ray rayIntoWorld;
    private RaycastHit raycastHit;
    private Vector2 mousePosition;

    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.depthTextureMode = DepthTextureMode.DepthNormals;
    }

    private void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        rayIntoWorld = mainCamera.ScreenPointToRay(mousePosition);

        if(Physics.Raycast(rayIntoWorld, out raycastHit, layerMask)) {
            cursorObject.position = raycastHit.point;
            cursorObject.rotation = Quaternion.FromToRotation(-Vector3.forward, raycastHit.normal);
        }

    }
}
