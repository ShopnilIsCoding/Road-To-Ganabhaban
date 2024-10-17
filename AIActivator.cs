using UnityEngine;

public class AIActivator : MonoBehaviour
{
   
    public ObjectSelector objectSelector;
    
  
    public string aiScriptName = "AI"; 

   
    public void OnButtonClick()
    {
       
        if (objectSelector != null && !string.IsNullOrEmpty(objectSelector.tagName))
        {
           
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(objectSelector.tagName);

           
            foreach (GameObject obj in objectsWithTag)
            {
               
                MonoBehaviour aiScript = (MonoBehaviour)obj.GetComponent(aiScriptName);
                
               
                if (aiScript != null)
                {
                    aiScript.enabled = true;
                }
            }

            Debug.Log("AI scripts enabled for all objects with tag: " + objectSelector.tagName);
        }
        else
        {
            Debug.Log("No object selected or invalid tag.");
        }
    }
}
