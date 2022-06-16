using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObj : MonoBehaviour
{
    [Header("------�������� ����------")]
    public GameObject crossHair = null;
    public Vector3 offset = Vector3.zero;

    [Header("------ī�޶� ����------")]
    public GameObject camObj = null;
    public Camera tpsCam = null;
    public float valuecam = 10f;

    [Header("------����ü ��ô�� ����------")]
    [Range(0, 50)] public float maxDistance = 20f;
    public GameObject bomObj = null;


    private Vector3 hitInfoTrn = Vector3.zero;
    private bool isZoom = false;
    RaycastHit hit;
    Rigidbody bomrgid;

    private void Update()
    {
        InputKey();
        DrawRay();
    }

    private void InputKey()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isZoom = true;
            crossHair.SetActive(true);
            ZoomIn();
        }

        if (Input.GetMouseButtonUp(1))
        {
            isZoom = false;
            crossHair.SetActive(false);
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        tpsCam.fieldOfView -= valuecam;
        camObj.transform.position += offset;
    }

    private void ZoomOut()
    {
        tpsCam.fieldOfView += valuecam;
        camObj.transform.position -= offset;
    }

    private void DrawRay()
    {
        if (!isZoom) return;
        Debug.DrawRay(camObj.transform.position, camObj.transform.forward * maxDistance, Color.magenta);
        if (Physics.Raycast(camObj.transform.position, camObj.transform.forward, out hit, maxDistance))
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitInfoTrn = hit.transform.position;
                GameObject obj = Instantiate(bomObj, camObj.transform.position, Quaternion.identity);
                bomrgid = obj.GetComponent<Rigidbody>();
                bomrgid.AddForce(hitInfoTrn, ForceMode.Impulse);

                Destroy(obj, 3f);
            }
        }
    }
}
