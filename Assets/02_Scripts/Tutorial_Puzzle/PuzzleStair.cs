using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStair : MonoBehaviour
{
    public int num = 0;
    public int stairnum;

    public List<Color> mycolors = new List<Color>();
    private Material material;

    public ParticleSystem stairEffect = null;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = mycolors[num];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlusNum();
        }
    }

    private void PlusNum()
    {
        if (num >= mycolors.Count - 1)
        {
            num = 0;
        }
        else
        {
            num++;
        }
        material.color = mycolors[num];

        if (num == 2)
        {
            CheckStairNum(true);
            stairEffect.Play();
            GameManager.Instance.Sound.clearLevel.Play();
        }
        else
            CheckStairNum(false);

        GameManager.Instance.Tutorial.CheckOpenDoor();
    }


    // 2°¡ Åë°ú
    private void CheckStairNum(bool isA)
    {
        switch (stairnum)
        {
            case 0:
                GameManager.Instance.Tutorial.isDone[0] = isA;
                break;

            case 1:
                GameManager.Instance.Tutorial.isDone[1] = isA;
                break;

            case 2:
                GameManager.Instance.Tutorial.isDone[2] = isA;
                break;

            case 3:
                GameManager.Instance.Tutorial.isDone[3] = isA;
                break;

            case 4:
                GameManager.Instance.Tutorial.isDone[4] = isA;
                break;

            case 5:
                GameManager.Instance.Tutorial.isDone[5] = isA;
                break;
        }
    }
}
