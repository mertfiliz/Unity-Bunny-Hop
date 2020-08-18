using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public static float Score_Multiplier_Power = 1f;

    public int highscore;

    public static PlayerData DefaultValues
    {
        get
        {
            PlayerData defaultData = new PlayerData();
            return defaultData;
        }      
    }   

    public class HighPoint: PlayerData
    {
        public static int highscore_total = 0;        
    }
}

[System.Serializable]
public class PlayerDataLocked
{
    public bool hat_1_L = false, hat_2_L = true, hat_3_L = true, hat_4_L = true, hat_5_L = true;
    public bool body_1_L = false, body_2_L = true, body_3_L = true, body_4_L = true, body_5_L = true;
    public bool hands_1_L = false, hands_2_L = true, hands_3_L = true, hands_4_L = true, hands_5_L = true;
    public bool legs_1_L = false, legs_2_L = true, legs_3_L = true, legs_4_L = true, legs_5_L = true;
    public bool trail_1_L = false, trail_2_L = true, trail_3_L = true, trail_4_L = true, trail_5_L = true, trail_6_L = true, trail_7_L = true, trail_8_L = true;

    public static PlayerDataLocked DefaultValues
    {
        get
        {
            PlayerDataLocked defaultData = new PlayerDataLocked();
            return defaultData;
        }
    }
}

[System.Serializable]
public class PlayerDataCustomize
{
    public int hat_selection, body_selection, hands_selection, legs_selection, trail_selection;

    // Chest Costs
    public static int chest_cost = 250;
    // Hat Costs
    public static int hat_2_cost = 30, hat_3_cost = 100, hat_4_cost = 500, hat_5_cost = 1000;
    // Body Costs
    public static int body_2_cost = 30, body_3_cost = 100, body_4_cost = 500, body_5_cost = 1000;
    // Hands Costs
    public static int hands_2_cost = 30, hands_3_cost = 100, hands_4_cost = 500, hands_5_cost = 1000;
    // Legs Costs
    public static int legs_2_cost = 30, legs_3_cost = 100, legs_4_cost = 500, legs_5_cost = 1000;
    // Trail Costs
    public static int trail_2_cost = 200, trail_3_cost = 200, trail_4_cost = 200, trail_5_cost = 500, trail_6_cost = 750, trail_7_cost = 1000, trail_8_cost = 2000;

    public static PlayerDataCustomize DefaultValues
    {
        get
        {
            PlayerDataCustomize defaultData = new PlayerDataCustomize();
            return defaultData;
        }
    }

    public class Trails : PlayerDataCustomize
    {
        public static int trail_no = 0;
        public static int hat_no = 0;
    }
}

[System.Serializable]
public class PlayerDataCoins
{
    public int Coins;

    public static PlayerDataCoins DefaultValues
    {
        get
        {
            PlayerDataCoins defaultData = new PlayerDataCoins();
            return defaultData;
        }
    }
}

[System.Serializable]
public class PlayerDataOrbs
{
    public int Red_Orbs;
    public int Blue_Orbs;

    public static PlayerDataOrbs DefaultValues
    {
        get
        {
            PlayerDataOrbs defaultData = new PlayerDataOrbs();
            defaultData.Red_Orbs = 0;
            defaultData.Blue_Orbs = 0;
            return defaultData;
        }
    }
}

[System.Serializable]
public class PlayerDataSettings
{
    public int music_vol_selection;
    public int effect_vol_selection;

    public static PlayerDataSettings DefaultValues
    {
        get
        {
            PlayerDataSettings defaultData = new PlayerDataSettings();
            defaultData.music_vol_selection = 4;
            defaultData.effect_vol_selection = 4;
            return defaultData;
        }
    }
}

[System.Serializable]
public class PlayerDataUpgrade
{
    public float Score_Multiplier = 1f;
    public int Carrot_Points = 0;
    public int Carrot_Spawn_Rate = 0;
    public int upgrade_1_level;
    public int upgrade_2_level;
    public int upgrade_3_level;
    public int upgrade_1_cost_base = 40;
    public int upgrade_2_cost_base = 40;
    public int upgrade_3_cost_base = 40;

    public static PlayerDataUpgrade DefaultValues
    {
        get
        {
            PlayerDataUpgrade defaultData = new PlayerDataUpgrade();
            defaultData.Score_Multiplier = 1f;
            defaultData.Carrot_Points = 0;
            defaultData.Carrot_Spawn_Rate = 0;
            defaultData.upgrade_1_level = 0;
            defaultData.upgrade_2_level = 0;
            defaultData.upgrade_3_level = 0;
            return defaultData;
        }
    }
}
