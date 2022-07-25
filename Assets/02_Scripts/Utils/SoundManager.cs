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
    public AudioSource FXAudio = null;
    public AudioSource FX2Audio = null;
    public AudioSource FX3Audio = null;

    [Header("[[---AudioSetting Sliders---]]")]
    public Slider BGMSlider = null;
    public Slider VFXSlider = null;


    private float bgmVol = 1f;
    private float vfxVol = 1f;

    // Set String 
    private string VOL_BGM = ConstantManager.VOL_BGM;
    private string VOL_VFX = ConstantManager.VOL_VFX;
    private void Start()
    {
        SettingVolume();
    }

    private void SettingVolume()
    {
        bgmVol = PlayerPrefs.GetFloat(VOL_BGM, 1f);
        BGMSlider.value = bgmVol;

        vfxVol = PlayerPrefs.GetFloat(VOL_VFX, 1f);
        VFXSlider.value = vfxVol;
    }

    /// <summary>
    /// 설정에서 배경음 슬라이더 조절하는 함수
    /// </summary>
    public void SetBGMSoundSlider()
    {
        BgmAudio.volume = BGMSlider.value;
        bgmVol = BGMSlider.value;

        PlayerPrefs.SetFloat(VOL_BGM, bgmVol);
    }

    /// <summary>
    /// 설정에서 효과음 슬라이더 조절하는 함수
    /// </summary>
    public void SetVFXSoundSlider()
    {
        FXAudio.volume = VFXSlider.value;
        FX2Audio.volume = VFXSlider.value;
        FX3Audio.volume = VFXSlider.value;

        vfxVol = VFXSlider.value;

        PlayerPrefs.SetFloat(VOL_VFX, vfxVol);
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

    public void Sound_ButtonClick()
    {
        FX3Audio.clip = VFXInfo[4].clip;
        FX3Audio.Play();
    }
}
