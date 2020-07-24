using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform cursorObject;
    [SerializeField]
    private Vector3 positionOffset;
    private MeshRenderer cursorObjectRenderer;

    private Camera mainCamera;
    private int layerMask = 1 << 8;
    
    private Ray rayIntoWorld;
    private RaycastHit raycastHit;
    private Vector2 mousePosition;

    private void Start()
    {
        cursorObjectRenderer = cursorObject.GetComponent<MeshRenderer>();
        mainCamera = Camera.main;
        mainCamera.depthTextureMode = DepthTextureMode.DepthNormals;
        cursorObjectRenderer.enabled = false;
    }

    private void Update()
    {
        CheckScale();
        mousePosition = Mouse.current.position.ReadValue();
        rayIntoWorld = mainCamera.ScreenPointToRay(mousePosition);

        if (Mouse.current.leftButton.isPressed) {
            if(Physics.Raycast(rayIntoWorld, out raycastHit, layerMask)) {
                cursorObjectRenderer.enabled = true;
                cursorObject.position = raycastHit.point + Vector3.Project(positionOffset, raycastHit.normal);
                Quaternion targetRotation = Quaternion.FromToRotation(-Vector3.forward, raycastHit.normal);
                cursorObject.rotation = Quaternion.RotateTowards(cursorObject.rotation, targetRotation, 1);
            } else {
                cursorObjectRenderer.enabled = false;
            }
        }
    }

    private void CheckScale() {
        if (!Mouse.current.rightButton.wasPressedThisFrame && !Mouse.current.rightButton.wasReleasedThisFrame) {
            if (Mouse.current.rightButton.isPressed) {
                cursorObject.localScale += Vector3FromScalar(Mouse.current.delta.ReadValue().x)*Time.deltaTime;
            }
        }
    }

    private Vector3 Vector3FromScalar(float scaler) {
        return new Vector3(scaler, scaler, scaler);
    }

    private Vector2 Vector2FromScalar(float scaler) {
        return new Vector2(scaler, scaler);
    }
}
