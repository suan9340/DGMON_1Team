using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerData",
menuName = "Scriptable Object/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float sensivity;
    public int starCnt;
}
