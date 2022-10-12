using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icy_Road : MonoBehaviour
{
    [SerializeField] public bool isBreaked = false;
    public MeshRenderer meshRenderer;
    public Material[] mat;
    [SerializeField] LayerMask layerMask;

    [Header("오브젝트 재생성 시간")]
    public float resetTime = 2f;

    [Header("오브젝트 깨지기 준비 시간")]
    public float readyBreakTime = 1.5f;

    private bool isCollider = false;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isCollider) return;
            isCollider = true;
            Debug.Log("Collider!!!!!!!!!");

            meshRenderer.material = mat[1];
            Invoke(nameof(StartBreaking), readyBreakTime);
        }
    }

    private void StartBreaking()
    {
        isBreaked = true;
        gameObject.SetActive(false);

        Invoke(nameof(ResetBreaking), resetTime);
    }

    private void ResetBreaking()
    {
        meshRenderer.material = mat[0];
        gameObject.SetActive(true);
        isBreaked = false;
        isCollider = false;
    }
}
