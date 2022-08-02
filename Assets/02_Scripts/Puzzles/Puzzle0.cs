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
            playerDire.Play();
    }

    public void ConnectData()
    {
        const string SAVE_PATH = "SO/";
        playerData = Resources.Load<PlayerData>(SAVE_PATH + "PlayerData");
    }

    private void ShowFireLight()
    {
        lightBig.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag_Player))
        {
            if (isStayCollider) return;

            isStayCollider = true;

            if (playerData.starCnt == 0)
            {
                UIManager.Instance.StayWarning();
                Debug.Log("다시와");
            }
            else
            {
                Debug.Log("qweqew");
                playerData.isClear0 = true;

                ShowFireLight();

                door.gameObject.SetActive(false);
                playerData.starCnt = 0;

                UIManager.Instance.UpdateStarUI();
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
}
