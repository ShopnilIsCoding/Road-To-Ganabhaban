using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGunShotAnimation : MonoBehaviour
{
    public Animator animator;
    public GameObject dialogueManager;// for start dialogue

    public GameObject NPCEnable;
    public GameObject TrainEnable;

    void Start(){
        // animator = GetComponent<Animator>();
        NPCEnable.SetActive(true);
        TrainEnable.SetActive(true);
    }

    public void PlayGunShotAnime(){
        //play the animation
        Debug.Log("PlayGunShotAnime");
        animator.Play("spare3");
        // animator.SetTrigger("mirpur10death");
    }
    public void StartMainGameplay(){
        dialogueManager.SetActive(true);
    }
}
