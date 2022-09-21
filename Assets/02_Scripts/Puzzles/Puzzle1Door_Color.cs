using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Door_Color : MonoBehaviour
{
    private Material material = null;

    private void Start()
    {
        material = GetComponent<Renderer>().material;

        material.color = new Color(0f, 0.6f, 0.2f, 0f);
    }
}
