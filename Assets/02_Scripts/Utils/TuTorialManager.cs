using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TuTorialManager : MonoBehaviour
{
    public List<bool> isDone = new List<bool>();
    public GameObject puzzleClearDoor = null;

    public ParticleSystem cleareffect = null;
    int cnt = 0;
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

    private void MoveDoor()
    {
        SoundManager.Instance.Sound_StairClear();
        cleareffect.Play();
        puzzleClearDoor.transform.DOMoveY(22, 3f);
    }
}
