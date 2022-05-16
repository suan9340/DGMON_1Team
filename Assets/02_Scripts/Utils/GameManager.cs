using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    public SoundManager soundManager = null;
    public SoundManager Sound { get { return soundManager; } }

}
