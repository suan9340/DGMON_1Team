using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    Camera cam;
    CharacterController controller;

    public float speed = 5f;
    public float runSpeed = 8f;
    public float finalSpeed;

    public bool toggleCameraRotation;
    public bool run;

    public float smoothness = 10f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
            toggleCameraRotation = true;
        else
            toggleCameraRotation = false;

        if (Input.GetKey(KeyCode.LeftShift))
            run = true;
        else
            run = false;

        InputMovement();
    }

    private void LateUpdate()
    {
        if (toggleCameraRotation /*!= true*/)
        {
            Vector3 playerRotate = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }

    private void InputMovement()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxis(ConstantManager.MV_VE) + right * Input.GetAxis(ConstantManager.MV_HO);
        controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);
    }
}
