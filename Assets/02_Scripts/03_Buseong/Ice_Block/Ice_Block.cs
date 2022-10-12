using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Block : MonoBehaviour
{
    GameObject iceObject;
    [SerializeField] private LayerMask layermask;
    [SerializeField] private bool isIced = false;

    //[SerializeField] private GameObject starObject;

    private float minposX;

    MeshRenderer meshRenderer;

    void Start()
    {
        SettingGame();

    }

    void Update()
    {
        if (isIced)
        {
            if (iceObject.transform.localScale.x <= minposX)
            {
                //starObject.SetActive(true);
                Destroy(iceObject);
            }

            StartCoroutine(MeltingIce(iceObject));
            meshRenderer.material.color = Color.red;
            isIced = false;
        }
        else
        {
            meshRenderer.material.color = Color.green;
            StopCoroutine(MeltingIce(iceObject));
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (starObject.CompareTag("Player"))
        {
            Destroy(starObject);
        }
    }*/

    private void LateUpdate()
    {
        IsCheck(iceObject);
    }

    private void SettingGame()
    {
        iceObject = GetComponent<GameObject>();
        meshRenderer = GetComponent<MeshRenderer>();

        iceObject = transform.gameObject;
        minposX = transform.localScale.x * 0.2f;
        Debug.Log(minposX);
    }

    IEnumerator MeltingIce(GameObject obj)
    {
        obj.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);

        yield return new WaitForSeconds(0.5f);
    }

    private bool IsCheck(GameObject obj)
    {
        BoxCollider boxCol = obj.transform.GetComponent<BoxCollider>();
        Bounds bounds = boxCol.bounds;

        Collider[] cols = Physics.OverlapBox(bounds.center, bounds.size * 0.3f, obj.transform.rotation, layermask);

        if (cols.Length != 0)
        {
            return isIced = true;
        }
        else return isIced = false;
    }
}
