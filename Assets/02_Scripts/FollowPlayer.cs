using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    /*public Vector3 offset = Vector3.zero;
    public GameObject targetPosition;*/
    public Transform target;
    private float relativeHeigth = 1.0f; // ���� �� y��
    private float zDistance = -1.0f;// z�� ���� ��� �ʿ� ������.
    private float xDistance = 1.0f; // x��
    public float dampSpeed = 2;  // ���󰡴� �ӵ� ª���� Ÿ�ٰ� ���� �����δ�.
    [SerializeField] private Transform MobArm;

    void Update()
    {
/*        transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition.transform.position, 0.02f);
*/        Vector3 newPos = target.position + new Vector3(xDistance, relativeHeigth, -zDistance); // Ÿ�� �������� �ش� ��ġ�� ����.. �� Ÿ�� �ֺ��� ��ġ�� ��ġ�� ��´�.. ������ �Ÿ��� ���ϴ� ���
          transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dampSpeed);
          //MobArm.rotation = Quaternion.Euler()
    }
}

