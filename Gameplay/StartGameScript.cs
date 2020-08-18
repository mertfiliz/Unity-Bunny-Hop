using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour
{
    public static StartGameScript ins;
    public Text Countdown;
    public Image Count_Background;
    public static GameObject Top_Panel;
    public bool Coroutine_Started = false;

    void Start()
    {
        ins = this;
        Top_Panel = GameObject.Find("Top_Panel");
        Top_Panel.SetActive(false);
        Countdown.enabled = true;
    }

    public void TapToStart()
    {
        // If coroutine has not started.
        if(!Coroutine_Started)
        {
            StartCoroutine(StartCountdown());
        }      
    }

    public IEnumerator StartCountdown()
    {
        Coroutine_Started = true;
        float ct = 3f;

        Countdown.text = "Get Ready!";

        while(ct >= -0.5f)
        {
            yield return new WaitForSeconds(0.75f);
            Countdown.text = ct.ToString();
            ct--;
        }        

        Movement.StartGameCountdownEnded = true;
        yield return new WaitForSeconds(2f);
        Count_Background.gameObject.SetActive(false);
        Coroutine_Started = false;
        yield return null;        
    }
}
