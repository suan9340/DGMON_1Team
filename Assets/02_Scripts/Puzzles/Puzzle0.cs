using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class Puzzle0 : MonoBehaviour
{
    [Header("상호작용 시 켜질 라이트")]
    public ParticleSystem lightBig = null;

    [Header("상호작용 시 열릴 문")]
    public GameObject door = null;

    [Header("촛불 켜지기 전 이펙트")]
    public ParticleSystem lightbeforeEffect = null;

    [Header("시네머신")]
    public PlayableDirector playerDire;

    [Header("시네머신 카메라와 메인 카메라")]
    public Camera cinemacineCam = null;
    public Camera mainCam = null;


    [Header("오디오 소스")]
    public AudioSource audio = null;

    private bool isStayCollider = false;
    private string tag_Player = ConstantManager.TAG_PLAYER;

    private PlayerData playerData;

    private void Start()
    {
        ConnectData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartStay();
        }
    }

    public void ConnectData()
    {
        const string SAVE_PATH = "SO/";
        playerData = Resources.Load<PlayerData>(SAVE_PATH + "PlayerData");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag_Player))
        {
            if (playerData.isClear0) return;

            if (isStayCollider) return;

            isStayCollider = true;

            if (playerData.starCnt == 0)
            {
                UIManager.Instance.StayWarning();
                Debug.Log("다시와");
            }
            else
            {
                StartStay();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tag_Player))
        {
            isStayCollider = false;
        }
    }

    private void StartStay()
    {
        audio.volume = PlayerPrefs.GetFloat(ConstantManager.VOL_BGM, 1f);

        GameManager.Instance.SetGameState(GameState.isSetting);
        mainCam.gameObject.SetActive(false);
        playerDire.Play();
        playerData.isClear0 = true;
        playerData.starCnt = 0;
        UIManager.Instance.UpdateStarUI();

        door.gameObject.SetActive(false);
    }

    public void TimeLineEnd()
    {
        cinemacineCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);

        UIManager.Instance.StaySucessful();
    }
}
