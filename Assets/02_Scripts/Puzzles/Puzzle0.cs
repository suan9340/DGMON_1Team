using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Puzzle0 : MonoBehaviour
{
    [Header("��ȣ�ۿ� �� ���� ����Ʈ")]
    public ParticleSystem lightBig = null;

    [Header("��ȣ�ۿ� �� ���� ��")]
    public GameObject door = null;

    private PlayerData playerData;

    private void Start()
    {
        ConnectData();
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
        if (other.CompareTag(ConstantManager.TAG_PLAYER))
        {
            if (playerData.starCnt == 0)
            {
                Debug.Log("�ٽÿ�");
            }
            else
            {
                Debug.Log("qweqew");
                playerData.starCnt = 0;
                playerData.isClear0 = true;


                ShowFireLight();

                door.gameObject.SetActive(false);
                UIManager.Instance.UpdateStarUI();
            }
        }
    }
}
