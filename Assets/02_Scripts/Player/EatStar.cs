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
            //if (isGet) return;
            //isGet = true;
            playerData.starCnt++;

            UIManager.Instance.UpdateStarUI();
            Destroy(other.gameObject);

            var starPos = other.transform.position;
            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.startPointEat, starPos);
            SoundManager.Instance.Sound_GetStar();


            Debug.Log("qwe");
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
