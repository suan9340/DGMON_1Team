using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    private CharacterController controller;

    private string puzzle1Tag = ConstantManager.TAG_PZ1;

    [Header("Puzzle 1 Stair Objects")]
    public GameObject[] stairs = null;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag(puzzle1Tag))
        {

        }
    }
}
