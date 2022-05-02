using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taurus : MonoBehaviour
{
    public List<GameObject> foundObjects;
    public GameObject taurusObj;
    public string tagName;
    public float shortDis;
    private GameObject child;

    private void Update()
    {
        foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tagName));
        shortDis = Vector3.Distance(gameObject.transform.position, foundObjects[0].transform.position);

        taurusObj = foundObjects[0];

        foreach (GameObject found in foundObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (distance < shortDis)
            {
                taurusObj = found;
                shortDis = distance;
            }
            if (shortDis < 1.5 && Input.GetMouseButtonDown(0))
            {
                child = taurusObj;
                child.transform.parent = gameObject.transform;
            }
            if (child && Input.GetMouseButtonUp(0))
            {
                child.transform.SetParent(null);
            }
        }
    }
}
