using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    public enum StarTypeState { One, Two, Three, }
    public StarTypeState StarState = StarTypeState.Three;

    private void Update()
    {
        
    }
    public void OnClickMoveScence()
    {
        SceneManager.LoadScene("JunHuk");
    }
    private void StarStateController()
    {
        switch (StarState)
        {
            case StarTypeState.One:
                Debug.Log("1Ÿ��");
                break;
            case StarTypeState.Two:
                Debug.Log("2Ÿ��");

                break;
        }
    }
}
