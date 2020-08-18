using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public GameObject Loading_Screen;
    public Slider Loading_Slider;
    public AudioSource AudioSource;

    void Start()
    {
        AdManager.AdManage.Request_Banner();
        AdManager.AdManage.Show_Banner();
    }

    public void Load_MenuScene()
    {
        AudioSource.Stop();
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
}
