using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMirpur10Animation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool firstTime = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // wait for esc button to be pressed to load the animation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameObject.activeSelf){
                // quit the game
                Application.Quit();
            }else
            {
                // pause the game
                Time.timeScale = 0;
            }
            // show menu options
        }else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(firstTime){
                // play the animation
                // GetComponent<Animator>().Play("Mirpur10");
                TimeLine.instance.playDollyRollScene();
                gameObject.SetActive(false);
                firstTime = false;
                return;
            }
            
            if(gameObject.activeSelf){
                Time.timeScale = 1;
                gameObject.SetActive(false);
            }
        }
        
    }
}
