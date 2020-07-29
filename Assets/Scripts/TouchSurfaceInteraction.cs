using UnityEngine;

public class TouchSurfaceInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform cursorObject;
    [SerializeField]
    private Vector3 positionOffset;
    private MeshRenderer cursorObjectRenderer;

    private int layerMask = 1 << 8;

    private void Start()
    {
        cursorObjectRenderer = cursorObject.GetComponent<MeshRenderer>();
        cursorObjectRenderer.enabled = false;
    }

    private void Update() {

    }
    
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.layer == layerMask) {
            cursorObjectRenderer.enabled = true;
            cursorObject.position = other.GetContact(0).point + Vector3.Project(positionOffset, other.GetContact(0).normal);
            Quaternion targetRotation = Quaternion.FromToRotation(-Vector3.forward, other.GetContact(0).normal);
            cursorObject.rotation = Quaternion.RotateTowards(cursorObject.rotation, targetRotation, 3);
        } else {
            cursorObjectRenderer.enabled = false;
        }
    }
}
