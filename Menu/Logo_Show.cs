using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo_Show : MonoBehaviour
{
    void Start()
    {
        Invoke("Load_MenuScene", 2f);
    }

    public void Load_MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
