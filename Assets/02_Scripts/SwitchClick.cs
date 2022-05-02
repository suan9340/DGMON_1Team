using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchClick : MonoBehaviour
{
    [Header("스위치 눌렀을 떄 얼마나 내릴껀지")] public float pushSwitchValue = 0.3f;
    [Header("상호작용할 맵")] public GameObject moveMapObj = null;
    public float moveMapPos = 6f;

    private Vector3 currentMapPos = Vector3.zero;

    private void Start()
    {
        currentMapPos = moveMapObj.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var objParent = gameObject.transform.parent.gameObject;
            objParent.transform.DOMove(new Vector2(objParent.transform.position.x, pushSwitchValue), 1f).OnComplete(() => MoveTrueMap());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var objParent = gameObject.transform.parent.gameObject;
            objParent.transform.DOMove(new Vector2(objParent.transform.position.x, -pushSwitchValue), 1f).OnComplete(() => MoveBackMap());
        }
    }

    private void MoveTrueMap()
    {
        moveMapObj.gameObject.transform.DOMove(new Vector3(moveMapObj.transform.position.x, moveMapPos, moveMapObj.transform.position.z), 1f);
    }

    private void MoveBackMap()
    {
        moveMapObj.gameObject.transform.DOMove(currentMapPos, 1f);
    }
}
