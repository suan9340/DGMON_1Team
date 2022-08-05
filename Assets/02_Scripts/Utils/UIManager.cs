using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class SettingText
{
    public string title;
    public string staytext;
    public string cant_abilitytext;
    public string get_abilitytext;
}

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

    public List<SettingText> setText = new List<SettingText>();

    [Header("------UI ���ӿ�����Ʈ------")]
    [Tooltip("ū ����â")] public Image bigSetChang = null;

    [Tooltip("���� ����â")] public GameObject settingChang = null;
    [Tooltip("ū ����â")] public GameObject settingMenuChang = null;
    [Tooltip("�� ��")] public GameObject endGameChang = null;

    [Header("������ ��� �� UI")]
    [Tooltip("�� UI")] public Text starUIText = null;

    [Header("�� �Ծ��� �� ȭ�鿡 �� �̹���")]
    public GameObject eatStar_Image = null;

    [Header("�������� �����̴�")]
    public Slider sensitivitySlider = null;

    [Header("------��ų ���ӿ�����Ʈ------")]
    public Image puzzle0_DonClearImage = null;
    public Text puzzle0_DonClearText = null;

    [Header("------�⵵ ���� ���ӿ�����Ʈ------")]
    public Image stayWarningImage = null;
    public Text stayWarningText = null;

    [Header("------�⵵ ���� ���ӿ�����Ʈ------")]
    public Image staySuccessImage = null;
    public Text[] staySucessText = null;


    private bool isShowDonJump = false;
    private bool isSettingChang = false;
    private bool isGetnewSkill = false;

    private PlayerData playerData;

    private readonly WaitForSeconds starEatScreenDelay = new WaitForSeconds(0.5f);

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

        if (Input.GetKeyDown(KeyCode.Tab) && isGetnewSkill)
        {
            isGetnewSkill = false;
        }
    }


    /// <summary>
    /// ��ûâ�̳� �׷��� ���� �� �ִϸ��̼� ���ߵ���
    /// </summary>

    public void ConnectData()
    {
        const string SAVE_PATH = "SO/";
        playerData = Resources.Load<PlayerData>(SAVE_PATH + "PlayerData");
    }

    /// <summary>
    /// ����â ������ ��
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

            //Time.timeScale = 0f;
        }
        else
        {
            GameManager.Instance.SetGameState(GameState.isPlaying);
            bigSetChang.transform.DOScaleY(0f, 0.15f).SetUpdate(true).OnComplete(() => { bigSetChang.gameObject.SetActive(false); });

            //Time.timeScale = 1f;
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
        starUIText.text = $"�� : {playerData.starCnt}";
    }

    public IEnumerator ScreenEatStar()
    {
        eatStar_Image.SetActive(true);
        yield return starEatScreenDelay;
        eatStar_Image.SetActive(false);
    }

    /// <summary>
    /// �� �Ծ��� �� ȭ�鿡�� �ణ ���̰�
    /// </summary>
    public void EatStarUI()
    {
        StartCoroutine(ScreenEatStar());
    }


    /// <summary>
    /// ����â���� �������� ���� ��
    /// </summary>
    public void OnClickSensivitivy()
    {

    }

    public void Puzzle0DonClear()
    {
        if (isShowDonJump) return;
        isShowDonJump = true;

        StartCoroutine(FadeImage(puzzle0_DonClearImage, puzzle0_DonClearText, 0.8f, 0.3f));
    }

    public void StayWarning()
    {
        StartCoroutine(FadeImage(stayWarningImage, stayWarningText, 1.4f, 0.6f));
    }

    public void StaySucessful()
    {
        isGetnewSkill = true;
        StartCoroutine(Fade2Image(staySuccessImage, staySucessText[0], staySucessText[1], 0.4f, 0.9f));
    }

    /// <summary>
    /// �̹����� �ؽ�Ʈ ���̵���,�ƿ� ���ִ°�
    /// 1. Ÿ�� �̹���, 2. Ÿ�� �ؽ�Ʈ, 3. ǥ�õ� �ð�, 4. ���� ��
    /// </summary>
    /// <param name="_image"></param>
    /// <param name="_text"></param>
    /// <param name="_delayTime"></param>
    /// <param name="_alpha"></param>
    /// <returns></returns>
    public IEnumerator FadeImage(Image _image, Text _text, float _delayTime, float _alpha)
    {
        float fadeTime = 0.5f;

        _image.gameObject.SetActive(true);
        _text.DOFade(1f, fadeTime);
        _image.DOFade(_alpha, fadeTime);

        yield return new WaitForSeconds(_delayTime);

        _image.DOFade(0f, fadeTime);
        _text.DOFade(0f, fadeTime);

        yield return new WaitForSeconds(fadeTime);

        _image.gameObject.SetActive(false);
        isShowDonJump = false;

        yield return null;
    }

    /// <summary>
    /// �̹����� �ؽ�Ʈ ���̵� �ξƿ��ε� �̰� ��ų ����� �� ����ϴ� �Լ�
    /// 1. Ÿ�� �̹���, 2,3. Ÿ�� �ؽ�Ʈ��, 4. �̹��� ���İ�, 5. �ؽ�Ʈ ���İ�
    /// </summary>
    /// <param name="_image"></param>
    /// <param name="_text"></param>
    /// <param name="_delayTime"></param>
    /// <param name="_alphaim"></param>
    /// <returns></returns>
    public IEnumerator Fade2Image(Image _image, Text _text, Text _text2, float _alphaim, float _alphaTt)
    {
        float fadeTime = 0.5f;
        SoundManager.Instance.Sound_GetNewAblilty();
        _image.gameObject.SetActive(true);
        _text.DOFade(_alphaTt, fadeTime);
        _text2.DOFade(_alphaTt, fadeTime);
        _image.DOFade(_alphaim, fadeTime);

        while (isGetnewSkill) yield return null;

        _image.DOFade(0f, fadeTime);
        _text.DOFade(0f, fadeTime);
        _text2.DOFade(0f, fadeTime);

        yield return new WaitForSeconds(fadeTime);

        _image.gameObject.SetActive(false);
        isShowDonJump = false;
        GameManager.Instance.SetGameState(GameState.isPlaying);
        yield return null;
    }

    public void SettingTexts(int _nums)
    {
        stayWarningText.text = setText[_nums].staytext;
        puzzle0_DonClearText.text = setText[_nums].cant_abilitytext;
        staySucessText[0].text = setText[_nums].get_abilitytext;
    }


}
