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

    public List<bool> isDone = new List<bool>();
    public GameObject puzzleClearDoor = null;

    public ParticleSystem cleareffect = null;
    int cnt = 0;

    private void Start()
    {
        SoundManager.Instance.Sound_TutoBGM();    
    }

    #region Puzzle 1 Fuctions

    /// <summary>
    /// ������ �� �����ߴ��� üũ�ϰ� �´ٸ� �� ������
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
            MoveDoor();
    }

    /// <summary>
    /// ó���� �� ������ �ϴ°�
    /// </summary>
    private void MoveDoor()
    {
        SoundManager.Instance.Sound_StairClear();
        cleareffect.Play();
        puzzleClearDoor.transform.DOMoveY(18f, 3f);
    } 
    #endregion


}
