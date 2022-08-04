using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStair : MonoBehaviour
{
    private int stairColor_num = 0;
    public int stairnum;

    public List<Color> mycolors = new List<Color>();
    private Material material;

    public ParticleSystem stairEffect = null;

    private bool isOn = false;

    private int clearNum;

    private void Start()
    {
        clearNum = mycolors.Count - 1;

        material = GetComponent<Renderer>().material;
        //material.color = mycolors[stairColor_num];

        SetRandomStairColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlusNum();
        }
    }

    private void SetRandomStairColor()
    {
        stairColor_num = Random.Range(0, mycolors.Count - 2);
        UpdateStairColor();
    }

    private void PlusNum()
    {
        if (isOn) return;
        
        stairColor_num++;
        UpdateStairColor();

        if (stairColor_num == clearNum)
        {
            isOn = true;
            stairEffect.Play();
            SoundManager.Instance.Sound_Stair_Hint();
            CheckStairNum(true);

            TuTorialManager.Instance.CheckOpenDoor();
            
        }
        else
            CheckStairNum(false);
    }


    // 2°¡ Åë°ú
    private void CheckStairNum(bool isA)
    {
        switch (stairnum)
        {
            case 0:
                TuTorialManager.Instance.isDone[0] = isA;
                break;

            case 1:
                TuTorialManager.Instance.isDone[1] = isA;
                break;

            case 2:
                TuTorialManager.Instance.isDone[2] = isA;
                break;

            case 3:
                TuTorialManager.Instance.isDone[3] = isA;
                break;

            case 4:
                TuTorialManager.Instance.isDone[4] = isA;
                break;

            case 5:
                TuTorialManager.Instance.isDone[5] = isA;
                break;
        }
    }   

    private void UpdateStairColor()
    {
        material.color = mycolors[stairColor_num];
    }
}
