using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staritem : MonoBehaviour
{
    public float rotSpeed = 100f;
    void Update()
    {
        transform.Rotate(new Vector3(transform.rotation.x, rotSpeed * Time.deltaTime, transform.rotation.z));
    }
}
