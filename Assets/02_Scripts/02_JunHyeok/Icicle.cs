using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] LayerMask layermask;
    [Header("?ºÏùå ?¨ÏÑ§???úÍ∞Ñ")]
    public float ResetTime = 10f;
    [Header("?àÏù¥Í∏∏Ïù¥")]
    public float rayLength = 1000f;
    [Header("Í≥†ÎìúÎ¶?Ï§ëÎ†• Í∞?")]
    public float gravityScale = 1f;
    [Header("?úÎ†à???Ä??")]
    public float delayTIme = 1f;

    private Vector3 endpos = Vector3.zero;
    private Vector3 startpos = Vector3.zero;

    private Rigidbody rb;
    public ConstantForce cf;
    public Collider mycollider;
    public RaycastHit hitinfo;

    private bool isCheckd = true;

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

            //Debug.Log("Î∂Ä?™Ìòî??);
            
            rb.useGravity = true;
            isCheckd = true;
            cf.enabled = true;
        }
        else
        {
            if (isCheckd == false) return;
            cf.enabled = false;
            //Debug.Log("??Î∂Ä?™Ìòî??);
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
        Transform pos = other.transform;
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.LevelUp, pos.position);
        Invoke(nameof(SetIce), ResetTime);
        gameObject.SetActive(false);
        rb.useGravity = false;
        cf.enabled = false;
        mycollider.gameObject.SetActive(false);
    }

    private void SetIce()
    {
        rb.isKinematic = true;
        transform.position = startpos;
        gameObject.SetActive(true);
        mycollider.gameObject.SetActive(true);

        Invoke(nameof(SetDelay), delayTIme);
    }

    private void SetDelay()
    {
        isCheckd = false;
        rb.isKinematic = false;
    }
}
