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
    [Header("딜레이 타임")]
    public float delayTIme = 0.5f;

    private Vector3 endpos = Vector3.zero;
    private Vector3 startpos = Vector3.zero;

    private Rigidbody rb;
    public ConstantForce cf;
    public Collider mycollider;
    public RaycastHit hitinfo;

    private bool isCheckd = true;
    private bool isDelayTime = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>();
        mycollider = GetComponentInChildren<Collider>();

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
        if (isDelayTime) return;

        float spehereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        if (Physics.SphereCast(startpos, spehereScale / 2f, endpos, out hitinfo, rayLength, layermask))
        {
            if (isCheckd) return;

            //Debug.Log("부딪혔다");
            rb.useGravity = true;
            isCheckd = true;

            cf.enabled = true;
        }
        else
        {
            if (isCheckd == false) return;
            cf.enabled = false;
            //Debug.Log("안 부딪혔다");
            isCheckd = false;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    float spehereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
    //    bool isHit = Physics.SphereCast(startpos, spehereScale / 2f, endpos, out hitinfo, rayLength, layermask);

    //    Gizmos.color = Color.red;
    //    if (isHit)
    //    {
    //        Gizmos.DrawRay(transform.position, Vector3.down * hitinfo.distance);
    //        Gizmos.DrawWireSphere(transform.position + Vector3.down * hitinfo.distance, transform.lossyScale.x / 2);
    //    }
    //    else
    //    {
    //        Gizmos.DrawRay(transform.position, Vector3.down * rayLength);
    //    }
    //}

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("콜리전 충돌");

        Invoke(nameof(SetIce), ResetTime);
        gameObject.SetActive(false);
        rb.useGravity = false;
        mycollider.gameObject.SetActive(false);
    }

    private void SetIce()
    {
        isDelayTime = true;
        cf.enabled = false;
        rb.isKinematic = false;
        transform.position = startpos;
        gameObject.SetActive(true);
        mycollider.gameObject.SetActive(true);

        Invoke(nameof(SetDelay), delayTIme);
    }

    private void SetDelay()
    {
        isDelayTime = false;
    }
}
