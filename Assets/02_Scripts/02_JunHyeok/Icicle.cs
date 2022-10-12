using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Icicle : MonoBehaviour
{
    [SerializeField] LayerMask layermask;
    public float ResetTime = 10f;
    public float rayLength = 1000f;
    public float gravityScale = 1f;
    public float delayTIme = 1f;

    private Vector3 endpos = Vector3.zero;
    private Vector3 startpos = Vector3.zero;

    private Rigidbody rb;
    private ConstantForce cf;
    private Collider mycollider;
    public RaycastHit hitinfo;

    private bool isCheckd = true;
    public Transform particlepos;

    private bool isDropping = false;
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
        float spehereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        if (Physics.SphereCast(startpos, spehereScale / 2f, endpos, out hitinfo, rayLength, layermask))
        {
            if (isCheckd) return;

            rb.useGravity = true;
            CheckRayCast(true);
        }
        else
        {
            if (isCheckd == false) return;

            CheckRayCast(false);
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
        if (isDropping) return;

        isDropping = true;
        if (other.gameObject.CompareTag("Player"))
        {
            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.dropIce_2, particlepos.position);
        }
        else
        {
            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.dropIce_1, particlepos.position);
        }

        Invoke(nameof(SetIce), ResetTime);

        mycollider.gameObject.SetActive(false);
        gameObject.SetActive(false);

        rb.useGravity = false;
        cf.enabled = false;
    }

    private void SetIce()
    {
        rb.isKinematic = true;

        gameObject.SetActive(true);
        mycollider.gameObject.SetActive(true);

        transform.position = startpos;

        Invoke(nameof(SetDelay), delayTIme);
    }

    private void SetDelay()
    {
        isCheckd = false;
        rb.isKinematic = false;
        isDropping = false;
    }

    private void CheckRayCast(bool _isboolen)
    {
        isCheckd = _isboolen;
        cf.enabled = _isboolen;
    }
}
