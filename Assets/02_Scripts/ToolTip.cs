using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public List<GameObject> foundObjects;
    public GameObject Tooltip;
    public string tagName;
    public float shortDis;

    void Update()
    {
        foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tagName));
        shortDis = Vector3.Distance(gameObject.transform.position, foundObjects[0].transform.position);

        Tooltip = foundObjects[0];


        foreach (GameObject found in foundObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
            if (distance < shortDis)
            {
                Tooltip = found;
                shortDis = distance;
            }
        }
            /*if(distance < 5)
            {
                Image.FindObjectOfType<ToolTip>().
            }*/
    }
}
