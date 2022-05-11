using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    private Rigidbody myrigid;

    private void Awake()
    {
        myrigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PuzzleCube"))
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}
