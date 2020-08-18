using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public static RestartGame RG;
    void Awake()
    {
        RG = this;
    }
    public void Restart()
    {
        AdManager.AdManage.bannerView.Destroy();
        AdManager.AdManage.Show_Interstitial();
        SceneManager.LoadScene("Game");
        GameOverController.End_Panel.SetActive(false);                        
        Movement.StartGame = false;
        Movement.StartGameCountdownEnded = false;
        Movement.current_score = 0;
        Movement.temp_score = 0;        
    }
}
