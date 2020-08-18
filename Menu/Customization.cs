using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    public static Customization Custom;
    public int total_coins, total_redorbs, total_blueorbs;

    private int upgrade_1_level, upgrade_1_cost, upgrade_2_level, upgrade_2_cost, upgrade_3_level, upgrade_3_cost;
    private float Score_Multiplier;
    public int Carrot_Points;
    public int Carrot_Spawn_Rate;
    public GameObject Upgrade_1_Sprite_Current, Upgrade_2_Sprite_Current, Upgrade_3_Sprite_Current;
    public Sprite[] Upgrade_Sprites;
    public Text Upgrade_1_Info_Text, Upgrade_1_Current_Bonus_Text, Upgrade_1_Cost_Text;
    public Text Upgrade_2_Info_Text, Upgrade_2_Current_Bonus_Text, Upgrade_2_Cost_Text;
    public Text Upgrade_3_Info_Text, Upgrade_3_Current_Bonus_Text, Upgrade_3_Cost_Text;

    public bool hat_1_locked, hat_2_locked, hat_3_locked, hat_4_locked, hat_5_locked;
    public bool body_1_locked, body_2_locked, body_3_locked, body_4_locked, body_5_locked;
    public bool hands_1_locked, hands_2_locked, hands_3_locked, hands_4_locked, hands_5_locked;
    public bool legs_1_locked, legs_2_locked, legs_3_locked, legs_4_locked, legs_5_locked;
    public bool trail_1_locked, trail_2_locked, trail_3_locked, trail_4_locked, trail_5_locked, trail_6_locked, trail_7_locked, trail_8_locked;

    public int hat_selection, body_selection, hands_selection, legs_selection, trail_selection;

    public GameObject Hat_1, Hat_2, Hat_3, Hat_4, Hat_5;
    public GameObject Body_1, Body_2, Body_3, Body_4, Body_5;
    public GameObject Hands_1, Hands_2, Hands_3, Hands_4, Hands_5;
    public GameObject Legs_1, Legs_2, Legs_3, Legs_4, Legs_5;
    public GameObject Trail_1, Trail_2, Trail_3, Trail_4, Trail_5, Trail_6, Trail_7, Trail_8;   

    public GameObject Buy_Screen, Chest_Screen;

    public GameObject Chest;
    public Sprite Chest_Img_Closed, Chest_Img_Opened;
    public Text Chest_Tap_Text, Chest_Item_Text;
    public GameObject Item_Img;
    public Sprite[] Chest_Items_Img; 
    public GameObject ItemChest, TapOpenChest;
    public GameObject Chest_Close_Button;
    private string chest_item;
    private int chest_item_count;

    public Text CoinText, RedOrbText, BlueOrbText;
    public int item_no, item_value;
    public GameObject Item;
    public ScrollRect CustomizeScroll;
    public AudioSource ChestOpen;

    void Start()
    {
        Custom = this;
        Buy_Screen.SetActive(false);
        Chest_Screen.SetActive(false);
        LoadCoins();
        LoadOrbs();
        LoadCosts();
        LoadUpgrades();
        LoadLocked();
        LoadCustomization();

        CustomizeScroll.verticalNormalizedPosition = 1;
    }

    public void LoadCoins()
    {
        PlayerDataCoins loadedData = SaveLoadCoins.LoadPlayer();
        total_coins = loadedData.Coins;        
        CoinText.text = total_coins.ToString();
    }

    public void LoadOrbs()
    {
        PlayerDataOrbs loadedData = SaveLoadOrbs.LoadPlayer();
        total_redorbs = loadedData.Red_Orbs;
        total_blueorbs = loadedData.Blue_Orbs;
        RedOrbText.text = total_redorbs.ToString();
        BlueOrbText.text = total_blueorbs.ToString();
    }
    
    public void LoadCosts()
    {
        // Hats Costs
        Hat_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hat_2_cost.ToString();
        Hat_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hat_3_cost.ToString();
        Hat_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hat_4_cost.ToString();
        Hat_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hat_5_cost.ToString();

        // Head Costs
        Body_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.body_2_cost.ToString();
        Body_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.body_3_cost.ToString();
        Body_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.body_4_cost.ToString();
        Body_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.body_5_cost.ToString();

        // Hands Costs
        Hands_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hands_2_cost.ToString();
        Hands_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hands_3_cost.ToString();
        Hands_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hands_4_cost.ToString();
        Hands_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.hands_5_cost.ToString();

        // Legs Costs
        Legs_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.legs_2_cost.ToString();
        Legs_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.legs_3_cost.ToString();
        Legs_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.legs_4_cost.ToString();
        Legs_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.legs_5_cost.ToString();

        // Trail Costs
        Trail_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.trail_2_cost.ToString();
        Trail_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.trail_3_cost.ToString();
        Trail_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.trail_4_cost.ToString();
        Trail_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.trail_5_cost.ToString();
        Trail_6.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.trail_6_cost.ToString();
        Trail_7.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.trail_7_cost.ToString();
        Trail_8.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = PlayerDataCustomize.trail_8_cost.ToString();
    }

    public void LoadUpgrades()
    {
        PlayerDataUpgrade loadedData = SaveLoadUpgrade.LoadPlayer();

        // UPGRADE 1
        Score_Multiplier = loadedData.Score_Multiplier;  
        upgrade_1_level = loadedData.upgrade_1_level;        
        upgrade_1_cost = loadedData.upgrade_1_cost_base;

        Upgrade_1_Info_Text.text = "Increases the Score Multiplier +0.1";
        Upgrade_1_Current_Bonus_Text.text = "Current Bonus: " + Score_Multiplier;

        if (upgrade_1_level < Upgrade_Sprites.Length - 1)
        {
            Upgrade_1_Cost_Text.text = upgrade_1_cost.ToString();
        }
        else
        {
            Upgrade_1_Cost_Text.text = "MAX";
        }

        // Set bar sprite.
        Upgrade_1_Sprite_Current.GetComponent<Image>().sprite = Upgrade_Sprites[upgrade_1_level];


        // UPGRADE 2
        Carrot_Points = loadedData.Carrot_Points;
        upgrade_2_level = loadedData.upgrade_2_level;
        upgrade_2_cost = loadedData.upgrade_2_cost_base;

        Upgrade_2_Info_Text.text = "Each carrot adds additional +100 points";
        Upgrade_2_Current_Bonus_Text.text = "Current Bonus: " + Carrot_Points;

        if (upgrade_2_level < Upgrade_Sprites.Length - 1)
        {
            Upgrade_2_Cost_Text.text = upgrade_2_cost.ToString();
        }
        else
        {
            Upgrade_2_Cost_Text.text = "MAX";
        }

        // Set bar sprite.
        Upgrade_2_Sprite_Current.GetComponent<Image>().sprite = Upgrade_Sprites[upgrade_2_level];


        // UPGRADE 3
        Carrot_Spawn_Rate = loadedData.Carrot_Spawn_Rate;
        upgrade_3_level = loadedData.upgrade_3_level;
        upgrade_3_cost = loadedData.upgrade_3_cost_base;

        Upgrade_3_Info_Text.text = "Increases the Carrot Spawn Rate +%2";
        Upgrade_3_Current_Bonus_Text.text = "Current Bonus: +%" + Carrot_Spawn_Rate;

        if (upgrade_3_level < Upgrade_Sprites.Length - 1)
        {
            Upgrade_3_Cost_Text.text = upgrade_3_cost.ToString();
        }
        else
        {
            Upgrade_3_Cost_Text.text = "MAX";
        }

        // Set bar sprite.
        Upgrade_3_Sprite_Current.GetComponent<Image>().sprite = Upgrade_Sprites[upgrade_3_level];


    }

    public void LoadLocked()
    {
        PlayerDataLocked loadedData = SaveLoadLocked.LoadPlayer();

        hat_2_locked = loadedData.hat_2_L;
        hat_3_locked = loadedData.hat_3_L;
        hat_4_locked = loadedData.hat_4_L;
        hat_5_locked = loadedData.hat_5_L;

        body_2_locked = loadedData.body_2_L;
        body_3_locked = loadedData.body_3_L;
        body_4_locked = loadedData.body_4_L;
        body_5_locked = loadedData.body_5_L;

        hands_2_locked = loadedData.hands_2_L;
        hands_3_locked = loadedData.hands_3_L;
        hands_4_locked = loadedData.hands_4_L;
        hands_5_locked = loadedData.hands_5_L;

        legs_2_locked = loadedData.legs_2_L;
        legs_3_locked = loadedData.legs_3_L;
        legs_4_locked = loadedData.legs_4_L;
        legs_5_locked = loadedData.legs_5_L;

        trail_2_locked = loadedData.trail_2_L;
        trail_3_locked = loadedData.trail_3_L;
        trail_4_locked = loadedData.trail_4_L;
        trail_5_locked = loadedData.trail_5_L;
        trail_6_locked = loadedData.trail_6_L;
        trail_7_locked = loadedData.trail_7_L;
        trail_8_locked = loadedData.trail_8_L;

        // HATS LOCKED
        if (hat_2_locked)
        {
            Hat_2.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hat_2.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hat_2.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hat_2.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (hat_3_locked)
        {
            Hat_3.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hat_3.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hat_3.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hat_3.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (hat_4_locked)
        {
            Hat_4.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hat_4.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hat_4.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hat_4.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (hat_5_locked)
        {
            Hat_5.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hat_5.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hat_5.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hat_5.transform.Find("Coins").gameObject.SetActive(false);
        }

        // BODY LOCKED
        if (body_2_locked)
        {
            Body_2.transform.Find("Lock_Border").gameObject.SetActive(true);
            Body_2.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Body_2.transform.Find("Lock_Border").gameObject.SetActive(false);
            Body_2.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (body_3_locked)
        {
            Body_3.transform.Find("Lock_Border").gameObject.SetActive(true);
            Body_3.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Body_3.transform.Find("Lock_Border").gameObject.SetActive(false);
            Body_3.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (body_4_locked)
        {
            Body_4.transform.Find("Lock_Border").gameObject.SetActive(true);
            Body_4.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Body_4.transform.Find("Lock_Border").gameObject.SetActive(false);
            Body_4.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (body_5_locked)
        {
            Body_5.transform.Find("Lock_Border").gameObject.SetActive(true);
            Body_5.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Body_5.transform.Find("Lock_Border").gameObject.SetActive(false);
            Body_5.transform.Find("Coins").gameObject.SetActive(false);
        }

        // HANDS LOCKED
        if (hands_2_locked)
        {
            Hands_2.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hands_2.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hands_2.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hands_2.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (hands_3_locked)
        {
            Hands_3.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hands_3.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hands_3.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hands_3.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (hands_4_locked)
        {
            Hands_4.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hands_4.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hands_4.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hands_4.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (hands_5_locked)
        {
            Hands_5.transform.Find("Lock_Border").gameObject.SetActive(true);
            Hands_5.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Hands_5.transform.Find("Lock_Border").gameObject.SetActive(false);
            Hands_5.transform.Find("Coins").gameObject.SetActive(false);
        }

        // LEGS LOCKED
        if (legs_2_locked)
        {
            Legs_2.transform.Find("Lock_Border").gameObject.SetActive(true);
            Legs_2.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Legs_2.transform.Find("Lock_Border").gameObject.SetActive(false);
            Legs_2.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (legs_3_locked)
        {
            Legs_3.transform.Find("Lock_Border").gameObject.SetActive(true);
            Legs_3.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Legs_3.transform.Find("Lock_Border").gameObject.SetActive(false);
            Legs_3.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (legs_4_locked)
        {
            Legs_4.transform.Find("Lock_Border").gameObject.SetActive(true);
            Legs_4.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Legs_4.transform.Find("Lock_Border").gameObject.SetActive(false);
            Legs_4.transform.Find("Coins").gameObject.SetActive(false);
        }
        if (legs_5_locked)
        {
            Legs_5.transform.Find("Lock_Border").gameObject.SetActive(true);
            Legs_5.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Legs_5.transform.Find("Lock_Border").gameObject.SetActive(false);
            Legs_5.transform.Find("Coins").gameObject.SetActive(false);
        }

        // TRAIL LOCKED
        if (trail_2_locked)
        {
            Trail_2.transform.Find("Lock_Border").gameObject.SetActive(true);
            Trail_2.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Trail_2.transform.Find("Lock_Border").gameObject.SetActive(false);
            Trail_2.transform.Find("Coins").gameObject.SetActive(false);
        }

        if (trail_3_locked)
        {
            Trail_3.transform.Find("Lock_Border").gameObject.SetActive(true);
            Trail_3.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Trail_3.transform.Find("Lock_Border").gameObject.SetActive(false);
            Trail_3.transform.Find("Coins").gameObject.SetActive(false);
        }

        if (trail_4_locked)
        {
            Trail_4.transform.Find("Lock_Border").gameObject.SetActive(true);
            Trail_4.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Trail_4.transform.Find("Lock_Border").gameObject.SetActive(false);
            Trail_4.transform.Find("Coins").gameObject.SetActive(false);
        }

        if (trail_5_locked)
        {
            Trail_5.transform.Find("Lock_Border").gameObject.SetActive(true);
            Trail_5.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Trail_5.transform.Find("Lock_Border").gameObject.SetActive(false);
            Trail_5.transform.Find("Coins").gameObject.SetActive(false);
        }

        if (trail_6_locked)
        {
            Trail_6.transform.Find("Lock_Border").gameObject.SetActive(true);
            Trail_6.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Trail_6.transform.Find("Lock_Border").gameObject.SetActive(false);
            Trail_6.transform.Find("Coins").gameObject.SetActive(false);
        }

        if (trail_7_locked)
        {
            Trail_7.transform.Find("Lock_Border").gameObject.SetActive(true);
            Trail_7.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Trail_7.transform.Find("Lock_Border").gameObject.SetActive(false);
            Trail_7.transform.Find("Coins").gameObject.SetActive(false);
        }

        if (trail_8_locked)
        {
            Trail_8.transform.Find("Lock_Border").gameObject.SetActive(true);
            Trail_8.transform.Find("Coins").gameObject.SetActive(true);
        }
        else
        {
            Trail_8.transform.Find("Lock_Border").gameObject.SetActive(false);
            Trail_8.transform.Find("Coins").gameObject.SetActive(false);
        }
    }

    public void LoadCustomization()
    {
        PlayerDataCustomize loadedData = SaveLoadCustomize.LoadPlayer();
        
        hat_selection = loadedData.hat_selection;
        body_selection = loadedData.body_selection;
        hands_selection = loadedData.hands_selection;
        legs_selection = loadedData.legs_selection;
        trail_selection = loadedData.trail_selection;

        LoadHatDetails(hat_selection);
        LoadBodyDetails(body_selection);
        LoadHandsDetails(hands_selection);
        LoadLegsDetails(legs_selection);
        LoadTrailDetails(trail_selection);
    }

    public void LoadHatDetails(int hat_no)
    {
        if (hat_no == 1)
        {
            Hat_1.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hat_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hat_no == 2)
        {
            Hat_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_2.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hat_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hat_no == 3)
        {
            Hat_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_3.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hat_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hat_no == 4)
        {
            Hat_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_4.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hat_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hat_no == 5)
        {
            Hat_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hat_5.transform.Find("Tick_Selected").gameObject.SetActive(true);
        }
    }

    public void LoadBodyDetails(int body_no)
    {
        if (body_no == 1)
        {
            Body_1.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Body_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (body_no == 2)
        {
            Body_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_2.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Body_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (body_no == 3)
        {
            Body_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_3.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Body_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (body_no == 4)
        {
            Body_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_4.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Body_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (body_no == 5)
        {
            Body_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Body_5.transform.Find("Tick_Selected").gameObject.SetActive(true);
        }
    }

    public void LoadHandsDetails(int hands_no)
    {
        if (hands_no == 1)
        {
            Hands_1.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hands_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hands_no == 2)
        {
            Hands_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_2.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hands_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hands_no == 3)
        {
            Hands_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_3.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hands_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hands_no == 4)
        {
            Hands_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_4.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Hands_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (hands_no == 5)
        {
            Hands_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Hands_5.transform.Find("Tick_Selected").gameObject.SetActive(true);
        }
    }

    public void LoadLegsDetails(int legs_no)
    {
        if (legs_no == 1)
        {
            Legs_1.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Legs_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (legs_no == 2)
        {
            Legs_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_2.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Legs_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (legs_no == 3)
        {
            Legs_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_3.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Legs_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (legs_no == 4)
        {
            Legs_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_4.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Legs_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (legs_no == 5)
        {
            Legs_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Legs_5.transform.Find("Tick_Selected").gameObject.SetActive(true);
        }
    }

    public void LoadTrailDetails(int trail_no)
    {
        if (trail_no == 1)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (trail_no == 2)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (trail_no == 3)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (trail_no == 4)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (trail_no == 5)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (trail_no == 6)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (trail_no == 7)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(true);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(false);
        }
        else if (trail_no == 8)
        {
            Trail_1.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_2.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_3.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_4.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_5.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_6.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_7.transform.Find("Tick_Selected").gameObject.SetActive(false);
            Trail_8.transform.Find("Tick_Selected").gameObject.SetActive(true);
        }
    }

    public void Open_Chest_Screen()
    {
        Chest.gameObject.GetComponent<Image>().sprite = Chest_Img_Closed;
        
        int chest_costs = PlayerDataCustomize.chest_cost;
        if(total_coins >= chest_costs)
        {
            Chest_Screen.SetActive(true);
        }
    }

    public void Open_Chest()
    {      
        GameObject.Find("Sound_Lobby").GetComponent<AudioSource>().volume /= 8;       

        Chest_Close_Button.gameObject.SetActive(false);
        Chest_Tap_Text.gameObject.SetActive(false);
        TapOpenChest.gameObject.SetActive(false);
        int chest_costs = PlayerDataCustomize.chest_cost;
        total_coins -= chest_costs;
        SaveCustomization();
        Chest.gameObject.GetComponent<Image>().sprite = Chest_Img_Opened;
        Chest.GetComponent<Animator>().SetTrigger("open_chest");
        SetChestItem();
        Invoke("GetChestItem", .75f);
    }

    public void SetChestItem()
    {
        int rnd = Random.Range(1, 101); // 1,2, ... ,100

        // 20% Coin
        if(rnd > 80)
        {
            chest_item = "Coin";
            Item_Img.GetComponent<Image>().sprite = Chest_Items_Img[0];
            int rnd_coin = Random.Range(1, 1001); // 1,2,3, 1000

            // 0.1% - 100 chest
            if(rnd_coin == 1000)
            {
                chest_item_count = 25000; 
            }
            // 20% - (3-4 chest)
            else if(rnd_coin > 800)
            {
                chest_item_count = Random.Range(700, 1000);
            }
            // 40% - 1 chest
            else if (rnd_coin > 400)
            {
                chest_item_count = Random.Range(180, 380);
            }
            else if (rnd_coin >= 1)
            {
                chest_item_count = Random.Range(20, 150);
            }
        }

        // 40% Red Orb
        else if (rnd > 40)
        {
            chest_item = "Red Orb";
            Item_Img.GetComponent<Image>().sprite = Chest_Items_Img[1];
            int rnd_redorb = Random.Range(1, 101); // 1,2,3, 100

            // 3%
            if(rnd_redorb > 98)
            {
                chest_item_count = Random.Range(280, 360);
            }
            // 10%
            else if (rnd_redorb > 88)
            {
                chest_item_count = Random.Range(120, 160);
            }
            // 30%
            else if (rnd_redorb > 58)
            {
                chest_item_count = Random.Range(40, 60);
            }
            else if (rnd_redorb >= 1)
            {
                chest_item_count = Random.Range(10, 22);
            }
        }

        // 40% Blue Orb
        else if (rnd >= 1)
        {
            chest_item = "Blue Orb";
            Item_Img.GetComponent<Image>().sprite = Chest_Items_Img[2];
            int rnd_blueorb = Random.Range(1, 101); // 1,2,3, 100

            // 3%
            if (rnd_blueorb > 98)
            {
                chest_item_count = Random.Range(280, 360);
            }
            // 10%
            else if (rnd_blueorb > 88)
            {
                chest_item_count = Random.Range(120, 160);
            }
            // 30%
            else if (rnd_blueorb > 58)
            {
                chest_item_count = Random.Range(40, 60);
            }
            else if (rnd_blueorb >= 1)
            {
                chest_item_count = Random.Range(10, 22);
            }
        }        
        Chest_Item_Text.text = chest_item_count + " " + chest_item;
    }

    public void GetChestItem()
    {
        Chest_Close_Button.SetActive(true);
        ItemChest.gameObject.SetActive(true);
        ChestOpen.Play();
        GameObject.Find("Sound_Lobby").GetComponent<AudioSource>().volume *= 8;

        if (chest_item == "Coin")
        {
            total_coins += chest_item_count;
        }
        else if(chest_item == "Red Orb")
        {
            total_redorbs += chest_item_count;
        }
        else if (chest_item == "Blue Orb")
        {
            total_blueorbs += chest_item_count;
        }

        SaveCustomization();
    }

    public void Close_Chest()
    {
        Chest_Tap_Text.gameObject.SetActive(true);
        TapOpenChest.gameObject.SetActive(true);
        ItemChest.gameObject.SetActive(false);
        Chest_Screen.SetActive(false);
    }
    
    public void Hat_1_Select()
    {
        hat_selection = 1;
        LoadHatDetails(hat_selection);
    }
    public void Hat_2_Select()
    {
        item_no = 2;    
        
        if (hat_2_locked)
        {    
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hat_2.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hat_2.transform.Find("Hat_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hat_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if(total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            hat_selection = 2;
            LoadHatDetails(hat_selection);
        }
    }
    public void Hat_3_Select()
    {
        item_no = 3;

        if (hat_3_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hat_3.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hat_3.transform.Find("Hat_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hat_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            hat_selection = 3;
            LoadHatDetails(hat_selection);
        }
    }
    public void Hat_4_Select()
    {
        item_no = 4;

        if (hat_4_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hat_4.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hat_4.transform.Find("Hat_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hat_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            hat_selection = 4;
            LoadHatDetails(hat_selection);
        }
    }
    public void Hat_5_Select()
    {
        item_no = 5;

        if (hat_5_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hat_5.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hat_5.transform.Find("Hat_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hat_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            hat_selection = 5;
            LoadHatDetails(hat_selection);
        }
    }

    public void Body_1_Select()
    {
        body_selection = 1;
        LoadBodyDetails(body_selection);
    }
    public void Body_2_Select()
    {
        item_no = 6;
        if (body_2_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Body_2.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Body_2.transform.Find("Body_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Body_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            body_selection = 2;
            LoadBodyDetails(body_selection);
        }
    }
    public void Body_3_Select()
    {
        item_no = 7;
        if (body_3_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Body_3.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Body_3.transform.Find("Body_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Body_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            body_selection = 3;
            LoadBodyDetails(body_selection);
        }
    }
    public void Body_4_Select()
    {
        item_no = 8;
        if (body_4_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Body_4.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Body_4.transform.Find("Body_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Body_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            body_selection = 4;
            LoadBodyDetails(body_selection);
        }
    }
    public void Body_5_Select()
    {
        item_no = 9;
        if (body_5_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Body_5.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Body_5.transform.Find("Body_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Body_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            body_selection = 5;
            LoadBodyDetails(body_selection);
        }
    }

    public void Hands_1_Select()
    {
        hands_selection = 1;
        LoadHandsDetails(hands_selection);
    }
    public void Hands_2_Select()
    {
        item_no = 10;
        if (hands_2_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hands_2.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hands_2.transform.Find("Hands_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hands_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }

        }
        else
        {
            hands_selection = 2;
            LoadHandsDetails(hands_selection);
        }
    }
    public void Hands_3_Select()
    {
        item_no = 11;
        if (hands_3_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hands_3.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hands_3.transform.Find("Hands_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hands_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }

        }
        else
        {
            hands_selection = 3;
            LoadHandsDetails(hands_selection);
        }
    }
    public void Hands_4_Select()
    {
        item_no = 12;
        if (hands_4_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hands_4.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hands_4.transform.Find("Hands_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hands_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            hands_selection = 4;
            LoadHandsDetails(hands_selection);
        }
    }
    public void Hands_5_Select()
    {
        item_no = 13;
        if (hands_5_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Hands_5.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Hands_5.transform.Find("Hands_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Hands_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            hands_selection = 5;
            LoadHandsDetails(hands_selection);
        }
    }

    public void Legs_1_Select()
    {
        legs_selection = 1;
        LoadLegsDetails(legs_selection);
    }
    public void Legs_2_Select()
    {
        item_no = 14;
        if (legs_2_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Legs_2.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Legs_2.transform.Find("Legs_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Legs_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            legs_selection = 2;
            LoadLegsDetails(legs_selection);
        }
    }
    public void Legs_3_Select()
    {
        item_no = 15;
        if (legs_3_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Legs_3.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Legs_3.transform.Find("Legs_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Legs_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            legs_selection = 3;
            LoadLegsDetails(legs_selection);
        }
    }
    public void Legs_4_Select()
    {
        item_no = 16;
        if (legs_4_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Legs_4.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Legs_4.transform.Find("Legs_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Legs_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            legs_selection = 4;
            LoadLegsDetails(legs_selection);
        }
    }
    public void Legs_5_Select()
    {
        item_no = 17;
        if (legs_5_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Legs_5.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Legs_5.transform.Find("Legs_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Legs_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            legs_selection = 5;
            LoadLegsDetails(legs_selection);
        }
    }

    public void Trail_1_Select()
    {
        trail_selection = 1;
        LoadTrailDetails(trail_selection);
    }
    public void Trail_2_Select()
    {
        item_no = 18;
        if (trail_2_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Trail_2.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Trail_2.transform.Find("Trail_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Trail_2.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            trail_selection = 2;
            LoadTrailDetails(trail_selection);
        }
    }
    public void Trail_3_Select()
    {
        item_no = 19;
        if (trail_3_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Trail_3.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Trail_3.transform.Find("Trail_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Trail_3.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            trail_selection = 3;
            LoadTrailDetails(trail_selection);
        }
    }
    public void Trail_4_Select()
    {
        item_no = 20;
        if (trail_4_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Trail_4.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Trail_4.transform.Find("Trail_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Trail_4.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            trail_selection = 4;
            LoadTrailDetails(trail_selection);
        }
    }
    public void Trail_5_Select()
    {
        item_no = 21;
        if (trail_5_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Trail_5.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Trail_5.transform.Find("Trail_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Trail_5.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            trail_selection = 5;
            LoadTrailDetails(trail_selection);
        }
    }
    public void Trail_6_Select()
    {
        item_no = 22;
        if (trail_6_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Trail_6.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Trail_6.transform.Find("Trail_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Trail_6.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            trail_selection = 6;
            LoadTrailDetails(trail_selection);
        }
    }
    public void Trail_7_Select()
    {
        item_no = 23;
        if (trail_7_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Trail_7.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Trail_7.transform.Find("Trail_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Trail_7.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            trail_selection = 7;
            LoadTrailDetails(trail_selection);
        }
    }
    public void Trail_8_Select()
    {
        item_no = 24;
        if (trail_8_locked)
        {
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").GetComponent<Image>().sprite = Trail_8.GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Item_Border").transform.Find("Item").GetComponent<Image>().sprite = Trail_8.transform.Find("Trail_Image").GetComponent<Image>().sprite;
            Buy_Screen.transform.Find("Buy_Border").transform.Find("Golden_Border").transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text = Trail_8.transform.Find("Coins").transform.Find("Coin_Text").GetComponent<Text>().text;
            Buy_Screen.SetActive(true);

            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = false;
            Get_Value();
            if (total_redorbs >= item_value)
            {
                GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            trail_selection = 8;
            LoadTrailDetails(trail_selection);
        }
    }

    public void Get_Value()
    {
        if (item_no == 2)
        {
            Item = Hat_2;
            item_value = PlayerDataCustomize.hat_2_cost;
        }
        else if (item_no == 3)
        {
            Item = Hat_3;
            item_value = PlayerDataCustomize.hat_3_cost;
        }
        else if (item_no == 4)
        {
            Item = Hat_4;
            item_value = PlayerDataCustomize.hat_4_cost;
        }
        else if (item_no == 5)
        {
            Item = Hat_5;
            item_value = PlayerDataCustomize.hat_5_cost;
        }

        if (item_no == 6)
        {
            Item = Body_2;
            item_value = PlayerDataCustomize.body_2_cost;
        }
        else if (item_no == 7)
        {
            Item = Body_3;
            item_value = PlayerDataCustomize.body_3_cost;
        }
        else if (item_no == 8)
        {
            Item = Body_4;
            item_value = PlayerDataCustomize.body_4_cost;
        }
        else if (item_no == 9)
        {
            Item = Body_5;
            item_value = PlayerDataCustomize.body_5_cost;
        }

        if (item_no == 10)
        {
            Item = Hands_2;
            item_value = PlayerDataCustomize.hands_2_cost;
        }
        else if (item_no == 11)
        {
            Item = Hands_3;
            item_value = PlayerDataCustomize.hands_3_cost;
        }
        else if (item_no == 12)
        {
            Item = Hands_4;
            item_value = PlayerDataCustomize.hands_4_cost;
        }
        else if (item_no == 13)
        {
            Item = Hands_5;
            item_value = PlayerDataCustomize.hands_5_cost;
        }

        if (item_no == 14)
        {
            Item = Legs_2;
            item_value = PlayerDataCustomize.legs_2_cost;
        }
        else if (item_no == 15)
        {
            Item = Legs_3;
            item_value = PlayerDataCustomize.legs_3_cost;
        }
        else if (item_no == 16)
        {
            Item = Legs_4;
            item_value = PlayerDataCustomize.legs_4_cost;
        }
        else if (item_no == 17)
        {
            Item = Legs_5;
            item_value = PlayerDataCustomize.legs_5_cost;
        }

        if (item_no == 18)
        {
            Item = Trail_2;
            item_value = PlayerDataCustomize.trail_2_cost;
        }
        else if (item_no == 19)
        {
            Item = Trail_3;
            item_value = PlayerDataCustomize.trail_3_cost;
        }
        else if (item_no == 20)
        {
            Item = Trail_4;
            item_value = PlayerDataCustomize.trail_4_cost;
        }
        else if (item_no == 21)
        {
            Item = Trail_5;
            item_value = PlayerDataCustomize.trail_5_cost;
        }
        else if (item_no == 22)
        {
            Item = Trail_6;
            item_value = PlayerDataCustomize.trail_6_cost;
        }
        else if (item_no == 23)
        {
            Item = Trail_7;
            item_value = PlayerDataCustomize.trail_7_cost;
        }
        else if (item_no == 24)
        {
            Item = Trail_8;
            item_value = PlayerDataCustomize.trail_8_cost;
        }
    }

    public void Unlock(int item_no)
    {
        if (item_no == 2)
        {
            hat_selection = 2;
            hat_2_locked = false;
            LoadHatDetails(2);
        }
        else if (item_no == 3)
        {
            hat_selection = 3;
            hat_3_locked = false;
            LoadHatDetails(3);
        }
        else if (item_no == 4)
        {
            hat_selection = 4;
            hat_4_locked = false;
            LoadHatDetails(4);
        }
        else if (item_no == 5)
        {
            hat_selection = 5;
            hat_5_locked = false;
            LoadHatDetails(5);
        }

        if (item_no == 6)
        {
            body_selection = 2;
            body_2_locked = false;
            LoadBodyDetails(2);
        }
        else if (item_no == 7)
        {
            body_selection = 3;
            body_3_locked = false;
            LoadBodyDetails(3);
        }
        else if (item_no == 8)
        {
            body_selection = 4;
            body_4_locked = false;
            LoadBodyDetails(4);
        }
        else if (item_no == 9)
        {
            body_selection = 5;
            body_5_locked = false;
            LoadBodyDetails(5);
        }

        if (item_no == 10)
        {
            hands_selection = 2;
            hands_2_locked = false;
            LoadHandsDetails(2);
        }
        else if (item_no == 11)
        {
            hands_selection = 3;
            hands_3_locked = false;
            LoadHandsDetails(3);
        }
        else if (item_no == 12)
        {
            hands_selection = 4;
            hands_4_locked = false;
            LoadHandsDetails(4);
        }
        else if (item_no == 13)
        {
            hands_selection = 5;
            hands_5_locked = false;
            LoadHandsDetails(5);
        }

        if (item_no == 14)
        {
            legs_selection = 2;
            legs_2_locked = false;
            LoadLegsDetails(2);
        }
        else if (item_no == 15)
        {
            legs_selection = 3;
            legs_3_locked = false;
            LoadLegsDetails(3);
        }
        else if (item_no == 16)
        {
            legs_selection = 4;
            legs_4_locked = false;
            LoadLegsDetails(4);
        }
        else if (item_no == 17)
        {
            legs_selection = 5;
            legs_5_locked = false;
            LoadLegsDetails(5);
        }

        if (item_no == 18)
        {
            trail_selection = 2;
            trail_2_locked = false;
            LoadTrailDetails(2);
        }
        else if (item_no == 19)
        {
            trail_selection = 3;
            trail_3_locked = false;
            LoadTrailDetails(3);
        }
        else if (item_no == 20)
        {
            trail_selection = 4;
            trail_4_locked = false;
            LoadTrailDetails(4);
        }
        else if (item_no == 21)
        {
            trail_selection = 5;
            trail_5_locked = false;
            LoadTrailDetails(5);
        }
        else if (item_no == 22)
        {
            trail_selection = 6;
            trail_6_locked = false;
            LoadTrailDetails(6);
        }
        else if (item_no == 23)
        {
            trail_selection = 7;
            trail_7_locked = false;
            LoadTrailDetails(7);
        }
        else if (item_no == 24)
        {
            trail_selection = 8;
            trail_8_locked = false;
            LoadTrailDetails(8);
        }
    }

    public void Buy_Item()
    {
        Get_Value();
        
        if (total_redorbs >= item_value)
        {
            GameObject.Find("Golden_Border").GetComponent<Button>().interactable = true;
            total_redorbs -= item_value;
            Item.gameObject.transform.Find("Lock_Border").gameObject.SetActive(false);
            Item.gameObject.transform.Find("Coins").gameObject.SetActive(false);
            Unlock(item_no);
            GameObject.Find("Buy_Screen").gameObject.SetActive(false);
            SaveCustomization();
        }       
    }

    public void Upgrade_1_Increase()
    {
        if(upgrade_1_level < Upgrade_Sprites.Length-1)
        {
            if(total_blueorbs >= upgrade_1_cost)
            {
                total_blueorbs -= upgrade_1_cost;
                Score_Multiplier += 0.1f;
                upgrade_1_level++;
                upgrade_1_cost *= 2;
                Upgrade_1_Sprite_Current.GetComponent<Image>().sprite = Upgrade_Sprites[upgrade_1_level];
                SaveCustomization();
                LoadUpgrades();
            }           
        }
    }

    public void Upgrade_2_Increase()
    {
        if (upgrade_2_level < Upgrade_Sprites.Length - 1)
        {
            if (total_blueorbs >= upgrade_2_cost)
            {
                total_blueorbs -= upgrade_2_cost;
                Carrot_Points += 100;
                upgrade_2_level++;
                upgrade_2_cost *= 2;
                Upgrade_2_Sprite_Current.GetComponent<Image>().sprite = Upgrade_Sprites[upgrade_2_level];
                SaveCustomization();
                LoadUpgrades();
            }
        }
    }

    public void Upgrade_3_Increase()
    {
        if (upgrade_3_level < Upgrade_Sprites.Length - 1)
        {
            if (total_blueorbs >= upgrade_3_cost)
            {
                total_blueorbs -= upgrade_3_cost;
                Carrot_Spawn_Rate += 2;
                upgrade_3_level++;
                upgrade_3_cost *= 2;
                Upgrade_3_Sprite_Current.GetComponent<Image>().sprite = Upgrade_Sprites[upgrade_3_level];
                SaveCustomization();
                LoadUpgrades();
            }
        }
    }

    public void SaveCustomization()
    {
        // Save Coins
        PlayerDataCoins saveDataCoins = new PlayerDataCoins();
        saveDataCoins.Coins = total_coins;
        SaveLoadCoins.SavePlayer(saveDataCoins);

        // Save Orbs
        PlayerDataOrbs saveDataOrbs = new PlayerDataOrbs();
        saveDataOrbs.Red_Orbs = total_redorbs;
        saveDataOrbs.Blue_Orbs = total_blueorbs;
        SaveLoadOrbs.SavePlayer(saveDataOrbs);

        // Save Upgrades
        PlayerDataUpgrade saveDataUpgrade = new PlayerDataUpgrade();
        saveDataUpgrade.Score_Multiplier = Score_Multiplier;
        saveDataUpgrade.Carrot_Points = Carrot_Points;
        saveDataUpgrade.Carrot_Spawn_Rate = Carrot_Spawn_Rate;
        saveDataUpgrade.upgrade_1_level = upgrade_1_level;
        saveDataUpgrade.upgrade_1_cost_base = upgrade_1_cost;
        saveDataUpgrade.upgrade_2_level = upgrade_2_level;
        saveDataUpgrade.upgrade_2_cost_base = upgrade_2_cost;
        saveDataUpgrade.upgrade_3_level = upgrade_3_level;
        saveDataUpgrade.upgrade_3_cost_base = upgrade_3_cost;
        SaveLoadUpgrade.SavePlayer(saveDataUpgrade);

        // Update Text
        CoinText.text = total_coins.ToString();
        RedOrbText.text = total_redorbs.ToString();
        BlueOrbText.text = total_blueorbs.ToString();

        // Save Selection
        PlayerDataCustomize saveDataCustomize = new PlayerDataCustomize();        
        saveDataCustomize.hat_selection = hat_selection;
        saveDataCustomize.body_selection = body_selection;
        saveDataCustomize.hands_selection = hands_selection;
        saveDataCustomize.legs_selection = legs_selection;
        saveDataCustomize.trail_selection = trail_selection;
        SaveLoadCustomize.SavePlayer(saveDataCustomize);

        // Save Locked
        PlayerDataLocked saveDataLocked = new PlayerDataLocked();

        saveDataLocked.hat_2_L = hat_2_locked;
        saveDataLocked.hat_3_L = hat_3_locked;
        saveDataLocked.hat_4_L = hat_4_locked;
        saveDataLocked.hat_5_L = hat_5_locked;

        saveDataLocked.body_2_L = body_2_locked;
        saveDataLocked.body_3_L = body_3_locked;
        saveDataLocked.body_4_L = body_4_locked;
        saveDataLocked.body_5_L = body_5_locked;

        saveDataLocked.hands_2_L = hands_2_locked;
        saveDataLocked.hands_3_L = hands_3_locked;
        saveDataLocked.hands_4_L = hands_4_locked;
        saveDataLocked.hands_5_L = hands_5_locked;

        saveDataLocked.legs_2_L = legs_2_locked;
        saveDataLocked.legs_3_L = legs_3_locked;
        saveDataLocked.legs_4_L = legs_4_locked;
        saveDataLocked.legs_5_L = legs_5_locked;

        saveDataLocked.trail_2_L = trail_2_locked;
        saveDataLocked.trail_3_L = trail_3_locked;
        saveDataLocked.trail_4_L = trail_4_locked;
        saveDataLocked.trail_5_L = trail_5_locked;
        saveDataLocked.trail_6_L = trail_6_locked;
        saveDataLocked.trail_7_L = trail_7_locked;
        saveDataLocked.trail_8_L = trail_8_locked;

        SaveLoadLocked.SavePlayer(saveDataLocked);
    }

    // NOT FOR USER
    public void ResetSave()
    {
        hat_2_locked = true;
        hat_3_locked = true;
        hat_4_locked = true;
        hat_5_locked = true;

        body_2_locked = true;
        body_3_locked = true;
        body_4_locked = true;
        body_5_locked = true;

        hands_2_locked = true;
        hands_3_locked = true;
        hands_4_locked = true;
        hands_5_locked = true;

        legs_2_locked = true;
        legs_3_locked = true;
        legs_4_locked = true;
        legs_5_locked = true;

        trail_2_locked = true;
        trail_3_locked = true;
        trail_4_locked = true;
        trail_5_locked = true;
        trail_6_locked = true;
        trail_7_locked = true;
        trail_8_locked = true;

        PlayerDataCustomize saveDataCustomize = new PlayerDataCustomize();
        hat_selection = 1;
        body_selection = 1;
        hands_selection = 1;
        legs_selection = 1;
        trail_selection = 1;

        LoadHatDetails(1); // Default Hat;
        LoadBodyDetails(1); // Default Body;
        LoadHandsDetails(1); // Default Hands;
        LoadLegsDetails(1); // Default Legs;
        LoadTrailDetails(1); // Default Trail;

        saveDataCustomize.hat_selection = 1;
        saveDataCustomize.body_selection = 1;
        saveDataCustomize.hands_selection = 1;
        saveDataCustomize.legs_selection = 1;
        saveDataCustomize.trail_selection = 1;
        SaveLoadCustomize.SavePlayer(saveDataCustomize);

        PlayerDataLocked saveDataLocked = new PlayerDataLocked();
        saveDataLocked.hat_2_L = hat_2_locked;
        saveDataLocked.hat_3_L = hat_3_locked;
        saveDataLocked.hat_4_L = hat_4_locked;
        saveDataLocked.hat_5_L = hat_5_locked;

        saveDataLocked.body_2_L = body_2_locked;
        saveDataLocked.body_3_L = body_3_locked;
        saveDataLocked.body_4_L = body_4_locked;
        saveDataLocked.body_5_L = body_5_locked;

        saveDataLocked.hands_2_L = hands_2_locked;
        saveDataLocked.hands_3_L = hands_3_locked;
        saveDataLocked.hands_4_L = hands_4_locked;
        saveDataLocked.hands_5_L = hands_5_locked;

        saveDataLocked.legs_2_L = legs_2_locked;
        saveDataLocked.legs_3_L = legs_3_locked;
        saveDataLocked.legs_4_L = legs_4_locked;
        saveDataLocked.legs_5_L = legs_5_locked;

        saveDataLocked.trail_2_L = trail_2_locked;
        saveDataLocked.trail_3_L = trail_3_locked;
        saveDataLocked.trail_4_L = trail_4_locked;
        saveDataLocked.trail_5_L = trail_5_locked;
        saveDataLocked.trail_6_L = trail_6_locked;
        saveDataLocked.trail_7_L = trail_7_locked;
        saveDataLocked.trail_8_L = trail_8_locked;
        SaveLoadLocked.SavePlayer(saveDataLocked);

        // Save Coins
        PlayerDataCoins saveDataCoins = new PlayerDataCoins();
        saveDataCoins.Coins = 10000;
        SaveLoadCoins.SavePlayer(saveDataCoins);

        //Save Orbs
        PlayerDataOrbs saveDataOrbs = new PlayerDataOrbs();
        saveDataOrbs.Red_Orbs = 25000;
        saveDataOrbs.Blue_Orbs = 25000;
        SaveLoadOrbs.SavePlayer(saveDataOrbs);

        // Reset Upgrade
        PlayerDataUpgrade saveDataUpgrade = new PlayerDataUpgrade();
        saveDataUpgrade.upgrade_1_level = 0;
        saveDataUpgrade.upgrade_2_level = 0;
        saveDataUpgrade.upgrade_3_level = 0;
        SaveLoadUpgrade.SavePlayer(saveDataUpgrade);

        LoadCoins();
        LoadOrbs();
        LoadUpgrades();
        LoadCosts();
        LoadCustomization();
        LoadLocked();
    } 
}
