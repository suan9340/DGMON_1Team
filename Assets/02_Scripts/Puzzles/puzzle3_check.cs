using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle3_check : MonoBehaviour
{
    private bool isCanCheck = false;

    private string tag_Puzzle3 = ConstantManager.TAG_PZ3;
    private void OnEnable()
    {
        Debug.Log("켜졌어");
        isCanCheck = true;
    }

    private void OnDisable()
    {
        Debug.Log("꺼졌어");
        isCanCheck = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tag_Puzzle3))
        {

        }
    }
}
