using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{
    [Header("사운드 밟았을 때")]
    public AudioClip soundBlock = null;

    private string tag_Player = ConstantManager.TAG_PLAYER;
    private float sound_Vol;

    private void Start()
    {
        sound_Vol = PlayerPrefs.GetFloat(ConstantManager.VOL_VFX, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(tag_Player))
        {
            Debug.Log("qwe");
            AudioSource.PlayClipAtPoint(soundBlock, transform.position, sound_Vol);
        }
    }
}
