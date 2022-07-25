using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    #region SingleTon

    private static ParticleManager _instance = null;
    public static ParticleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ParticleManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("ParticleManager").AddComponent<ParticleManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    #region �������̽�

    public enum ParticleType
    {
        stairHint,
        stairDoorOpen,
    }

    public int AddParticle(ParticleType pt, Vector3 pos)
    {
        switch (pt)
        {
            case ParticleType.stairHint:
                if (false == particleDic.ContainsKey(pt))
                {
                    Debug.Log("qwe");
                    particleDic[pt] = Resources.Load<GameObject>("VFX/StairHint_VFX");
                }
                break;

            case ParticleType.stairDoorOpen:
                if (false == particleDic.ContainsKey(pt))
                {
                    particleDic[pt] = Resources.Load<GameObject>("VFX/StairOpen_VFX");
                }
                break;


            default:
                Debug.LogWarning("�������� ���� ��ƼŬ�� �ִٰ�!?!?!");
                return 0;
        }

        if (particleDic[pt] == null)
        {
            Debug.LogWarning($"�ε��� ���߾� {pt}");
            return 0;
        }

        GameObject go = Instantiate<GameObject>(particleDic[pt], pos, Quaternion.identity);

        return 0;
    }

    private Dictionary<ParticleType, GameObject> particleDic = new Dictionary<ParticleType, GameObject>();
    #endregion
}
