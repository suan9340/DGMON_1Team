using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Puzzle1 : MonoBehaviour
{
    private Rigidbody myrigid;

    public List<Color> mycolors = new List<Color>();
    public List<GameObject> puzzleObj = new List<GameObject>();

    int num = 0, lastnum = 0;
    int clear = 0;

    public Color clearColor;
    public GameObject puzzleClearDoor = null;
    public ParticleSystem cleareffect = null;

    private void Awake()
    {
        myrigid = GetComponent<Rigidbody>();
        puzzleClearDoor.GetComponent<Renderer>().material.color = clearColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PuzzleCube"))
        {
            num = Random.Range(0, 4);

            if (collision.gameObject.GetComponent<Renderer>().material.color == mycolors[1]) return;
            if (lastnum == num) num = Random.Range(0, 4);

            if (num == 1)
            {
                GameManager.Instance.Sound.clearLevel.Play();
                clear++;
            }

            collision.gameObject.GetComponent<Renderer>().material.color = mycolors[num];
            lastnum = num;
            if (clear == 5)
            {
                GameManager.Instance.Sound.completeSond.Play();
                puzzleClearDoor.transform.DOMoveY(22, 1.5f);
                cleareffect.Play();
            }
        }
    }
}
