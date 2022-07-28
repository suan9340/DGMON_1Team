using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region SingleTon

    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    [Header("------UI 게임오브젝트------")]
    [Tooltip("큰 설정창")] public Image bigSetChang = null;

    [Tooltip("세부 설정창")] public GameObject settingChang = null;
    [Tooltip("큰 설정창")] public GameObject settingMenuChang = null;
    [Tooltip("겜 끝")] public GameObject endGameChang = null;

    [Tooltip("별 UI")] public Text starUIText = null;

    private bool isSettingChang = false;

    private PlayerData playerData;

    private void Awake()
    {
        ConnectData();
        UpdateStarUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingChangSet();
        }
    }

    public void ConnectData()
    {
        const string SAVE_PATH = "SO/";
        playerData = Resources.Load<PlayerData>(SAVE_PATH + "PlayerData");
    }

    /// <summary>
    /// 설정창 눌렀을 때
    /// </summary>
    public void SettingChangSet()
    {
        SoundManager.Instance.Sound_ButtonClick();

        isSettingChang = !isSettingChang;
        if (isSettingChang)
        {
            bigSetChang.gameObject.SetActive(true);
            GameManager.Instance.SetGameState(GameState.isSetting);
            bigSetChang.transform.DOScaleY(1f, 0.15f).SetUpdate(true);

            Time.timeScale = 0f;
        }
        else
        {
            GameManager.Instance.SetGameState(GameState.isPlaying);
            bigSetChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => { bigSetChang.gameObject.SetActive(false); });

            Time.timeScale = 1f;
        }
    }

    public void EndGame()
    {
        GameManager.Instance.SetGameState(GameState.isSetting);
        endGameChang.gameObject.SetActive(true);
        endGameChang.transform.DOScaleY(1f, 0.15f).SetUpdate(true);
    }

    public void OnClickEndGameStartScene()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.SetGameState(GameState.isStarting);
    }

    public void OnClickContinueGame()
    {
        GameManager.Instance.SetGameState(GameState.isPlaying);
        endGameChang.gameObject.SetActive(false);
        endGameChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true);
    }

    public void UpdateStarUI()
    {
        starUIText.text = $"★ : {playerData.starCnt}";
    }
}
