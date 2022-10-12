using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] LayerMask layermask;
    [Header("얼음 재설정 시간")]
    public float ResetTime = 10f;
    [Header("레이길이")]
    public float rayLength = 1000f;
    [Header("고드름 중력 값")]
    public float gravityScale = 1f;

    private Vector3 endpos = Vector3.zero;
    private Vector3 startpos = Vector3.zero;

    private Rigidbody rb;
    private Collider collider;
    public RaycastHit hitinfo;

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
        RayCast();
    }

    private void RayCast()
    {
        float spehereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        if (Physics.SphereCast(startpos, spehereScale / 2f, endpos, out hitinfo, rayLength, layermask))
        {
            if (isCheckd) return;
            rb.useGravity = true;
            isCheckd = true;
        }
        else
        {
            if (isCheckd == false) return;
            isCheckd = false;
        }
    }

    private void OnDrawGizmos()
    {
        float spehereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        bool isHit = Physics.SphereCast(startpos, spehereScale / 2f, endpos, out hitinfo, rayLength, layermask);

        Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, Vector3.down * hitinfo.distance);
            Gizmos.DrawWireSphere(transform.position + Vector3.down * hitinfo.distance, transform.lossyScale.x / 2);
        }
        else
        {
            Gizmos.DrawRay(transform.position, Vector3.down * rayLength);
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
