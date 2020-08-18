using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public GameObject Loading_Screen;
    public Slider Loading_Slider;
    public AudioSource Lobby_Sound, Menu_Blop;

    public GameObject CustomizationScreen, CustomizePanel, Upgrade_Panel, Buy_Screen, Settings_Screen, Home_Screen, BackToHome_Button;
    public Text CustomizeUpgrade_Text;
    public Button NextPrev_Button;

    private bool customizePanel = true;

    void Start()
    {
        Loading_Screen.SetActive(false);
        CustomizationScreen.SetActive(false);
        Buy_Screen.SetActive(false);
        Home_Screen.SetActive(true);
        Settings_Screen.SetActive(false);
        BackToHome_Button.SetActive(false);
    }   

    public void Load_MenuScene()
    {
        AdManager.AdManage.bannerView.Destroy();
        Loading_Screen.SetActive(true);
        StartCoroutine(LoadAsynchronously_Menu());
    }

    IEnumerator LoadAsynchronously_Menu()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");       

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            Loading_Slider.value = progress;

            yield return null;
        }

        Loading_Screen.SetActive(false);
    }

    public void Load_GameScene()
    {      
        Menu_Blop.Play();

        // Stop Lobby Music
        Lobby_Sound.Stop();

        // Load Game Scene
        StartCoroutine(LoadAsynchronously_Game());
    }

    IEnumerator LoadAsynchronously_Game()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");

        Loading_Screen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            Loading_Slider.value = progress;

            yield return null;
        }

        Loading_Screen.SetActive(false);
    }
    public void CloseProfile()
    {
        Menu_Blop.Play();
        Customization.Custom.SaveCustomization();
        CustomizeUpgrade_Text.text = "CUSTOMIZE";
        CustomizePanel.SetActive(true);
        Upgrade_Panel.SetActive(false);
        CustomizationScreen.SetActive(false);
    }

    public void CloseBuy()
    {
        Menu_Blop.Play();
        Buy_Screen.SetActive(false);
    }

    public void GoToCharacterCustomization()
    {
        Menu_Blop.Play();
        CustomizationScreen.SetActive(true);
    }

    public void OpenSettings()
    {
        Menu_Blop.Play();
        Home_Screen.SetActive(false);
        Settings_Screen.SetActive(true);
        BackToHome_Button.SetActive(true);
    }

    public void CloseSettings()
    {
        Menu_Blop.Play();
        Settings_Screen.SetActive(false);
        Home_Screen.SetActive(true);
        BackToHome_Button.SetActive(false);
    }

    public void SwapCustomizeUpgradePanel()
    {
        if(customizePanel)
        {
            customizePanel = false;
            CustomizeUpgrade_Text.text = "UPGRADE";
            CustomizePanel.SetActive(false);
            Upgrade_Panel.SetActive(true);
        }
        else
        {
            customizePanel = true;
            CustomizeUpgrade_Text.text = "CUSTOMIZE";
            CustomizePanel.SetActive(true);
            Upgrade_Panel.SetActive(false);
        }
    }

    public void OpenCustomizePanel()
    {
        CustomizePanel.SetActive(true);
        Upgrade_Panel.SetActive(false);
    }
    public void OpenUpgradePanel()
    {
        CustomizePanel.SetActive(false);
        Upgrade_Panel.SetActive(true);
    }
}
