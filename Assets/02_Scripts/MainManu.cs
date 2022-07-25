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

    private void Start()
    {
        SoundManager.Instance.Sound_TutoBGM();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOutGame)
            {
                OnClickQuitGame();
            }
            if (isSetting)
            {
                OnClickSetting();
            }
        }
    }

    /// <summary>
    /// 게임 시작 눌렀을 때
    /// </summary>
    public void OnClickStartGame()
    {
        SoundManager.Instance.Sound_ButtonClick();
        GameManager.Instance.SetGameState(GameState.isPlaying);
        SceneManager.LoadScene(1);
    }



    /// <summary>
    /// 게임 나갈껀지 물어봤을 때 네! 라고 했을 때
    /// </summary>
    public void OnClickYesQuit()
    {
        SoundManager.Instance.Sound_ButtonClick();
        Debug.Log("Quit Game Sucess!");
        Application.Quit();
    }



    /// <summary>
    /// 게임 나간다고 눌렀을 때
    /// </summary>
    public void OnClickQuitGame()
    {
        SoundManager.Instance.Sound_ButtonClick();

        isOutGame = !isOutGame;
        if (isOutGame)
        {
            gameQuitObj.SetActive(true);
            gameQuitObj.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
        }
        else
        {
            gameQuitObj.transform.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => { gameQuitObj.SetActive(false); });
        }
    }



    /// <summary>
    /// 게임 설정 눌렀을 때
    /// </summary>
    public void OnClickSetting()
    {
        SoundManager.Instance.Sound_ButtonClick();

        isSetting = !isSetting;
        if (isSetting)
        {
            SetChangObj.SetActive(true);
            SetChangObj.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
        }
        else
        {
            SetChangObj.transform.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => { SetChangObj.SetActive(false); });
        }

    }
}