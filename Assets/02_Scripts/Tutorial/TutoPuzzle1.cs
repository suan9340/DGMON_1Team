using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPuzzle1 : MonoBehaviour
{
    public List<Color> mycolors = new List<Color>();

    public GameObject openObj = null;

    private void Start()
    {
        openObj.GetComponent<Renderer>().material.color = mycolors[2];
    }
}
