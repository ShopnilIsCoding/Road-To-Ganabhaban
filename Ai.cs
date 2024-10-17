using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Ai : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform dest;
    public AudioSource walkSound;
    Animator animator;

    // Static list to track assigned destinations
    private static List<Transform> assignedDests = new List<Transform>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // If no destination is assigned initially, find a new one
        if (dest == null)
        {
            AssignNewDestination();
        }
    }

    void Update()
    {
        if (dest == null)
        {
            // Try to find a new destination if the current one is null
            AssignNewDestination();

            // If no destination could be assigned, trigger the "Dead" animation and destroy the GameObject
            if (dest == null)
            {
                animator.SetTrigger("Dead");
                Destroy(agent.gameObject, 3f);
                return; // Exit update if no destination
            }
        }

        if (agent != null && dest != null)
        {
            agent.destination = dest.position;

            if (agent.velocity.magnitude > 0.1f)
            {
                // Play the walking sound only if it's assigned
                if (walkSound != null && !walkSound.isPlaying)
                {
                    walkSound.Play();
                }
            }
            else
            {
                // Pause the walking sound if the agent is not moving and the sound is assigned
                if (walkSound != null && walkSound.isPlaying)
                {
                    walkSound.Pause();
                }
                transform.LookAt(dest.position);
            }

            animator.SetFloat("speed", agent.velocity.magnitude);
        }
    }

    // Assign the closest available destination dynamically if one becomes available
    void AssignNewDestination()
    {
        // Find all objects with the tag "dest"
        GameObject[] possibleDests = GameObject.FindGameObjectsWithTag("dest");
        Transform closestDest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject potentialDest in possibleDests)
        {
            Transform potentialTransform = potentialDest.transform;

            // Check if the potential destination is not the AI itself and not already assigned to another AI
            if (potentialTransform != transform && !assignedDests.Contains(potentialTransform))
            {
                // Calculate the distance from this AI to the potential destination
                float distance = Vector3.Distance(transform.position, potentialTransform.position);

                // Check if this destination is closer than the current closest destination
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDest = potentialTransform;
                }
            }
        }

        // If a valid destination is found, assign it and mark it as used
        if (closestDest != null)
        {
            dest = closestDest;
            assignedDests.Add(dest);  // Mark this destination as assigned
        }
    }

    private void OnDestroy()
    {
        // When this AI is destroyed, remove its assigned destination from the list
        if (dest != null)
        {
            assignedDests.Remove(dest);
        }
    }
}
