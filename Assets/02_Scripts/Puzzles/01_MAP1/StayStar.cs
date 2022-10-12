using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class StayStar : MonoBehaviour
{
    [Header("��ȣ�ۿ� �� ���� ����Ʈ")]
    public ParticleSystem lightBig = null;

    [Header("��ȣ�ۿ� �� ���� ��")]
    public GameObject door = null;

    [Header("�к� ������ �� ����Ʈ")]
    public ParticleSystem lightbeforeEffect = null;

    [Header("�ó׸ӽ�")]
    public PlayableDirector[] playerDire;

    [Header("�ó׸ӽ� ī�޶�� ���� ī�޶�")]
    public Camera cinemacineCam = null;
    public Camera mainCam = null;


    [Header("����� �ҽ�")]
    public new AudioSource audio = null;

    [Header("�⵵�� ����")]
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
                Debug.Log("�ٽÿ�");
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
