using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarClear : MonoBehaviour
{
    public GameObject stageClear;
    private float time;

    private void Awake()
    {
        time = 1f;
    }

    private void Update()
    {
        
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if(time <= 0)
        {
            GetComponent<StarClear>().enabled = false;
        }
    }
    private void LateUpdate()
    {
        GameObject Stageclear = Instantiate(stageClear);
        Stageclear.transform.position = transform.position;

    }
}
