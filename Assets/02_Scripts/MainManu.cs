using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainManu : MonoBehaviour
{
    public GameObject gameQuitObj = null;
    public GameObject SetChangObj = null;

    private bool isOutGame = false;
    private bool isSetting = false;

    private void Update()
    {
        if (isOutGame || isSetting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnClickNoQuit();
                OnClickQuitSetting();
            }
        }
    }

    public void OnClickStartGame()
    {
        GameManager.Instance.SetGameState(GameState.isPlaying);
        SceneManager.LoadScene("TuTorial");
    }

    public void OnClickYesQuit()
    {
        Application.Quit();
    }

    public void OnClickNoQuit()
    {
        gameQuitObj.gameObject.SetActive(false);
        gameQuitObj.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
        isOutGame = false;
    }

    public void OnClickQuitGame()
    {
        gameQuitObj.gameObject.SetActive(true);
        gameQuitObj.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
        isOutGame = true;
    }
    public void OnClickSetting()
    {
        SetChangObj.gameObject.SetActive(true);
        isSetting = true;
        SetChangObj.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
    }

    public void OnClickQuitSetting()
    {
        SetChangObj.gameObject.SetActive(false);
        isSetting = false;
        SetChangObj.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
    }
}