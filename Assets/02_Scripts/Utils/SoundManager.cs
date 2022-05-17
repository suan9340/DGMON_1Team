using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [Header("[[---BGM---]]")] public AudioSource BgmAudio = null;
    [Header("[[---EFFECT---]]")] public AudioSource clearLevel = null;
    public AudioSource jumpSound = null;
    public AudioSource completeSond = null;
    public AudioSource getCoin = null;

    public void SetBGMSlider(float _volume)
    {
        BgmAudio.volume = _volume;
    }

    public void SetEFFECTSlider(float _voulme)
    {
        clearLevel.volume = _voulme;
        jumpSound.volume = _voulme;
        completeSond.volume = _voulme;
        getCoin.volume = _voulme;
    }
}
