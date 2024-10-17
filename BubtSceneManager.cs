using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class BubtSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera virtualCamera;
    public GameObject box_left, box_right, box_middle, box_up, box_down, box_extra;
    public static BubtSceneManager Instance; // Singleton instance

    public GameObject AcctiveObject,NotActiveObject;

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // add cine machine camera
    void Start()
    {
        // adjust camera position to UnityEditor.TransformWorldPlacementJSON:{"position":{"x":0.0,"y":0.0,"z":0.0},"rotation":{"x":0.4361010789871216,"y":-0.3698270916938782,"z":0.20289406180381776,"w":0.7949073910713196},"scale":{"x":1.0,"y":1.0,"z":1.0}}
        virtualCamera.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        virtualCamera.transform.rotation = new Quaternion(0.4361010789871216f, -0.3698270916938782f, 0.20289406180381776f, 0.7949073910713196f);
        virtualCamera.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        AcctiveObject.SetActive(true);
        NotActiveObject.SetActive(false);

        box_left.transform.Find("Ring").gameObject.SetActive(true);
        box_right.transform.Find("Ring").gameObject.SetActive(true);
        box_middle.transform.Find("Ring").gameObject.SetActive(true);
        box_up.transform.Find("Ring").gameObject.SetActive(true);
        box_down.transform.Find("Ring").gameObject.SetActive(true);
        box_extra.transform.Find("Ring").gameObject.SetActive(true);
        Invoke("HideAllRings", 3.0f);
    }
    void HideAllRings()
    {
        box_left.transform.Find("Ring").gameObject.SetActive(false);
        box_right.transform.Find("Ring").gameObject.SetActive(false);
        box_middle.transform.Find("Ring").gameObject.SetActive(false);
        box_up.transform.Find("Ring").gameObject.SetActive(false);
        box_down.transform.Find("Ring").gameObject.SetActive(false);
        box_extra.transform.Find("Ring").gameObject.SetActive(false);
    }
    public int boxClicked = 0; // 0 for none, 1=left, 2=right, 3=middle, 4=up, 5=down, 6 extra
    public string selectedObject = "zero";
    bool BoxSelected = false;
    public void OnBoxClicked(GameObject clickedObject)
    {
        // Do something with the clicked object
        Debug.Log("Clicked: " + clickedObject.name);
        if (clickedObject.name == "box_extra"){
            if(BoxSelected && selectedObject == "box_right"){
                // move the npc to the extra box
                MoveNPCSafe();

            }
        }
        else if (clickedObject.name != "box_middle"){
            selectedObject = clickedObject.name;
            BoxSelected = true;
        }
        box_extra.transform.Find("Ring").gameObject.SetActive(false);


        if(clickedObject.name == "box_left")
        {
            
            box_left.transform.Find("Ring").gameObject.SetActive(true);
            box_right.transform.Find("Ring").gameObject.SetActive(false);
            box_middle.transform.Find("Ring").gameObject.SetActive(true);
            box_up.transform.Find("Ring").gameObject.SetActive(false);
            box_down.transform.Find("Ring").gameObject.SetActive(false);
            box_extra.transform.Find("Ring").gameObject.SetActive(false);
        }
        else if(clickedObject.name == "box_right")
        {
            box_left.transform.Find("Ring").gameObject.SetActive(false);
            box_right.transform.Find("Ring").gameObject.SetActive(true);
            box_middle.transform.Find("Ring").gameObject.SetActive(true);
            box_up.transform.Find("Ring").gameObject.SetActive(false);
            box_down.transform.Find("Ring").gameObject.SetActive(false);
            box_extra.transform.Find("Ring").gameObject.SetActive(true);
        }
        else if(clickedObject.name == "box_middle")
        {
            if(selectedObject == "box_left")
            {
                LeftNPCattack();
            }
            else if(selectedObject == "box_right")
            {
                RightNPCattack();
            }
            else if(selectedObject == "box_up")
            {
                UpNPCattack();
            }
            else if(selectedObject == "box_down")
            {
                DownNPCattack();
            }
        }
        else if(clickedObject.name == "box_up")
        {
            box_left.transform.Find("Ring").gameObject.SetActive(false);
            box_right.transform.Find("Ring").gameObject.SetActive(false);
            box_middle.transform.Find("Ring").gameObject.SetActive(true);
            box_up.transform.Find("Ring").gameObject.SetActive(true);
            box_down.transform.Find("Ring").gameObject.SetActive(false);
            box_extra.transform.Find("Ring").gameObject.SetActive(false);

            
        }
        else if(clickedObject.name == "box_down")
        {
            box_left.transform.Find("Ring").gameObject.SetActive(false);
            box_right.transform.Find("Ring").gameObject.SetActive(false);
            box_middle.transform.Find("Ring").gameObject.SetActive(true);
            box_up.transform.Find("Ring").gameObject.SetActive(false);
            box_down.transform.Find("Ring").gameObject.SetActive(true);
            box_extra.transform.Find("Ring").gameObject.SetActive(false);
        }
        else if(clickedObject.name == "box_extra")
        {
            // box_left.transform.Find("Ring").gameObject.SetActive(false);
            // box_right.transform.Find("Ring").gameObject.SetActive(false);
            // box_middle.transform.Find("Ring").gameObject.SetActive(true);
            // box_up.transform.Find("Ring").gameObject.SetActive(false);
            // box_down.transform.Find("Ring").gameObject.SetActive(false);
            // box_extra.transform.Find("Ring").gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Invalid box clicked");
        }




        // if (clickedObject.name != "box_middle")
        // {
        //     selectedObject = clickedObject.name;
        //     // for eeach box colider




        //     foreach (GameObject location in LocationSelections)
        //     {
        //         if (clickedObject.name == "box_right")
        //         {
        //             if (location.name == "box_middle" || location.name == "box_extra")
        //             {
        //                 location.transform.Find("Ring").gameObject.SetActive(true);
        //             }
        //             else
        //             {
        //                 location.transform.Find("Ring").gameObject.SetActive(false);
        //             }

        //         }
        //         else
        //         {
        //             // if the clicked object is the current object
        //             if (location.name == clickedObject.name)
        //             {
        //                 // enable the ring object
        //                 location.transform.Find("Ring").gameObject.SetActive(true);
        //                 BoxSelected = true;

        //             }
        //             else
        //             {
        //                 location.transform.Find("Ring").gameObject.SetActive(false);
        //             }
        //         }



        //     }



        // }
        // else
        // {
        //     // middle clicked
        //     if (BoxSelected)
        //     {
        //         clickedObject.gameObject.transform.Find("Ring").gameObject.SetActive(false);
        //         if (selectedObject == "box_left")
        //         {
        //             LeftNPCattack();
        //         }
        //         else if (selectedObject == "box_right")
        //         {
        //             RightNPCattack();
        //         }
        //         else if (selectedObject == "box_up")
        //         {
        //             UpNPCattack();
        //         }
        //         else if (selectedObject == "box_down")
        //         {
        //             DownNPCattack();
        //         }
        //         // move the camera to the selected object
        //         // virtualCamera.transform.position = new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y, virtualCamera.transform.position.z);
        //         BoxSelected = false;
        //     }
        // }







        // You can implement your logic here, like changing the camera position
        // if(clickedObject.name != "box_middle"){
        //     selectedObject = clickedObject.name;
        // }




    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            // Debug.Log("BubtSceneManager is active");
            // on mouse scroll up, move the camera forward with mouse position as the pivot point mot more than 10 units
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && virtualCamera.transform.position.z < 10)
            {
                // Debug.Log("Mouse ScrollWheel up");
                // move the camera forward
                virtualCamera.transform.position = new Vector3(virtualCamera.transform.position.x, virtualCamera.transform.position.y, virtualCamera.transform.position.z + 1);
            }
            // on mouse scroll down, move the camera backward with mouse position as the pivot point not more than 10 units
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && virtualCamera.transform.position.z > -10)
            {
                // Debug.Log("Mouse ScrollWheel down");

                // move the camera backward
                virtualCamera.transform.position = new Vector3(virtualCamera.transform.position.x, virtualCamera.transform.position.y, virtualCamera.transform.position.z - 1);
            }
            // Detect when the left mouse button is clicked to select a parent object
            // if (Input.GetMouseButtonDown(0))  // Left-click
            // {
            //     SelectParentAndMoveNPCs();
            // }

        }
    }

    // void LeftNPCattack()
    // {
    //     Debug.Log("Left NPC attack");
    //     // for each child in the box_left
    //     foreach (Transform child in box_left.transform.Find("npcs"))
    //     {
    //         NavMeshAgent agent = child.GetComponent<NavMeshAgent>();


    //         NavMeshPath path = new NavMeshPath();
    //         if (agent.CalculatePath(box_middle.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
    //         {
    //             agent.SetDestination(box_middle.transform.position);
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Destination is not reachable for " + child.name);
    //         }




    //         if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
    //         {
    //             // Set the destination
    //             agent.SetDestination(box_middle.transform.position);

    //             // Make the child look at the target
    //             child.LookAt(box_middle.transform.position);

    //             // Update the animation speed based on the agent's velocity
    //             child.GetComponent<Animator>().SetFloat("speed", agent.velocity.magnitude);
    //         }
    //         else
    //         {
    //             Debug.LogWarning("NavMeshAgent is either not active, not enabled, or not on a NavMesh for: " + child.name);
    //         }
    //     }


    // }

    int NPCAttackCount = 0;

    void AttactOnPosition(GameObject box_name, GameObject destination)
    {
        NPCAttackCount++;
        if(NPCAttackCount > 2)
        {
            NPCAttackCount = 0;
            TimeLine.instance.playShopNilScene();
        }
        Debug.Log("Left NPC attack");
    foreach (Transform child in box_name.transform.Find("npcs"))
    {
        NavMeshAgent agent = child.GetComponent<NavMeshAgent>();

        if (agent != null && agent.isActiveAndEnabled)
        {
            // if (!agent.isOnNavMesh)
            // {
            //     // Try to find a nearby point on the NavMesh
            //     NavMeshHit hit;
            //     if (NavMesh.SamplePosition(child.position, out hit, 5.0f, NavMesh.AllAreas))
            //     {
            //         child.position = hit.position;  // Snap to valid NavMesh position
            //         Debug.Log("Child snapped to NavMesh: " + child.name);
            //     }
            //     else
            //     {
            //         Debug.LogWarning("No valid NavMesh position found for: " + child.name);
            //         continue;
            //     }
            // }

            if (agent.isOnNavMesh)
            {
                // Generate a random direction
                Vector3 randomDirection = Random.insideUnitSphere * 10.0f;
                randomDirection.y = 0;  // Keep them on the same horizontal plane

                Vector3 destinationRandom = destination.transform.position + randomDirection;


                agent.SetDestination(destinationRandom);

                child.LookAt(destinationRandom);

                child.GetComponent<Animator>().SetFloat("speed", agent.velocity.magnitude);
            }else
            {
                Debug.Log("NavMeshAgent is either not active or not enabled for: " + child.name);
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is either not active or not enabled for: " + child.name);
        }
    }
    }

    
    void LeftNPCattack()
    {
        NPCAttackCount++;
        AttactOnPosition(box_left, box_middle);
    }

    
    
    void RightNPCattack()
    {
        AttactOnPosition(box_right, box_middle);
    }
    void UpNPCattack()
    {
        AttactOnPosition(box_up, box_middle);

    }
    void DownNPCattack()
    {
        AttactOnPosition(box_down, box_middle);

    }
    void MoveNPCSafe()
    {
        AttactOnPosition(box_right, box_extra);

    }


}
