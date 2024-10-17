using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public float maxDistanceFromCamera1 = 10f;

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    void Update()
    {
        if (camera2.activeInHierarchy)
        {
            RotateCamera2();
            MoveCamera2();
            KeepCamera2WithinRange();
        }
    }

    void RotateCamera2()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        camera2.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    void MoveCamera2()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += camera2.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= camera2.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= camera2.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += camera2.transform.right;
        }

        if (direction != Vector3.zero)
        {
            camera2.transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }

    void KeepCamera2WithinRange()
    {
        float distanceFromCamera1 = Vector3.Distance(camera1.transform.position, camera2.transform.position);

        if (distanceFromCamera1 > maxDistanceFromCamera1)
        {
            Vector3 directionToCamera1 = (camera1.transform.position - camera2.transform.position).normalized;
            camera2.transform.position += directionToCamera1 * (distanceFromCamera1 - maxDistanceFromCamera1);
        }
    }

    // Draw Gizmos to show the maximum distance range for camera2
    void OnDrawGizmos()
    {
        if (camera1 != null)
        {
            Gizmos.color = Color.yellow;

            // Draw a wire sphere around camera1 to represent the max distance
            Gizmos.DrawWireSphere(camera1.transform.position, maxDistanceFromCamera1);

            // Draw a line between camera1 and camera2 to visualize their current distance
            if (camera2 != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(camera1.transform.position, camera2.transform.position);
            }
        }
    }
}
