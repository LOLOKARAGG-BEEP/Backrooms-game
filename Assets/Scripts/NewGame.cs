using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public GameObject panelSettings;

    private void Start()
    {
        panelSettings.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
        //SceneManager.LoadScene(1)
    }

    public void GameSettings()
    {
        SceneManager.LoadScene("Settings");
        //SceneManager.LoadScene(2)
    }

    public void Settings()
    {
        if (panelSettings.activeSelf == false)
        {
            panelSettings.SetActive(true);
        }
        else if (panelSettings.activeSelf == true)
        {
            panelSettings.SetActive(false);
        }
    }

    public void Exit()
    {
       Application.Quit();
    }
}
