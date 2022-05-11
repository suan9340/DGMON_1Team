using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuTorialManager : MonoBehaviour
{
    public GameObject circleEffect = null;
    [Header("튜토리얼 1단계")] public List<Transform> tutoTrn = new List<Transform>();

    private Transform curretnTrn;

    [SerializeField, Header("현재 튜토리얼 1단계 단계")]
    private int num = 0;

    private void Start()
    {
        ConnectPosition(num);
    }

    // 배열에 등록되 있는 값을 하나씩 현재 포지션에 연결해주는
    private void ConnectPosition(int _num)
    {
        circleEffect.transform.position = tutoTrn[_num].transform.position;
    }
}
