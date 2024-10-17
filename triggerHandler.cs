using System;
using UnityEngine;
using UnityEngine.UI;

public class TriggerHandler : MonoBehaviour
{
    public Text instructionText;
    public npcController npcController;
    public bool left;
    public bool right;

    private void Start()
    {
        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false);
        }

        if (npcController == null)
        {
            npcController = FindObjectOfType<npcController>();
            if (npcController == null)
            {
                Debug.LogError("No npcController found in the scene. Please ensure there is an npcController component attached to a GameObject.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("npcsController"))
        {
            Debug.Log("Entering trigger zone");

            if (instructionText != null)
            {
                instructionText.gameObject.SetActive(true);


                if (left && right)
                {
                    instructionText.text = "Press 'A' to move left or 'D' to move right.";
                }
                else if (left)
                {
                    instructionText.text = "Press 'A' to move left.";
                }
                else if (right)
                {
                    instructionText.text = "Press 'D' to move right.";
                }
                else
                {
                    instructionText.text = "";
                }
            }

            if (npcController != null)
            {
                npcController.SetInTriggerZone(true);
                npcController.canMoveLeft = left;
                npcController.canMoveRight = right;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("npcsController"))
        {
            Debug.Log("Exiting trigger zone");

            if (instructionText != null)
            {
                instructionText.gameObject.SetActive(false);
            }

            if (npcController != null)
            {
                npcController.SetInTriggerZone(false);
                npcController.canMoveLeft = false;
                npcController.canMoveRight = false;
            }
        }
    }
}
