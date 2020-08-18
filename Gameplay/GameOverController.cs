using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public static GameObject End_Panel, Revive_Panel, GameOver_Panel;
    public static GameOverController instance;
    public static GameObject Checkpoint_Block;
    public static GameObject ReviveSlider;
    public static float restart_timer;
    public static bool game_over = false;
    public static Text Score_UI, Coin_UI;
    public Text HighScore_Text;
    private bool restart_enabled;
    private bool internet_connection;

    public int total_coins = 0;

    void Awake()
    {
        instance = this;   
        End_Panel = GameObject.Find("End_Panel");
        Revive_Panel = GameObject.Find("Revive_UI");
        GameOver_Panel = GameObject.Find("Game_Over");
        ReviveSlider = GameObject.Find("ReviveSlider");
        Score_UI = GameObject.Find("Score_UI").GetComponent<Text>();
        Coin_UI = GameObject.Find("Coin_UI").GetComponent<Text>();

        End_Panel.SetActive(false);
        GameOver_Panel.SetActive(false);

        internet_connection = false;
        restart_enabled = true;
    }  

    public static void GameOver()
    {
        Movement.Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        End_Panel.SetActive(true);
        GameOver_Panel.SetActive(false);
        Revive_Panel.SetActive(true);

        AdManager.AdManage.Show_Banner();
                
        game_over = true;
        restart_timer = 5f;

        if(instance.internet_connection)
        {
            if (instance.restart_enabled && AdManager.AdManage.rewardedAd.IsLoaded())
            {
                instance.StartCoroutine(instance.RestartTimerCountdown());
            }
            else
            {
                instance.GameEnded();
            }
        }       
        else
        {
            instance.GameEnded();
        }       
    }

    public void GameEnded()
    {
        AdManager.AdManage.bannerView.Destroy();

        Revive_Panel.SetActive(false);
        Calculate_Coin();

        string Highscore_Current = Movement.current_score.ToString("F0");
        Score_UI.text = Highscore_Current;

        // Get current score as int.
        int highscore_current = int.Parse(Highscore_Current);

        PlayerData loadedData = SaveLoad.LoadPlayer();
        string Highscore_Total_str = loadedData.highscore.ToString();

        // Get highscore as int.
        int Highscore_Total = int.Parse(Highscore_Total_str);

        HighScore_Text.text = "HIGHSCORE";

        // Check if highscore is bigger than current score.
        if (highscore_current > Highscore_Total)
        {
            HighScore_Text.text = "NEW HIGHSCORE";
            // Save new highscore.
            PlayerData.HighPoint.highscore_total = highscore_current;
            PlayerData saveData = new PlayerData();
            saveData.highscore = PlayerData.HighPoint.highscore_total;
            SaveLoad.SavePlayer(saveData);
        }

        GameOver_Panel.SetActive(true);
        game_over = false;
    }

    void Update()
    {
        if (game_over)
        {
            ReviveSlider.GetComponent<Slider>().value = restart_timer;
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            internet_connection = false;
        }
        else
        {
            internet_connection = true;
        }
    }

    public void Calculate_Coin()
    {
        string Highscore_Current = Movement.current_score.ToString("F0");
        int highscore_current = int.Parse(Highscore_Current);       
        int carrot_count = Carrot_Collector.Carrot_C.carrot_ct;

        int coin = (highscore_current / 500) + (carrot_count * 2);
        Coin_UI.text = coin.ToString();

        PlayerDataCoins loadedData = SaveLoadCoins.LoadPlayer();
        total_coins = loadedData.Coins;

        total_coins += coin;

        PlayerDataCoins saveDataCoins = new PlayerDataCoins();
        saveDataCoins.Coins = total_coins;
        SaveLoadCoins.SavePlayer(saveDataCoins);
    }
    
    public IEnumerator RestartTimerCountdown()
    {
        restart_enabled = false;

        while (restart_timer >= 0f)
        {
            yield return new WaitForSeconds(0.01f);
            restart_timer -= 0.01f;

        }

        GameEnded();
        yield return null;
        game_over = false;
    }
}
