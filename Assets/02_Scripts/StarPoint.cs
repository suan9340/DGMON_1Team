using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPoint : MonoBehaviour
{
    public Text starPointTxt;
    [Header("모은별빛조각")] public int starPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StarPoint")
        {
            StarPoint starpoint = other.GetComponent<StarPoint>();
            starPoint++;
            StarPointText();
            Destroy(other.gameObject);
        }
    }

    void StarPointText()
    {
        starPointTxt.text = "★ : " + starPoint;
    }
}
