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

            SetGameState(GameState.isPlaying);
            bigSetChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
        }
        else
        {
            settingMenuChang.SetActive(true);
            Time.timeScale = 0f;

            SetGameState(GameState.isSetting);
            bigSetChang.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
        }
    }

    public void OnClickSetQick()
    {
        isSettingChang = !isSettingChang;

        SetGameState(GameState.isPlaying);
        bigSetChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
    }

    public void OnClickReallySetOut()
    {
        settingChang.SetActive(false);
        SettingChangSet();
    }
    public void OnClickOption()
    {
        settingMenuChang.SetActive(false);
        settingChang.SetActive(true);
    }

    public void OnClickQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }


    public void SetGameState(GameState _state)
    {
        GameManager.Instance.gameState = _state;
    }
}
