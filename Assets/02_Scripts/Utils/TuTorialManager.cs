using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TuTorialManager : MonoBehaviour
{
    #region SingleTon

    private static TuTorialManager _instance = null;
    public static TuTorialManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TuTorialManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("TuTorialManager").AddComponent<TuTorialManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    private PlayerData playerData;

    [HideInInspector] public List<bool> isDone = new List<bool>();
    public GameObject puzzleClearDoor = null;

    public ParticleSystem cleareffect = null;
    int cnt = 0;


    //-------------------------------------------------------------------//

    [Header("소리블럭 오브젝트와 불리언")]
    [HideInInspector] public List<bool> isSound = new List<bool>();
    [HideInInspector] public List<GameObject> soundObj = new List<GameObject>();

    [Header("소리블럭 클리어 했는지 확인")]
    public bool isClear_sound = false;

    [Header("클리어했을 때 별과 문 상호작용 오브젝트")]
    public ParticleSystem stat5 = null;
    public GameObject door = null;

    private void Start()
    {
        ConnectData();

        SoundManager.Instance.Sound_TutoBGM();
        UIManager.Instance.SettingTexts(0);

        playerData.starCnt = 0;
        playerData.isClear0 = false;
    }

    public void ConnectData()
    {
        const string SAVE_PATH = "SO/";
        playerData = Resources.Load<PlayerData>(SAVE_PATH + "PlayerData");
    }

    #region Puzzle 1 Fuctions

    /// <summary>
    /// 조건을 다 만족했는지 체크하고 맞다면 문 열리게
    /// </summary>
    public void CheckOpenDoor()
    {
        cnt = 0;
        for (int i = 0; i < isDone.Count; i++)
        {
            if (isDone[i] == true)
                cnt++;
        }

        if (cnt == isDone.Count)
        {
            SoundManager.Instance.Sound_MainBGM();
            UIManager.Instance.SettingTexts(1);
            MoveDoor();
        }
    }

    /// <summary>
    /// 처음에 문 열리게 하는거
    /// </summary>
    private void MoveDoor()
    {
        SoundManager.Instance.Sound_StairClear();
        cleareffect.Play();
        puzzleClearDoor.transform.DOMoveY(15f, 3f);
    }
    #endregion


    #region Puzzle 4

    public void ResetBoolenSound()
    {
        for (int i = 0; i < isSound.Count; i++)
            isSound[i] = false;
    }

    public void ClearPuzzle4()
    {
        door.gameObject.SetActive(false);
        stat5.gameObject.SetActive(true);
        isClear_sound = true;
    }

    #endregion
}
