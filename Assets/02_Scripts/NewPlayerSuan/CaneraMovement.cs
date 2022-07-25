using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneraMovement : MonoBehaviour
{
    public Transform followObj = null;
    public float followSpeed = 10f;
    public float sensitivity = 100f;
    public float clampAngle = 70f;

    private float rotX;
    private float rotY;

    public Transform realCamera = null;
    public Vector3 dirNormalized = Vector3.zero;
    public Vector3 finalDir = Vector3.zero;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;

    private string MOS_Y = ConstantManager.MV_MOSY;
    private string MOS_X = ConstantManager.MV_MOSX;

    private void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
    }

    private void Update()
    {
        rotX += -(Input.GetAxis(MOS_Y)) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis(MOS_X) * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, followObj.position, followSpeed * Time.deltaTime);
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        else
            finalDistance = maxDistance;

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);

    }
}
