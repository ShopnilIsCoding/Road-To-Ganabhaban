using UnityEngine;

public class TriggerPopup : MonoBehaviour
{
    public GameObject popupObject;  

    void Start()
    {
        
        if (popupObject != null)
        {
            popupObject.SetActive(false);
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("npcsController"))
        {
            // Show the popup or game object
            if (popupObject != null)
            {
                popupObject.SetActive(true);
            }
        }
    }

  
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("npcsController"))
        {
            if (popupObject != null)
            {
                popupObject.SetActive(false);
            }
        }
    }
}
