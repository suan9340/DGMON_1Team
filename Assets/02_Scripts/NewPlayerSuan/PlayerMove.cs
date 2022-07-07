using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 6f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float _horizontal = Input.GetAxisRaw(ConstantManager.MV_HO);
        float _vertical = Input.GetAxisRaw(ConstantManager.MV_VE);

        Vector3 _dir = new Vector3(_horizontal, 0, _vertical);

        if (_dir.magnitude >= 0.1f)
        {
            controller.Move(_dir * speed * Time.deltaTime);
        }
    }
}
