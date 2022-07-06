using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum A
{
    d,
    f,
}

public class EffectManager : MonoBehaviour
{
    #region SingleTon   

    private static EffectManager _instance = null;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EffectManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("EffectManager").AddComponent<EffectManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public enum EffectType
    {
        FloorEffect,
    }

    //public ParticleSystem floorEffectPrefab = null;

    private void Start()
    {

    }

    /// <summary>
    /// ����Ʈ�� �ε��ϴ� ��
    /// </summary>
    /// <param name="_type"></param>
    public void AddEffect(EffectType _type)
    {
        var _name = "VFX/";

        switch (_type)
        {
            case EffectType.FloorEffect:
                //if (floorEffectPrefab == null)
                //{
                //    floorEffectPrefab = Resources.Load<ParticleSystem>(_name + "floor_EF");
                //}

                break;
        }
    }


    /// <summary>
    /// �������� ����Ʈ ������ ���⼭!
    /// </summary>
    /// <param name="_effectType"></param>
    /// <param name="_pos"></param>
    /// <param name="_normal"></param>
    public void PlayHitEffect(EffectType _effectType, Vector3 _pos, Vector3 _normal)
    {

    }
}
