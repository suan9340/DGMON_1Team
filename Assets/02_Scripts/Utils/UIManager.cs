using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("------UI 게임오브젝트------")]
    [Tooltip("큰 설정창")] public Image bigSetChang = null;

    [Tooltip("세부 설정창")] public GameObject settingChang = null;
    [Tooltip("큰 설정창")] public GameObject settingMenuChang = null;
    [Tooltip("겜 끝")] public GameObject endGameChang = null;

    private bool isSettingChang = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingChangSet();
        }
    }

    private void SettingChangSet()
    {
        isSettingChang = !isSettingChang;
        if (!isSettingChang)
        {
            settingChang.SetActive(false);
            Time.timeScale = 1f;

            GameManager.Instance.SetGameState(GameState.isPlaying);
            bigSetChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
        }
        else
        {
            settingMenuChang.SetActive(true);
            Time.timeScale = 0f;

            GameManager.Instance.SetGameState(GameState.isSetting);
            bigSetChang.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
        }
    }

    // click 설정창 닫기
    public void OnClickSetQick()
    {
        //isSettingChang = !isSettingChang;
        //settingChang.SetActive(false);

        //GameManager.Instance.SetGameState(GameState.isPlaying);
        //bigSetChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
        SettingChangSet();
    }

    // click 닫기
    public void OnClickReallySetOut()
    {
        settingChang.SetActive(false);
        SettingChangSet();
    }

    // click 설정
    public void OnClickOption()
    {
        settingMenuChang.SetActive(false);
        settingChang.SetActive(true);
    }

    // click 게임종료
    public void OnClickQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void EndGame()
    {
        GameManager.Instance.SetGameState(GameState.isSetting);
        endGameChang.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
    }

    public void OnClickEndGameStartScene()
    {
        SceneManager.LoadScene("StartScene");
        GameManager.Instance.SetGameState(GameState.isStarting);
    }

    public void OnClickContinueGame()
    {
        GameManager.Instance.SetGameState(GameState.isPlaying);
        endGameChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
    }
}
