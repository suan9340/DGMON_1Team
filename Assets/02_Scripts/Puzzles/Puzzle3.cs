using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour
{
    private MeshRenderer mashRenderer;

    private List<Material> m = new List<Material>();

    [Header("�ٲ� ����")]
    public Color color;

    [Header("ȸ�� �ӵ�")]
    public float rotSpeed = 100f;

    [Header("üũ�� �ݶ��̴��ڽ�")]
    public BoxCollider boxCol;

    public bool isDefaultObject = false;
    private bool isStop = false;

    private string tag_Player = ConstantManager.TAG_PLAYER;
    private string tag_Puzzle3 = ConstantManager.TAG_PZ3;

    private void Start()
    {
        mashRenderer = GetComponent<MeshRenderer>();

        SettingColor();
    }

    void Update()
    {
        if (isStop || isDefaultObject) return;
        else
            transform.eulerAngles += new Vector3(transform.rotation.x, rotSpeed, transform.rotation.z) * Time.deltaTime;
    }

    private void SettingColor()
    {
        mashRenderer.GetMaterials(m);
        m[0].color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(tag_Player))
        {
            isStop = !isStop;
            if (isStop) boxCol.enabled = true;
            else boxCol.enabled = false;

        }
        if (collision.collider.CompareTag(tag_Puzzle3))
        {
            Debug.Log("d���ƾƤ���");
        }
    }
}
