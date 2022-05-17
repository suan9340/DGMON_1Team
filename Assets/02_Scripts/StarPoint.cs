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

    public GameObject gameNonclearObj;

    public List<GameObject> foundObjects;
    public GameObject StageClear;
    public string tagName;
    public float shortDis;
    public GameObject StarClear;
    [Header("�ʿ��Ѻ�������")] public int needStar = 5;

    public GameObject stageClear;
    private float time;

    public GameObject starFirst;

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
                    StartCoroutine(PrintNoneClear(true));
                    nestarPointTxt.text = "�ʿ��� �� ���� : " + needStar + "��\n" + "������ " + nestarPoint + "���� �����մϴ�";
                }
            }
        }
        if (shortDis >= 3.5)
        {
            if (gameNonclearObj.activeSelf == false) return;
            gameNonclearObj.SetActive(false);
        }
    }

    void StarFirst()
    {
        StartCoroutine(PrintImg());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StarPoint"))
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

    private IEnumerator PrintNoneClear(bool isActicve)
    {
        gameNonclearObj.SetActive(isActicve);
        yield return new WaitForSeconds(1f);
        gameNonclearObj.SetActive(!isActicve);

        yield return null;
    }
    void StarPointText()
    {
        starPointTxt.text = "�� : " + starPoint;
    }

    private IEnumerator PrintImg()
    {
        starFirst.SetActive(true);
        yield return new WaitForSeconds(2f);
        starFirst.SetActive(false);
        yield return null;
    }
}
