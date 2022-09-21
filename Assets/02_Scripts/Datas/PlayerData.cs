using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerData",
menuName = "Scriptable Object/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float sensivity;
    public int starCnt;
    public List<int> needStars;
    public bool isClear0;
}

[System.Serializable]
public class NeedStarStay
{
    public int needStar;
}