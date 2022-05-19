using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPrevention : MonoBehaviour
{
    public Vector3 playerStartPos = Vector3.zero;

    private void Start()
    {
        playerStartPos = new Vector3(-80f, 20f, 17f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = playerStartPos;
        }
    }
}
