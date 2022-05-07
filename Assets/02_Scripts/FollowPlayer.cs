using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public Transform playerTrn = null;
    public GameObject targetPosition;
    Vector3 vel = Vector3.zero;
    //update에 사용할 때
    private void Update()
    {
        /*transform.position = Vector3.Lerp(transform.position, playerTrn.position, 3f);*/
        transform.position = playerTrn.position + offset;
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition.transform.position, ref vel, 1f);
    }


    //Coroutine + Lerp 사용할때
    private void B()
    {
        StartCoroutine(A());
    }

    private IEnumerator A()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * 3f;
            transform.position = Vector3.Lerp(transform.position, playerTrn.position, time);

        }
        yield return null;
        yield return new WaitForSeconds(0.4f);
    }
}
