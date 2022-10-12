using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTheDiscAngle : MonoBehaviour
{
    bool isRotate = true;

    void Start()
    {
        StartCoroutine(Rotation());
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    IEnumerator Rotation()
    {
        while (isRotate)
        {
            Vector3 rotate = transform.eulerAngles;
            rotate += Vector3.up * 3f;
            transform.rotation = Quaternion.Euler(rotate);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
