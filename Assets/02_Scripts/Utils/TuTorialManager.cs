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

    [HideInInspector] public List<bool> isDone = new List<bool>();
    public GameObject puzzleClearDoor = null;

    public ParticleSystem cleareffect = null;
    int cnt = 0;

    //-------------------------------------------------------------------//

    public Color puzzle3DefaultColor;

    //-------------------------------------------------------------------//

    public List<bool> isSound = new List<bool>();
    public List<GameObject> soundObj = new List<GameObject>();
    public bool isClear_sound = false;

    private void Start()
    {
        SoundManager.Instance.Sound_TutoBGM();
        UIManager.Instance.SettingTexts(0);
        SetDefaultColor();
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

    #region Puzzle 2 Functions

    private void SetDefaultColor()
    {
        puzzle3DefaultColor = new Color(0.32f, 0.28f, 0.55f, 1f);
    }



    #endregion

    #region Puzzle 4

    public void ResetBoolenSound()
    {
        for (int i = 0; i < isSound.Count; i++)
            isSound[i] = false;
    }

    #endregion
}
