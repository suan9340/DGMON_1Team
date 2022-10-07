using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] LayerMask layermask;
    [Header("얼음 재설정 시간")]
    public float ResetTime = 10f;
    public float rayLength = 1000f;

    private Vector3 endpos = Vector3.zero;
    private Vector3 startpos = Vector3.zero;

    private Rigidbody rb;
    private Collider mycollider;

    private bool isCheckd = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mycollider = GetComponent<Collider>();

        endpos = transform.TransformDirection(Vector3.down);
        startpos = transform.position;

        isCheckd = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(startpos, endpos * rayLength, Color.blue);
        //CheckRay();
    }

    private void CheckRay()
    {
        float capsuleScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);

        if (Physics.CapsuleCast(startpos, startpos, capsuleScale / 2f, endpos, out RaycastHit hitinfo, rayLength, layermask))
        {
            if (isCheckd) return;
            rb.useGravity = true;
            isCheckd = true;
            Debug.Log("인식했다고 tlqkf.");
        }
        else
        {
            if (isCheckd == false) return;
            isCheckd = false;
            Debug.Log("인식안됐다고 tlqkf.");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        rb.useGravity = false;
        mycollider.gameObject.SetActive(false);

        Invoke(nameof(SetIce), ResetTime);
    }

    private void SetIce()
    {
        rb.isKinematic = false;
        transform.position = startpos;
        gameObject.SetActive(true);
        mycollider.gameObject.SetActive(true);
    }
}
