using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2; 
    private bool isCam1Active;

    void Start()
    {
      
        isCam1Active = true;
        camera1.SetActive(isCam1Active);
        camera2.SetActive(!isCam1Active);
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCameras();
        }
    }

    void ToggleCameras()
    {
      
        isCam1Active = !isCam1Active;
        camera1.SetActive(isCam1Active);
        camera2.SetActive(!isCam1Active);
    }
}
