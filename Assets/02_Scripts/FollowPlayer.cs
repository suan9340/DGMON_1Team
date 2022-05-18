using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    /*public Vector3 offset = Vector3.zero;
    public GameObject targetPosition;*/
    public Transform target;
    private float relativeHeigth = 1.0f; // 높이 즉 y값
    private float zDistance = -1.0f;// z값 나는 사실 필요 없었다.
    private float xDistance = 1.0f; // x값
    public float dampSpeed = 2;  // 따라가는 속도 짧으면 타겟과 같이 움직인다.
    [SerializeField] private Transform MobArm;

    void Update()
    {
/*        transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition.transform.position, 0.02f);
*/        Vector3 newPos = target.position + new Vector3(xDistance, relativeHeigth, -zDistance); // 타겟 포지선에 해당 위치를 더해.. 즉 타겟 주변에 위치할 위치를 담는다.. 일정의 거리를 구하는 방법
          transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dampSpeed);
          //MobArm.rotation = Quaternion.Euler()
    }
}

