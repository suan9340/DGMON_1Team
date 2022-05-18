using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SingleTon

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    #endregion
    public GameState gameState;

    public SoundManager soundManager = null;
    public TuTorialManager tutoManager = null;

    public SoundManager Sound { get { return soundManager; } }
    public TuTorialManager Tutorial { get { return tutoManager; } }

    public void SetGameState(GameState _state)
    {
        gameState = _state;
        if (gameState == GameState.isSetting)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (gameState == GameState.isPlaying)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}
