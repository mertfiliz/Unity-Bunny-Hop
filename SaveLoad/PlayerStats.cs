using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text HighScore_Text;
    void Start()
    {
        PlayerData loadedData = SaveLoad.LoadPlayer();
        HighScore_Text.text = loadedData.highscore.ToString();
    }
}
