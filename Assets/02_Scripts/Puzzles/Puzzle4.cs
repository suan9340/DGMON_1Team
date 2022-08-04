using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{
    [Header("사운드 밟았을 때")]
    public AudioClip soundBlock = null;

    [Header("버튼의 넘버")]
    public int num;

    [Header("버튼의 색")]
    public Color c;

    [Header("버튼의 기본 색")]
    public Color reset_color;

    private string tag_Player = ConstantManager.TAG_PLAYER;
    private float sound_Vol;

    private AudioSource audio = null;
    private Material material = null;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        material = GetComponent<Renderer>().material;

        sound_Vol = PlayerPrefs.GetFloat(ConstantManager.VOL_VFX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(tag_Player))
        {
            Sound();

            if (TuTorialManager.Instance.isClear_sound) return;
            CheckSound();
        }
    }

    private void Sound()
    {
        sound_Vol = PlayerPrefs.GetFloat(ConstantManager.VOL_VFX);
        audio.volume = sound_Vol;
        audio.Play();
    }

    private void CheckSound()
    {
        int frontnum = num - 1;
        if (num == 0)
        {
            TuTorialManager.Instance.isSound[num] = true;

            Reset_All_SoundBlockColor();
            material.color = c;

        }
        else
        {
            if (TuTorialManager.Instance.isSound[frontnum])
            {
                TuTorialManager.Instance.isSound[num] = true;

                material.color = c;

                if (num == 7)
                {
                    SoundManager.Instance.Sound_Stair_Hint();
                    TuTorialManager.Instance.ClearPuzzle4();
                }
            }
            else
            {
                TuTorialManager.Instance.ResetBoolenSound();

                Reset_All_SoundBlockColor();
            }
        }


    }

    private void Reset_All_SoundBlockColor()
    {

        foreach (var obj in TuTorialManager.Instance.soundObj)
        {
            obj.GetComponent<Renderer>().material.color = reset_color;
        }

    }
}
