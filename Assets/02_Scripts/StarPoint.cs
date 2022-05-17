using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StarPoint : MonoBehaviour
{
    public Text starPointTxt;
    public Text nestarPointTxt;

    [Header("모은별빛조각")] public int starPoint;
    [Header("모자란별빛조각")] public int nestarPoint;
    [Header("필요한별빛조각")] public int needStar = 5;
    public GameObject gameNonclearObj;

    public List<GameObject> foundObjects;
    public GameObject StageClear;
    public string tagName;
    public float shortDis;

    public GameObject starFirstObj;

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
                    Debug.Log("스테이지 클리어");
                    break;
                }
                if (starPoint < 5)
                {
                    StartCoroutine(PrintNoneClear(true));
                    nestarPointTxt.text = $"필요한 별 조각 : {needStar}개\n별조각 {nestarPoint}개가 부족합니다.";
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
            GameManager.Instance.Sound.getCoin.Play();
            StarPoint starpoint = other.GetComponent<StarPoint>();
            starPoint++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("StarFirst"))
        {
            GameManager.Instance.Sound.getCoin.Play();
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
        starPointTxt.text = $"★ : {starPoint}";
    }

    private IEnumerator PrintImg()
    {
        GameManager.Instance.gameState = GameState.isSetting;
        starFirstObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.gameState = GameState.isPlaying;
        starFirstObj.SetActive(false);
        yield return null;
    }
}
