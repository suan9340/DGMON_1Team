using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TPSController : MonoBehaviour
{
    [Header("------Transform------")]
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform cameraArm;

    [Header("------PlayerData------")]
    [SerializeField] private PlayerData playerData;

    [Header("------UI Data------")]
    public Slider gazeSlider = null;
    [Range(0f, 0.5f)] public float gazeSpeed = 1f;
    [Range(0f, 0.5f)] public float gazeMinusSpeed = 1f;
    public Image fillImage = null;
    public List<Color> fillColors = new List<Color>();

    private float speed;

    private bool isRun;
    private bool jDown;
    private bool isJump;

    private bool isCantrun;

    Vector3 moveDir = Vector3.zero;

    private Rigidbody myrigid;
    private Animator myanim;

    public Ease ease;

    private CharacterController controller;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        ConnectData();

        myrigid = GetComponent<Rigidbody>();
        myanim = GetComponentInChildren<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }



    private void Update()
    {
        //UISet();
        if (GameManager.Instance.gameState == GameState.isSetting)
            return;
        PlayGameSet();
    }

    public void ConnectData()
    {
        const string SAVE_PATH = "SO/";
        playerData = Resources.Load<PlayerData>(SAVE_PATH + "PlayerData");

    }

    // GameSetting
    private void PlayGameSet()
    {
        //Jump();
        //LookAround();
        Move();
        //Move2();
        SpeedControl();
    }

    private void UISet()
    {
        SettingGazeSlider();
    }

    /// <summary>
    /// 플레이어의 이동
    /// </summary>
    private void Move()
    {
        isRun = Input.GetKey(KeyCode.LeftShift);
        Vector2 moveInput = new Vector2(Input.GetAxis(ConstantManager.MV_HO), Input.GetAxis(ConstantManager.MV_VE));
        bool isMove = moveInput.magnitude != 0;
        myanim.SetBool("isWalk", isMove);

        if (isMove)
        {
            Vector3 lookForWard = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForWard * moveInput.y + lookRight * moveInput.x;

            characterBody.forward += moveDir;

            transform.position += moveDir * speed * Time.deltaTime;
        }

        //controller.Move(moveDir * speed * Time.deltaTime);
        //moveDir.y += Physics.gravity.y * Time.deltaTime;
        if (isCantrun)
        {
            myanim.SetBool("isRun", false);
            return;
        }
        myanim.SetBool("isRun", isRun);
    }


    private void Move2()
    {
        var _ho = Input.GetAxis(ConstantManager.MV_HO);
        var _ve = Input.GetAxis(ConstantManager.MV_VE);

        moveDir = new Vector3(_ho, 0, _ve) * speed;

        if (controller.isGrounded)
        {
            if (Input.GetButton(ConstantManager.MV_JP))
                moveDir.y = playerData.jumpPower;
        }

        moveDir.y += Physics.gravity.y * Time.deltaTime * playerData.gravitySpeed;
        moveDir = transform.TransformDirection(moveDir);
        controller.Move(moveDir * Time.deltaTime);
    }


    /// <summary>
    /// 플레이어가 뛰는지, 걷는지 속도 구분
    /// </summary>
    private void SpeedControl()
    {
        if (isRun && isCantrun == false)
        {
            speed = playerData.runSpeed;
            //CountSlider();
        }
        else
            speed = playerData.normalSpeed;
    }


    /// <summary>
    /// 플레이어를 점프해주는 함수
    /// </summary>
    private void Jump()
    {
        jDown = Input.GetButtonDown("Jump");
        if (jDown && !isJump)
        {
            var _current = transform.localPosition;
            SoundManager.Instance.Sound_PlayerJump();
            //myrigid.AddForce(Vector3.up * playerData.jumpPower, ForceMode.Impulse);
            DOTween.To(() => transform.localPosition, x => transform.localPosition = x, new Vector3(_current.x, _current.y + 2, _current.z), 1f)
                .SetEase(ease).SetLoops(1, LoopType.Yoyo);

            myanim.SetBool("isJump", true);
            myanim.SetTrigger("doJump");
            isJump = true;
        }
    }


    /// <summary>
    /// 플레이어가 땅에 닿았을 때 점프 활성화
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Switch") || collision.gameObject.CompareTag("PuzzleCube"))
        {
            myanim.SetBool("isJump", false);
            isJump = false;
        }
    }


    // Input User MousePosition and rotate Camera 
    private void LookAround()
    {
        Vector2 _mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * playerData.sensivity;
        Vector3 _cameraAngle = cameraArm.rotation.eulerAngles;
        float _x = _cameraAngle.x - _mouseDelta.y;

        if (_x < 180f)
        {
            _x = Mathf.Clamp(_x, -1, 70f);
        }
        else
        {
            _x = Mathf.Clamp(_x, 345f, 360f);
        }

        cameraArm.rotation = Quaternion.Euler(_x, _cameraAngle.y + _mouseDelta.x, _cameraAngle.z); ;
    }


    // Plus Player Gaze(Setting GazeUI)
    private void SettingGazeSlider()
    {
        if (isRun) return;
        if (isCantrun)
        {
            SettingSliderColor(2);
            if (gazeSlider.value >= 1)
            {
                SettingSliderColor(0);
                isCantrun = false;
            }
        }

        if (gazeSlider == null) return;
        if (gazeSlider.value >= 1) return;

        gazeSlider.value += Time.deltaTime * gazeSpeed;
    }

    // if PlayerSlider is low, change Color and can't run
    private void CountSlider()
    {
        gazeSlider.value -= gazeMinusSpeed;
        if (gazeSlider.value <= 0.5f)
        {
            SettingSliderColor(1);
        }
        else
            SettingSliderColor(0);

        if (gazeSlider.value <= 0)
        {
            isCantrun = true;
            return;
        }
    }

    // Change Slider Color with Values
    private void SettingSliderColor(int _num)
    {
        fillImage.color = fillColors[_num];
    }
    // Setting GameManager GameState
}
