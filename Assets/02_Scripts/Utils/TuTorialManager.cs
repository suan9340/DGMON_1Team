using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuTorialManager : MonoBehaviour
{
    public GameObject circleEffect = null;
    [Header("Ʃ�丮�� 1�ܰ�")] public List<Transform> tutoTrn = new List<Transform>();

    private Transform curretnTrn;

    [SerializeField, Header("���� Ʃ�丮�� 1�ܰ� �ܰ�")]
    private int num = 0;

    private void Start()
    {
        ConnectPosition(num);
    }

    // �迭�� ��ϵ� �ִ� ���� �ϳ��� ���� �����ǿ� �������ִ�
    private void ConnectPosition(int _num)
    {
        circleEffect.transform.position = tutoTrn[_num].transform.position;
    }
}
