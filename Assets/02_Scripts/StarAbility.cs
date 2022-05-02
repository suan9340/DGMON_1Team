using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAbility : MonoBehaviour
{
    public enum StartypeState
    {
        Aries, Taurus, Gemini, Cancer, Leo, Virgo,
        Libra, Scorpio, Sagittarius, Capricorn, Aquarius,
        Pisces, Ophiuchus
    }
    public StartypeState StarState = StartypeState.Pisces;

    public float coolTime;
    private float readyTime;

    private void Start()
    {
        StarState = StartypeState.Ophiuchus;
    }
    private void Update()
    {
        Star();
        StarForm();
    }
    private void Star()
    {
        if (Time.time > readyTime)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                StarState = StartypeState.Aries;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                StarState = StartypeState.Taurus;

                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                StarState = StartypeState.Gemini;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                StarState = StartypeState.Cancer;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                StarState = StartypeState.Leo;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                StarState = StartypeState.Virgo;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                StarState = StartypeState.Libra;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                StarState = StartypeState.Scorpio;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                StarState = StartypeState.Sagittarius;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                StarState = StartypeState.Capricorn;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                StarState = StartypeState.Aquarius;
                readyTime = Time.time + coolTime;
            }
            if (Input.GetKeyDown(KeyCode.F12))
            {
                StarState = StartypeState.Pisces;
                readyTime = Time.time + coolTime;
            }
        }

    }
    private void StarForm()
    {
        if (StarState != StartypeState.Taurus)
        {
            GetComponent<Taurus>().enabled = false;
        }
        if (StarState == StartypeState.Taurus)
        {
            GetComponent<Taurus>().enabled = true;
        }
    }
}
