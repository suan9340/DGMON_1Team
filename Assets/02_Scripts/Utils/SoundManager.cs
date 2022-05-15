using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [Header("[[---BGM---]]")] public AudioSource BgmAudio = null;

    public void SetBGMSlider(float _volume)
    {
        BgmAudio.volume = _volume;
    }
}
