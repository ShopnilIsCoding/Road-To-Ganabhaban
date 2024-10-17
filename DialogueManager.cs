using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text msgText;
    public Text btnLeftText;
    public Text btnRightText;

    public GameObject BtnLeft;
    public GameObject BtnRight;

    public int Score = 0;
    // Start is called before the first frame update
    private int currentQuestion = 0;
    void Start()
    {
        AskQuestion_01();
        // start button click event
        BtnLeft.GetComponent<Button>().onClick.AddListener(OnBtnLeftClick);
        BtnRight.GetComponent<Button>().onClick.AddListener(OnBtnRightClick);
    }
    public void OnBtnLeftClick()
    {
        Debug.Log("Left Button Clicked"+currentQuestion);
        dialogueBox.SetActive(false);
        if (currentQuestion == 1)
        {
            Score += 1;
            AskQuestion_02();
        }
        if (currentQuestion == 2)
        {
            Score += 1;
            AskQuestion_03();
        }
        if (currentQuestion == 3)
        {
            Score += 1;
            AskQuestion_05();
        }
        if (currentQuestion == 4)
        {
            Score += 1;
            AskQuestion_07();
        }
        if (currentQuestion == 5)
        {
            Score += 1;
            AskQuestion_06();
        }
        if (currentQuestion == 6)
        {
            Score += 1;
            AskQuestion_04();
        }
        if (currentQuestion == 7)
        {
            Score -= 1;
            //end the game play gonobobon scene
            TimeLine.instance.playShopNilScene();
        }
    }
    public void OnBtnRightClick()
    {
        Debug.Log("Right Button Clicked"+currentQuestion);

        //
        dialogueBox.SetActive(false);
        if (currentQuestion == 1)
        {
            Score += 1;
            AskQuestion_03();
        }
        if (currentQuestion == 2)
        {
            Score += 1;
            AskQuestion_03();
        }
        if (currentQuestion == 3)
        {
            Score -= 1;
            AskQuestion_04();
        }
        if (currentQuestion == 4)
        {
            Score -= 1;
            AskQuestion_07();
        }
        if (currentQuestion == 5)
        {
            Score -= 1;
            AskQuestion_06();
        }
        if (currentQuestion == 6)
        {
            Score += 1;
            AskQuestion_07();
        }
        if (currentQuestion == 7)
        {
            Score += 1;
            //end the game and play bubt scene
            // TimeLine.instance.playBubtScene();
            TimeLine.instance.playShopNilScene();
        }

    }
    private void AskQuestion_01()
    {
        Debug.Log("Asking Question One");
        currentQuestion = 1;
        dialogueBox.SetActive(true);
        msgText.text = "g‡i †Mjv?! Gme KvU©yb gvK©v dvjZz wgwQj Ki‡Z G‡m dvI dvI RxebUv bó Kijv? GLb †K LvIqv‡e †Zvgvi Av¤§v‡i?";
        btnLeftText.text = "†K Zzwg? AvËv?";
        btnRightText.text = "Rvwbbv!";
    }
    private void AskQuestion_02()
    {
        //Don't know!? Na jene keno ascho emn andolone, tomara jibonew kicchu korte parbana. Do you want to know Where are you?
        currentQuestion = 2;
        dialogueBox.SetActive(true);
        msgText.text = "Rv‡bvbv!! bv †R‡b †Kb Gm‡Qv Ggb Av‡›`vj‡b, †Zvgvi Rxe‡bI wKQz Ki‡Z cvievbv nvwmbv Avcvi| Zzwg Rvb‡Z PvI Zzwg †Kv_vq?";
        btnLeftText.text = "Avwg †Kv_vq?";
        btnRightText.text = "Zzwg `vjvj?";
    }
    private void AskQuestion_03()
    {
        //Well, I'm a teacher, My Uncle is a Politician From AwamiLeague. Leave this kidding protest, lets go with me, let me show you our wealth. 
        currentQuestion = 3;
        dialogueBox.SetActive(true);
        msgText.text = "Av”Qv, Avwg GKRb wk¶K, Avgvi gvgv AvIqvgx jx‡Mi †bZv| wgwQj †`Lv jvM‡ebv, Pj †Nv‡i Avwm, Av‡mv Avgv‡`i wgicy‡ii ab-m¤ú` †`wL‡q †`B|";
        btnLeftText.text = "fvB‡`i mvnv‡h¨i Ki‡ev|";
        btnRightText.text = "P‡jb †`wL!";

    }
    private void AskQuestion_04()
    {
        //Lets fly with me, We have countless property here. Look! There are some catro league, they are saving our country from rajakar. lets see what they are doing there. 
        currentQuestion = 4;
        dialogueBox.SetActive(true);
        msgText.text = "Avgvi mv‡_ D‡o Pj, GLv‡b Avgv‡`i AmsL¨ m¤úwË Av‡Q| †`‡Lv! wKQz QvÎjxM Av‡Q, Zviv cvwK¯—vwb mš—vb I ivRvKvi‡`i †_‡K Avgv‡`i †`k‡K i¶v Ki‡Q| P‡jv †`wL Zviv wK Ki‡Q|";
        btnLeftText.text = "P‡jv †`wL!";
        btnRightText.text = "I‡`i‡K miv‡ev";
    }

    private void AskQuestion_05()
    {
        //How you can help, ok let me help you, I'm one of you. 
        currentQuestion = 5;
        dialogueBox.SetActive(true);
        msgText.text = "Zzwg wKfv‡e mvnvh¨ Ki‡Z cvi‡e? wVK Av‡Q, Avwg †`Lvw”Q| Avwg †Zvgv‡`i g‡a¨ GKRb|";
        btnLeftText.text = "jvM‡e bv";
        btnRightText.text = "ab¨ev`!";
    }
    private void AskQuestion_06()
    {
        //Well Well, This gol cokkor is not safe, anytime anyone from anyside can attack. I think you Should encourage them to leave immediatly
        currentQuestion = 6;
        dialogueBox.SetActive(true);
        msgText.text = "Av”Qv Av”Qv, GB †Mvj P°i GKUzI wbivc` bq, †h †Kvb w`K †_‡K †KD nvgjv Ki‡Z cv‡i| Avwg g‡b Kwi Zzwg Zv‡`i‡K DrmvwnZ Ki‡Z cv‡iv †hb Zviv ZvovZvwo P‡j hvq|";
        btnLeftText.text = "Pj Kwi!";
        btnRightText.text = "†KI hv‡ebv!";

    }
    private void AskQuestion_07()
    {
        //You are such a genz person, There are some student at BUBT campus near the stadium. Stack more than 5 hours By Catro Leage Team, Saving them is not our main priority, we should move toward Gonobobon for hasina apa
        currentQuestion = 7;
        dialogueBox.SetActive(true);
        msgText.text = "Zzwg GKRb †RbwR gvbyl‡i fvB, †÷wWqv‡gi cv‡k weBDwewU K¨v¤úv‡m wKQz QvÎ cvuP NÈv a‡i QvÎjx‡Mi `j Øviv nvgjv K‡i AvU‡K ivLv n‡q‡Q| Zv‡`i i¶v Kiv Avgv‡`i cÖavb j¶¨ bq, Avgv‡`i Mbfe‡bi w`‡K GwM‡q †h‡Z n‡e|";
        btnLeftText.text = "P‡jv MYfe‡b!";
        btnRightText.text = "fvB‡`i i¶v!";
    }


}
