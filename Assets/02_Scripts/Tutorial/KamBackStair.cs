using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamBackStair : MonoBehaviour
{
    public List<GameObject> stair = new List<GameObject>();

    int num = 0;
    public static readonly WaitForSeconds stairspeed = new WaitForSeconds(0.6f);

    private void Start()
    {
        StartCoroutine(KamBack());
        Debug.Log(stair.Count);
    }


    private IEnumerator KamBack()
    {
        GameObject s = null;
        while (true)
        {
            s = stair[num];

            if (num >= 9)
                num = 0;
            else
                num++;

            s.SetActive(false);
            yield return stairspeed;
            s.SetActive(true);
            yield return stairspeed;
        }
    }

    private void SetNum(GameObject s)
    {

    }
}
