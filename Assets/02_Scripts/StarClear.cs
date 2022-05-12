using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarClear : MonoBehaviour
{
    public List<GameObject> foundObjects;
    public GameObject StageClear;
    public string tagName;
    public float shortDis;
    void Update()
    {
        foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tagName));
        shortDis = Vector3.Distance(gameObject.transform.position, foundObjects[0].transform.position);


        foreach (GameObject found in foundObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
            if (distance < shortDis)
            {
                StageClear = found;
                shortDis = distance;
            }
            if (shortDis < 2 && Input.GetKeyDown(KeyCode.F)) 
            {
                Debug.Log("스테이지 클리어");
            }
        }
    }
}
