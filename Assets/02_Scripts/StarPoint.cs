using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPoint : MonoBehaviour
{
    public Text starPointTxt;
    [Header("������������")] public int starPoint;
    public Text nestarPointTxt;
    [Header("���ڶ���������")] public int nestarPoint;

    public GameObject gameNonclear;
    private bool gameNonClear = false;

    public List<GameObject> foundObjects;
    public GameObject StageClear;
    public string tagName;
    public float shortDis;
    public GameObject StarClear;
    [Header("�ʿ��Ѻ�������")] public int needStar = 5;

    public GameObject stageClear;
    private float time;

    public GameObject starFirst;
    private bool starfirst = false;

    void Update()
    {
        nestarPoint = needStar - starPoint;

        StarPointText();

        foundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tagName));
        shortDis = Vector3.Distance(gameObject.transform.position, foundObjects[0].transform.position);

        StageClear = foundObjects[0];


        foreach (GameObject found in foundObjects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
            if (distance < shortDis)
            {
                StageClear = found;
                shortDis = distance;
            }
            if (shortDis < 3.5 && Input.GetKeyDown(KeyCode.F))
            {
                if (starPoint >= 5)
                {
                    starPoint += -needStar;
                    GetComponent<StarClear>().enabled = true;
                    Destroy(StageClear);
                    Debug.Log("�������� Ŭ����");
                    break;
                }
                if (starPoint < 5)
                {
                    nestarPointTxt.text = "�ʿ��� �� ���� : " + needStar + "��\n" + "������ " + nestarPoint + "���� �����մϴ�";
                    NonClear();
                }
            }

        }
        if (shortDis >= 3.5)
        {
            gameNonclear.SetActive(false);
            gameNonClear = false;
        }
    }

    void NonClear()
    {
        gameNonclear.SetActive(true);
        gameNonClear = true;
    }

    void StarFirst()
    {
        starFirst.SetActive(true);
        starfirst = true;
        //StarFirst Text timer
        time = 5f;
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0)
        {
            starFirst.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StartPoint"))
        {
            StarPoint starpoint = other.GetComponent<StarPoint>();
            starPoint++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("StarFirst"))
        {
            StarFirst();
            StarPoint starFirst = other.GetComponent<StarPoint>();
            starPoint++;
            Destroy(other.gameObject);
        }
    }

    void StarPointText()
    {
        starPointTxt.text = "�� : " + starPoint;
    }
}
