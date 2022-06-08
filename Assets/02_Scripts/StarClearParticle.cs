using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarClearParticle : MonoBehaviour
{
    public GameObject starBoxParticle;
    public GameObject starBox;
    private float timeLeft = 2.0f;
    private float nextTime = 0.0f;

    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;

            StarBox();
        }
    }
    void StarBox()
    {
        GameObject starbox = Instantiate(starBox);
        starbox.transform.position = transform.position;
        GameObject starClearParticle = Instantiate(starBoxParticle);
        starClearParticle.transform.position = transform.position;
    }
}
