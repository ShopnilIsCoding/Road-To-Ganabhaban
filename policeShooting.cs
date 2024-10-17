using System.Collections;
using UnityEngine;

public class PoliceShooting : MonoBehaviour
{
    public GameObject bulletParticle;      // The bullet particle system prefab
    public Transform gunBarrel;            // Position of the gun barrel (where the bullet particle should spawn)
    public AudioClip deathSound;           // Death sound for NPCs
    public float minShootInterval = 2f;    // Minimum time between shots
    public float maxShootInterval = 3f;    // Maximum time between shots
    public string npcTag = "npckill";      // Tag used to find NPCs
    public float shootingRadius = 15f;     // Radius within which NPCs can be shot

    private Animator npcAnimator;           // NPC animator (for death animation)
    private Animator policeAnimator;
    private GameObject[] npcList;           // List of NPCs in the scene
    public AudioSource audioSource;        // AudioSource for playing shooting sounds
    private int shotsFired = 0;             // Counter for shots fired
    public int maxBullet;
    private GameObject currentTargetNPC = null; // Track the current NPC being targeted
    public npcController npcController;

    void Start()
    {
        // Start the shooting coroutine
        StartCoroutine(ShootingRoutine());
        policeAnimator = GetComponent<Animator>();
    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            // Wait for a random interval between minShootInterval and maxShootInterval
            float waitTime = Random.Range(minShootInterval, maxShootInterval);
            yield return new WaitForSeconds(waitTime);

            // Shoot the gun if NPCs are within range and if no target is being shot
            if (currentTargetNPC == null)
            {
                ShootGun();
            }
        }
    }

    void ShootGun()
    {
        // Find all NPCs with the tag "npckill"
        npcList = GameObject.FindGameObjectsWithTag(npcTag);

        if (npcList.Length > 0)
        {
            // Find an NPC that is within the shooting radius
            currentTargetNPC = GetNPCWithinRadius();

            if (currentTargetNPC != null)
            {
                // Play bullet particle system
                if (bulletParticle != null)
                {
                    Instantiate(bulletParticle, gunBarrel.position, gunBarrel.rotation);
                }

                // Play gunshot sound using AudioSource
                if (audioSource != null)
                {
                    audioSource.Play();
                }

                // Increment the shots fired counter
                shotsFired++;

                // Kill the selected NPC
                KillNPC(currentTargetNPC);

                // Check if the police should retreat after maxBullet shots
                if (shotsFired >= maxBullet)
                {
                    TriggerRetreat();
                    return;  // Stop further shooting after retreat is triggered
                }
            }
        }
    }

    GameObject GetNPCWithinRadius()
    {
        // Iterate through all NPCs and return the first one within the shooting radius
        foreach (GameObject npc in npcList)
        {
            float distance = Vector3.Distance(transform.position, npc.transform.position);
            if (distance <= shootingRadius)
            {
                return npc; // Return the first NPC in range
            }
        }

        // Return null if no NPC is within range
        return null;
    }

    void KillNPC(GameObject npc)
    {
        npcController.enabled= false;
        // Get the NPC's Animator
        npcAnimator = npc.GetComponent<Animator>();

        if (npcAnimator != null)
        {
            // Trigger the death animation on the NPC's animator
            npcAnimator.SetTrigger("Dead");

            // Play death sound
            AudioSource.PlayClipAtPoint(deathSound, npc.transform.position);

            // Wait for the death animation to finish before destroying the NPC
            float animationLength = GetAnimationClipLength(npcAnimator, "Dead");

            // Start coroutine to destroy NPC after death animation
            StartCoroutine(DestroyAfterDeath(npc, animationLength));

            // Clear current target after killing the NPC
            StartCoroutine(ClearTargetAfterDelay(3f)); // Delay to ensure cleanup
        }
    }

    private IEnumerator ClearTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentTargetNPC = null; // Reset target so the police can shoot a new NPC
        npcController.enabled=true;
    }

    private IEnumerator DestroyAfterDeath(GameObject npc, float delay)
    {
        // Wait for the length of the death animation
        yield return new WaitForSeconds(delay);

        // Destroy or deactivate the NPC after the death animation ends
        Destroy(npc);
    }

    float GetAnimationClipLength(Animator animator, string clipName)
    {
        // Find the animation clip by name and return its length
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        // Default to 3 seconds if the animation is not found (you can adjust this)
        return 3f;
    }

    void TriggerRetreat()
    {
        // Trigger the retreat animation
        if (policeAnimator != null)
        {
            policeAnimator.SetTrigger("retreat");
        }

        // Start a coroutine to destroy the police GameObject after the retreat animation ends
        StartCoroutine(DestroyAfterRetreat());
    }

    private IEnumerator DestroyAfterRetreat()
    {
        // Wait for the length of the retreat animation
        float retreatAnimationLength = GetAnimationClipLength(policeAnimator, "retreat");
        yield return new WaitForSeconds(retreatAnimationLength);

        // Destroy or deactivate the police GameObject after the retreat animation ends
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Set the gizmo color to something visible (like red)
        Gizmos.color = Color.red;

        // Draw a wire sphere at the police's position with the shooting radius
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }
}
