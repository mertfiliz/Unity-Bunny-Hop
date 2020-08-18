using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float music_vol;
    public float effect_vol;
    public int music_selection;
    public int effect_selection;
    public GameObject MusicBox, EffectBox;
    public Sprite[] SoundBox;

    void Start()
    {
        PlayerDataSettings loadedData = SaveLoadSettings.LoadPlayer();   
        music_selection = loadedData.music_vol_selection;
        effect_selection = loadedData.effect_vol_selection;
        Music_Box_Update(music_selection);
        Effect_Box_Update(effect_selection);
    }

    public void SaveSettings()
    {
        // Save Settings
        PlayerDataSettings saveDataSettings = new PlayerDataSettings();
        saveDataSettings.music_vol_selection = music_selection;
        saveDataSettings.effect_vol_selection = effect_selection;
        SaveLoadSettings.SavePlayer(saveDataSettings);
    }

    public void Music_Box_Update(int m_select)
    {
        if (m_select == 0)
        {
            music_vol = 0f;
            MusicBox.GetComponent<Image>().sprite = SoundBox[0];
        }
        else if (m_select == 1)
        {
            music_vol = 0.2f;
            MusicBox.GetComponent<Image>().sprite = SoundBox[1];
        }
        else if (m_select == 2)
        {
            music_vol = 0.4f;
            MusicBox.GetComponent<Image>().sprite = SoundBox[2];
        }
        else if (m_select == 3)
        {
            music_vol = 0.6f;
            MusicBox.GetComponent<Image>().sprite = SoundBox[3];
        }
        else if (m_select == 4)
        {
            music_vol = 0.8f;
            MusicBox.GetComponent<Image>().sprite = SoundBox[4];
        }
        else if (m_select == 5)
        {
            music_vol = 1f;
            MusicBox.GetComponent<Image>().sprite = SoundBox[5];
        }

        SaveSettings();
        GameObject.Find("Sound_Lobby").GetComponent<AudioSource>().volume = music_vol;
    }

    public void Effect_Box_Update(int e_select)
    {
        if (e_select == 0)
        {
            effect_vol = 0f;
            EffectBox.GetComponent<Image>().sprite = SoundBox[0];
        }
        else if (e_select == 1)
        {
            effect_vol = 0.2f;
            EffectBox.GetComponent<Image>().sprite = SoundBox[1];
        }
        else if (e_select == 2)
        {
            effect_vol = 0.4f;
            EffectBox.GetComponent<Image>().sprite = SoundBox[2];
        }
        else if (e_select == 3)
        {
            effect_vol = 0.6f;
            EffectBox.GetComponent<Image>().sprite = SoundBox[3];
        }
        else if (e_select == 4)
        {
            effect_vol = 0.8f;
            EffectBox.GetComponent<Image>().sprite = SoundBox[4];
        }
        else if (e_select == 5)
        {
            effect_vol = 1f;
            EffectBox.GetComponent<Image>().sprite = SoundBox[5];
        }
        SaveSettings();
        GameObject.Find("Menu_Blop").GetComponent<AudioSource>().volume = effect_vol;
        GameObject.Find("Chest_Sound").GetComponent<AudioSource>().volume = effect_vol;
    }

    public void Increase_MusicVolume()
    {       
        if(music_selection == 4)
        {
            music_selection = 5;
            Music_Box_Update(music_selection);
        }
        else if(music_selection == 3)
        {
            music_selection = 4;
            Music_Box_Update(music_selection);
        }
        else if (music_selection == 2)
        {
            music_selection = 3;
            Music_Box_Update(music_selection);
        }
        else if (music_selection == 1)
        {
            music_selection = 2;
            Music_Box_Update(music_selection);
        }
        else if (music_selection == 0)
        {
            music_selection = 1;
            Music_Box_Update(music_selection);
        }
    }
    public void Decrease_MusicVolume()
    {
        if (music_selection == 1)
        {
            music_selection = 0;
            Music_Box_Update(music_selection);
        }
        else if (music_selection == 2)
        {
            music_selection = 1;
            Music_Box_Update(music_selection);
        }
        else if (music_selection == 3)
        {
            music_selection = 2;
            Music_Box_Update(music_selection);
        }
        else if (music_selection == 4)
        {
            music_selection = 3;
            Music_Box_Update(music_selection);
        }
        else if (music_selection == 5)
        {
            music_selection = 4;
            Music_Box_Update(music_selection);
        }       
    }

    public void Increase_EffectVolume()
    {
        if (effect_selection == 4)
        {
            effect_selection = 5;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 3)
        {
            effect_selection = 4;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 2)
        {
            effect_selection = 3;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 1)
        {
            effect_selection = 2;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 0)
        {
            effect_selection = 1;
            Effect_Box_Update(effect_selection);
        }
    }
    
    public void Decrease_EffectVolume()
    {
        if (effect_selection == 1)
        {
            effect_selection = 0;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 2)
        {
            effect_selection = 1;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 3)
        {
            effect_selection = 2;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 4)
        {
            effect_selection = 3;
            Effect_Box_Update(effect_selection);
        }
        else if (effect_selection == 5)
        {
            effect_selection = 4;
            Effect_Box_Update(effect_selection);
        }
    }
}
