using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class AudioInfo
{
    public string name;
    public AudioClip clip;
}


public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("SoundManager").AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private List<AudioInfo> BGMInfo = new List<AudioInfo>();
    [SerializeField] private List<AudioInfo> VFXInfo = new List<AudioInfo>();


    [Header("[[---BGM---]]")] 
    public AudioSource BgmAudio = null;


    [Header("[[---EFFECT---]]")]
    //public AudioSource clearLevel = null;
    //public AudioSource jumpSound = null;
    //public AudioSource completeSond = null;
    //public AudioSource getStar = null;
    public AudioSource FXAudio = null;
    public AudioSource FX2Audio = null;

    public void SetBGMSlider(float _volume)
    {
        BgmAudio.volume = _volume;
    }

    public void SetEFFECTSlider(float _voulme)
    {
        //clearLevel.volume = _voulme;
        //jumpSound.volume = _voulme;
        //completeSond.volume = _voulme;
        //getStar.volume = _voulme;
    }

    /// <summary>
    /// 튜토리얼 BGM
    /// </summary>
    public void Sound_TutoBGM()
    {
        BgmAudio.clip = BGMInfo[0].clip;
        BgmAudio.Play();
    }

    /// <summary>
    /// 점프 효과음
    /// </summary>
    public void Sound_PlayerJump()
    {
        FXAudio.clip = VFXInfo[0].clip;
        FXAudio.Play();
    }


    /// <summary>
    /// 별 얻었을 때 효과음
    /// </summary>
    public void Sound_GetStar()
    {
        FX2Audio.clip = VFXInfo[1].clip;
        FX2Audio.Play();
    }


    /// <summary>
    /// 계단 퍼즐 풀 때 힌트 효과음
    /// </summary>
    public void Sound_Stair_Hint()
    {
        FX2Audio.clip = VFXInfo[2].clip;
        FX2Audio.Play();
    }


    /// <summary>
    /// 계단 퍼즐 풀었을 때 효과음
    /// </summary>
    public void Sound_StairClear()
    {
        FX2Audio.clip = VFXInfo[3].clip;
        FX2Audio.Play();
    }
}
