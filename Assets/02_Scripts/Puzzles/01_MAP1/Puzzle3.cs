using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Puzzle3 : MonoBehaviour
{
    private string tag_player = ConstantManager.TAG_PLAYER;

    private float a = 36.5f;

    private bool isCol = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(tag_player))
        {
            if (isCol) return;

            isCol = true;
            Debug.Log("´ê¾Ò´ç");
            gameObject.transform.DOMoveY(a, 2f).OnComplete(() => { isCol = false; });
        }
    }
}
