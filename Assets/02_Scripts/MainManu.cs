using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainManu : MonoBehaviour
{
    public GameObject gameQuitObj = null;
    private bool isOutGame = false;

    private void Update()
    {
        if (isOutGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnClickNoQuit();
            }
        }
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("TuTorial");
    }

    public void OnClickQuit()
    {
        gameQuitObj.SetActive(true);
        isOutGame = true;
    }

    public void OnClickYesQuit()
    {
        Application.Quit();
    }

    public void OnClickNoQuit()
    {
        gameQuitObj.SetActive(false);
        isOutGame = false;
    }
}