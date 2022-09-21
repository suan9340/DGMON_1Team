using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatStar : MonoBehaviour
{
    private string tag_star = ConstantManager.TAG_STAR;

    private bool isGet = false;

    private PlayerData playerData;

    private void Start()
    {
        ConnectData();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag_star))
        {
            playerData.starCnt++;
            UIManager.Instance.EatStarUI();

            Destroy(other.gameObject);
            var starPos = other.transform.position;
            UIManager.Instance.UpdateStarUI();

            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.startPointEat, starPos);
            SoundManager.Instance.Sound_GetStar();
        }
    }


    /// <summary>
    /// Connect Scriptable Object (PlayerData)
    /// </summary>
    public void ConnectData()
    {
        const string SAVE_PATH = "SO/";
        playerData = Resources.Load<PlayerData>(SAVE_PATH + "PlayerData");

    }
}
