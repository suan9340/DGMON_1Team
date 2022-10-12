using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class StayStar : MonoBehaviour
{
    [Header("상호작용 시 켜질 라이트")]
    public ParticleSystem lightBig = null;

    [Header("상호작용 시 열릴 문")]
    public GameObject door = null;

    [Header("촛불 켜지기 전 이펙트")]
    public ParticleSystem lightbeforeEffect = null;

    [Header("시네머신")]
    public PlayableDirector[] playerDire;

    [Header("시네머신 카메라와 메인 카메라")]
    public Camera cinemacineCam = null;
    public Camera mainCam = null;


    [Header("오디오 소스")]
    public new AudioSource audio = null;

    [Header("기도원 순서")]
    public int num = 0;

    private float bgmvol;

    private bool isStayCollider = false;
    public bool isStaySucess = false;

    private string tag_Player = ConstantManager.TAG_PLAYER;

    private PlayerData playerData;

    private readonly WaitForSeconds soundDelay = new WaitForSeconds(0.1f);

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
            if (isStaySucess) return;

            if (isStayCollider) return;

            isStayCollider = true;

            if (playerData.starCnt >= playerData.needStars[num])
            {
                StartStay();
            }
            else
            {
                UIManager.Instance.StayWarning();
                Debug.Log("다시와");
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
        SetBGM();

        GameManager.Instance.SetGameState(GameState.isSetting);

        cinemacineCam.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
        playerDire[num].Play();

        playerData.isClear0 = true;
        isStaySucess = true;

        playerData.starCnt = 0;
        UIManager.Instance.UpdateStarUI();

        if (door == null) return;
        door.gameObject.SetActive(false);
    }

    public void TimeLineEnd()
    {
        Debug.Log("TimeLineEnd");

        SoundManager.Instance.BgmAudio.UnPause();

        cinemacineCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);

        UIManager.Instance.StaySucessful();

        lightBig.gameObject.SetActive(true);
        lightBig.Play();
    }

    private void SetBGM()
    {
        bgmvol = PlayerPrefs.GetFloat(ConstantManager.VOL_BGM, 1f);

        SoundManager.Instance.BgmAudio.Pause();

        StartCoroutine(FadeInSound());
    }

    private IEnumerator FadeInSound()
    {
        audio.volume = 0f;
        while (true)
        {
            if (audio.volume < bgmvol)
            {
                audio.volume += 0.03f;
                yield return soundDelay;
            }
            else
            {
                audio.volume = bgmvol;
                yield break;
            }
        }
    }
}
