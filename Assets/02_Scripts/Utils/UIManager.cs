using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("------UI ���ӿ�����Ʈ------")]
    [Tooltip("ū ����â")] public Image bigSetChang = null;

    [Tooltip("���� ����â")] public GameObject settingChang = null;
    [Tooltip("ū ����â")] public GameObject settingMenuChang = null;
    [Tooltip("�� ��")] public GameObject endGameChang = null;

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

    // click ����â �ݱ�
    public void OnClickSetQick()
    {
        //isSettingChang = !isSettingChang;
        //settingChang.SetActive(false);

        //GameManager.Instance.SetGameState(GameState.isPlaying);
        //bigSetChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
        SettingChangSet();
    }

    // click �ݱ�
    public void OnClickReallySetOut()
    {
        settingChang.SetActive(false);
        SettingChangSet();
    }

    // click ����
    public void OnClickOption()
    {
        settingMenuChang.SetActive(false);
        settingChang.SetActive(true);
    }

    // click ��������
    public void OnClickQuit()
    {
        Debug.Log("���� ����");
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
