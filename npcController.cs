using System.Collections;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public Transform controller;
    public float moveDistance = 10f;
    public float rotateSpeed = 2f;
    public float moveSpeed = 2f;
    public float forwardSpeed = 5f;
    public CameraToggle cameraToggle;

    public bool canMoveLeft = false;  // Controlled by TriggerHandler
    public bool canMoveRight = false; // Controlled by TriggerHandler

    private bool inTriggerZone = false;
    private bool canMoveForward = true;

    public void SetInTriggerZone(bool isInZone)
    {
        inTriggerZone = isInZone;
        canMoveForward = !isInZone;
    }

    void Update()
    {
        // Move forward if allowed
        if (canMoveForward && Input.GetKey(KeyCode.W))
        {
            controller.position += controller.forward * forwardSpeed * Time.deltaTime;
        }

        // Rotation logic based on canMoveLeft and canMoveRight flags
        if (inTriggerZone)
        {
            if (canMoveLeft && Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(RotateAndMove(-90f)); // Rotate and move left
            }
            else if (canMoveRight && Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(RotateAndMove(90f));  // Rotate and move right
            }
        }
    }

    private IEnumerator RotateAndMove(float rotationAngle)
    {
        canMoveForward = false; // Disable forward movement during rotation/movement
        cameraToggle.enabled = false; // Disable camera control during rotation/movement

        // Rotate the controller
        Quaternion startRotation = controller.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, rotationAngle, 0f);
        float rotateProgress = 0f;
        while (rotateProgress < 1f)
        {
            rotateProgress += Time.deltaTime * rotateSpeed;
            controller.rotation = Quaternion.Lerp(startRotation, targetRotation, rotateProgress);
            yield return null;
        }

        // Move the controller
        Vector3 startPosition = controller.position;
        Vector3 targetPosition = startPosition + controller.forward * moveDistance;
        float moveProgress = 0f;
        while (moveProgress < 1f)
        {
            moveProgress += Time.deltaTime * moveSpeed;
            controller.position = Vector3.Lerp(startPosition, targetPosition, moveProgress);
            yield return null;
        }

        yield return new WaitForSeconds(2f); // Optional delay before allowing movement again

        canMoveForward = true; // Re-enable forward movement
        cameraToggle.enabled = true; // Re-enable camera control
    }
}
