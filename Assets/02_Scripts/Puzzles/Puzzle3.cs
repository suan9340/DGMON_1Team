using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour
{
    private MeshRenderer mashRenderer;

    private List<Material> m = new List<Material>();

    [Header("바꿀 색상")]
    public Color color;

    [Header("회전 속도")]
    public float rotSpeed = 100f;

    [Header("체크할 콜라이더박스")]
    public GameObject boxCol;

    public bool isDefaultObject = false;
    private bool isStop = false;
    private bool isCollsion = false;

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
            if (isCollsion) return;
            isCollsion = true;

            isStop = !isStop;
            if (isStop) boxCol.SetActive(true);
            else boxCol.SetActive(false);

        }
        //if (collision.collider.CompareTag(tag_Puzzle3))
        //{
        //    Debug.Log("d으아아ㅏㄱ");
        //} 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(tag_Player))
        {
            //Debug.Log("나갔어");
            isCollsion = false;
        }
    }
}
