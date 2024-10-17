using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    private GameObject selectedObject;
    private GameObject activeRing; 
    public string tagName;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
              
                GameObject hitObject = hit.collider.gameObject;
                selectedObject = hitObject;
                tagName = hitObject.tag;

               
                Transform ringTransform = selectedObject.transform.Find("Ring");

                if (ringTransform != null)
                {
                    GameObject newRing = ringTransform.gameObject;

                   
                    if (activeRing != null && activeRing != newRing)
                    {
                        activeRing.SetActive(false); 
                    }

                   
                    newRing.SetActive(true);
                    activeRing = newRing;

                    Debug.Log("Ring activated for: " + selectedObject.name);
                }
                else
                {
                    Debug.LogWarning("Ring object not found for: " + selectedObject.name);
                }
            }
        }
    }
}
