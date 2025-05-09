using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    Transform target;              // Player to follow
    Transform cameraTransform;     // The actual camera
    public Vector3 cameraOffset = new Vector3(0, 5, -5); // Adjust for 45° angle
    public float rotationSpeed = 5f;
    public float followSpeed = 10f;
    public float pitch = 45f;             // Fixed vertical angle

    private float currentYaw = 0f;

    void Start()
    {
        target = GetComponentInParent<Transform>();
        cameraTransform = GetComponent<Camera>().transform;
    }
    void LateUpdate()
    {
        if (!target) return;

        // Follow player position smoothly
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

        // Rotate around Y axis with mouse X
        currentYaw += Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, currentYaw, 0);

        // Position and aim the camera
        Vector3 rotatedOffset = Quaternion.Euler(pitch, currentYaw, 0) * cameraOffset;
        cameraTransform.position = target.position + rotatedOffset;
        cameraTransform.LookAt(target.position);
    }
}
