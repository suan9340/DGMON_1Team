using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public GameObject targetPosition;
    public string _targetName = "Cube";

    void Update()
    {
        transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition.transform.position, 0.01f);
    }
}

