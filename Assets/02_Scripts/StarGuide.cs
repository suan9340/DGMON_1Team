using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGuide : MonoBehaviour
{
    public GameObject question;

    public List<GameObject> foundObjects;
    public string tagName;
    public float shortDis;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Question = Instantiate(question);
        Question.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tagName));
        shortDis = Vector3.Distance(gameObject.transform.position, foundObjects[0].transform.position);

        question = foundObjects[0];

        foreach (GameObject found in foundObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
            if (distance < shortDis)
            {
                question = found;
                shortDis = distance;
            }
            if (shortDis < 3.5 && Input.GetKeyDown(KeyCode.F))
            {
                Destroy(question);
            }
        }
    }
}
