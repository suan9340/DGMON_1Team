using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] LayerMask layermask;
    [Header("얼음 재설정 시간")]
    public float ResetTime = 10f;
<<<<<<< HEAD
    public float rayLength = 1000f;
=======
    [Header("레이길이")]
    public float rayLength = 1000f;
    [Header("고드름 중력 값")]
    public float gravityScale = 1f;
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0

    private Vector3 endpos = Vector3.zero;
    private Vector3 startpos = Vector3.zero;

    private Rigidbody rb;
<<<<<<< HEAD
    private Collider mycollider;
=======
    private Collider collider;
    public RaycastHit hitinfo;
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0

    private bool isCheckd = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
<<<<<<< HEAD
        mycollider = GetComponentInChildren<Collider>();
=======
        collider = GetComponent<Collider>();
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0

        endpos = transform.TransformDirection(Vector3.down);
        startpos = transform.position;

        isCheckd = true;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        Debug.DrawRay(startpos, endpos * rayLength, Color.blue);
        CheckRay();
    }

    private void CheckRay()
    {
        float capsuleScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);

        if (Physics.CapsuleCast(startpos, startpos, capsuleScale / 2f, endpos, out RaycastHit hitinfo, rayLength, layermask))
=======
        RayCast();
    }

    private void RayCast()
    {
        float spehereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        if (Physics.SphereCast(startpos, spehereScale / 2f, endpos, out hitinfo, rayLength, layermask))
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0
        {
            if (isCheckd) return;
            rb.useGravity = true;
            isCheckd = true;
<<<<<<< HEAD
            Debug.Log("인식했다고 tlqkf.");
=======
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0
        }
        else
        {
            if (isCheckd == false) return;
            isCheckd = false;
<<<<<<< HEAD
            Debug.Log("인식안됐다고 tlqkf.");
=======
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
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        rb.useGravity = false;
<<<<<<< HEAD
        mycollider.gameObject.SetActive(false);
=======
        collider.gameObject.SetActive(false);
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0

        Invoke(nameof(SetIce), ResetTime);
    }

    private void SetIce()
    {
        rb.isKinematic = false;
        transform.position = startpos;
        gameObject.SetActive(true);
<<<<<<< HEAD
        mycollider.gameObject.SetActive(true);
=======
        collider.gameObject.SetActive(true);
>>>>>>> f328991c23c84d0198bb668cac3906e4e9b54af0
    }
}
