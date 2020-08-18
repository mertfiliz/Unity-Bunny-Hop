using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour
{
    public static ReviveController ReviveCtrl;
    public static GameObject Checkpoint_Block;
    public Text Countdown;
    public Image Countdown_Background;

    void Awake()
    {
        ReviveCtrl = this;
    }

    public void RestartFromCheckpoint()
    {
        AdManager.AdManage.bannerView.Destroy();
        AdManager.AdManage.Show_RewardedAd();
    }

    public void Restart_Success()
    {
        AdManager.AdManage.bannerView.Destroy();
        GameOverController.End_Panel.SetActive(false);
        GenerateBlocks.game_over = false;
        Movement.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        CheckRevivePosition();

        CameraMovement.Camera_Fall_Stop = false;

        GameObject.Find("Game_Sound").GetComponent<AudioSource>().Play();

        StartCoroutine(RestartCountdown());
    }

    public void Restart_Failed()
    {
        AdManager.AdManage.Show_Banner();
        GameOverController.instance.GameEnded();
    }

    public void CheckRevivePosition()
    {
        int i = 0;
        bool block_safe = false;

        Checkpoint_Block = GenerateBlocks.BlockList[i].gameObject;        
        
        if(Checkpoint_Block.gameObject.tag == "largeblock" ||
           Checkpoint_Block.gameObject.tag == "midblock" || 
           Checkpoint_Block.gameObject.tag == "smallblock")
        {
            while (!block_safe)
            {
                if (!Checkpoint_Block.transform.GetChild(0).GetComponent<Image>())
                {
                    Debug.Log("Block has no image");
                    block_safe = true;
                    break;
                }
                else
                {
                    i++;
                    Checkpoint_Block = GenerateBlocks.BlockList[i];
                    block_safe = false;
                }               
            }
            Movement.Player.transform.localPosition = new Vector2(Checkpoint_Block.transform.localPosition.x, Checkpoint_Block.transform.localPosition.y + 200);
        }
        else
        {
            while (Checkpoint_Block.gameObject.tag != "largeblock" &&
                   Checkpoint_Block.gameObject.tag != "midblock" &&
                   Checkpoint_Block.gameObject.tag != "smallblock")
            {
                i++;
                Checkpoint_Block = GenerateBlocks.BlockList[i];
            }
            Movement.Player.transform.localPosition = new Vector2(Checkpoint_Block.transform.localPosition.x, Checkpoint_Block.transform.localPosition.y + 200);
        }       
    }

    IEnumerator RestartCountdown()
    {
        AdManager.AdManage.bannerView.Destroy();
        Movement.StartGameCountdownEnded = false;
        float ct = 3f;

        Countdown_Background.gameObject.SetActive(true);
        Countdown.enabled = true;
        Countdown.text = "Welcome Back!";

        while (ct >= -0.5f)
        {
            yield return new WaitForSeconds(0.5f);
            Countdown.text = ct.ToString();
            ct--;
        }

        Countdown.text = "Go!";
        Movement.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Movement.StartGameCountdownEnded = true;
        Movement.StartGame = true;
        yield return new WaitForSeconds(0.5f);
        Countdown.enabled = false;
        Countdown_Background.gameObject.SetActive(false);
        yield return null;
    }

    public void NoRevive()
    {
        AdManager.AdManage.bannerView.Destroy();

        GameOverController.instance.StopCoroutine(GameOverController.instance.RestartTimerCountdown());
        GameOverController.Revive_Panel.SetActive(false);
        GameOverController.GameOver_Panel.SetActive(true);

        string Highscore_Current = Movement.current_score.ToString("F0");
        GameOverController.Score_UI.text = Highscore_Current;   
        
        int highscore_current = int.Parse(Highscore_Current);
        
        PlayerData loadedData = SaveLoad.LoadPlayer();
        string Highscore_Total_str = loadedData.highscore.ToString();

        int Highscore_Total = int.Parse(Highscore_Total_str);

        Debug.Log(highscore_current + "--" + Highscore_Total);

        if (highscore_current > Highscore_Total)
        {
            PlayerData.HighPoint.highscore_total = highscore_current;

            PlayerData saveData = new PlayerData();
            saveData.highscore = PlayerData.HighPoint.highscore_total;
            SaveLoad.SavePlayer(saveData);
        }

        GameOverController.instance.Calculate_Coin();
    }
}
