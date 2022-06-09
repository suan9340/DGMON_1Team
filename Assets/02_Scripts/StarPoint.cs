using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StarPoint : MonoBehaviour
{
    public Text starPointTxt;
    public Text nestarPointTxt;

    public int starPoint;
    public int nestarPoint;
    public int needStar = 5;
    public GameObject gameNonclearObj;

    public List<GameObject> foundObjects;
    public GameObject StageClear;
    public string tagName;
    public float shortDis;

    public GameObject starFirstObj;

    public GameObject starGuide2;


    void Update()
    {
        if (starPoint >= needStar)
        {
            starGuide2.SetActive(true);
        }
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
                if (starPoint >= needStar)
                {
                    starPoint += -needStar;
                    GetComponent<StarClear>().enabled = true;
                    Destroy(StageClear);
                    GameManager.Instance.UI.EndGame();

                    StopAllCoroutines();
                    break;
                }
                if (starPoint < needStar)
                {
                    StartCoroutine(PrintNoneClear(true));
                    nestarPointTxt.text = $"?„ìš”??ë³?ì¡°ê° : {needStar}ê°?në³„ì¡°ê°?{nestarPoint}ê°œê? ë¶€ì¡±í•©?ˆë‹¤.";
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
        //if (other.tag == "StarFirst")
        //{
        //    StarPoint starFirst = other.GetComponent<StarPoint>();
        //    starPoint++;
        //    StarPointText();
        //    Destroy(other.gameObject);
        //}
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
        starPointTxt.text = $"??: {starPoint}";
    }

    private IEnumerator PrintImg()
    {
        GameManager.Instance.SetGameState(GameState.isSetting);
        starFirstObj.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        GameManager.Instance.SetGameState(GameState.isPlaying);
        starFirstObj.SetActive(false);
        yield return null;
    }
}
