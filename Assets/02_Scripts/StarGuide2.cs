using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGuide2 : MonoBehaviour
{
    public GameObject exclamation;

    public List<GameObject> foundObjects;
    public string tagName;
    public float shortDis;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Exclamation = Instantiate(exclamation);
        Exclamation.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tagName));
        shortDis = Vector3.Distance(gameObject.transform.position, foundObjects[0].transform.position);

        exclamation = foundObjects[0];

        foreach (GameObject found in foundObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
            if (distance < shortDis)
            {
                exclamation = found;
                shortDis = distance;
            }
            if (shortDis < 3.5 && Input.GetKeyDown(KeyCode.F))
            {
                Destroy(exclamation);
            }
        }
    }
}
