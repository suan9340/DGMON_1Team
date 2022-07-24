using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private bool isWalk;
    private bool jDown;
    private bool isJump;

    private bool isCantrun;

    Vector3 moveDir = Vector3.zero;

    private Rigidbody myrigid;
    private Animator myanim;

    private void Awake()
    {
        ConnectData();

        myrigid = GetComponent<Rigidbody>();
        myanim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    private void Update()
    {
        UISet();
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
        LookAround();
        Move();
        SpeedControl();
        Jump();
    }

    private void UISet()
    {
        SettingGazeSlider();
    }

    private void Move()
    {
        isRun = Input.GetKey(KeyCode.LeftShift);
        isWalk = Input.GetKey(KeyCode.LeftControl);
        Vector2 moveInput = new Vector2(Input.GetAxis(ConstantManager.MV_HO), Input.GetAxis(ConstantManager.MV_VE));
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            Vector3 lookForWard = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForWard * moveInput.y + lookRight * moveInput.x;

            characterBody.forward += moveDir;

            transform.position += moveDir * speed * Time.deltaTime;
        }

        myanim.SetBool("isWalk", isMove);
        myanim.SetBool("isSlowWalk", isWalk);
        if (isCantrun)
        {
            myanim.SetBool("isRun", false);
            return;
        }
        myanim.SetBool("isRun", isRun);
    }


    // run, walk, slow walk ... Control Player Speed
    private void SpeedControl()
    {
        if (isRun && isCantrun == false)
        {
            speed = playerData.runSpeed;
            CountSlider();
        }
        else if (isWalk)
            speed = playerData.walkSpeed;
        else
            speed = playerData.normalSpeed;
    }


    // Player jump
    private void Jump()
    {
        jDown = Input.GetButtonDown("Jump");
        if (jDown && !isJump)
        {
            SoundManager.Instance.Sound_PlayerJump();
            myrigid.AddForce(Vector3.up * playerData.jumpPower, ForceMode.Impulse);
            myanim.SetBool("isJump", true);
            myanim.SetTrigger("doJump");
            isJump = true;
        }
    }


    // if player on the floor, change can jump
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
