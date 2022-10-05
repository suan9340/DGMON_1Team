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
    private Collider collider;

    private bool isCheckd = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        endpos = transform.TransformDirection(Vector3.down);
        startpos = transform.position;

        isCheckd = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(startpos, endpos * rayLength, Color.red);
        CheckRay();
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
        collider.gameObject.SetActive(false);

        Invoke(nameof(SetIce), ResetTime);
    }

    private void SetIce()
    {
        rb.isKinematic = false;
        transform.position = startpos;
        gameObject.SetActive(true);
        collider.gameObject.SetActive(true);
    }
}
