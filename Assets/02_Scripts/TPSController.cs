using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPSController : MonoBehaviour
{
    [Header("------위치 값------")]
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform cameraArm;

    [Header("------수치들------")]
    [Header("이동속도")][Range(0f, 20f)] public float normalSpeed = 5f;
    [Header("달리기속도")][Range(0f, 20f)] public float runSpeed = 8f;
    [Header("걷는속도")][Range(0f, 20f)] public float walkSpeed = 8f;
    [Header("감도")][Range(0f, 10f)] public float sensivity;
    [Header("점프 힘")][Range(0f, 10f)] public float jumpPower = 2f;

    [Header("------UI 관련된------")]
    public Slider gazeSlider = null;
    [Range(0f, 1f)] public float gazeSpeed = 2f;
    [Header("게이지 슬라이더 색상 오브제")] public Image fillImage = null;
    public List<Color> fillColors = new List<Color>();

    private float speed;

    private bool isRun;
    private bool isWalk;
    private bool jDown;
    private bool isJump;

    Vector3 moveDir = Vector3.zero;

    private Rigidbody myrigid;
    private Animator myanim;

    private void Awake()
    {
        myrigid = GetComponent<Rigidbody>();
        myanim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        UISet();
        if (GameManager.Instance.gameState == GameState.isSetting)
            return;
        PlayGameSet();
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
        myanim.SetBool("isRun", isRun);
    }


    // run, walk, slow walk ... Control Player Speed
    private void SpeedControl()
    {
        if (isRun)
        {
            CountSlider();
            speed = runSpeed;
        }
        else if (isWalk)
            speed = walkSpeed;
        else
            speed = normalSpeed;
    }


    // Player jump
    private void Jump()
    {
        jDown = Input.GetButtonDown("Jump");
        if (jDown && !isJump)
        {
            myrigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
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
        Vector2 _mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensivity;
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
        if (gazeSlider == null) return;
        if (gazeSlider.value >= 1) return;
        gazeSlider.value += Time.deltaTime * gazeSpeed;
    }


    // if PlayerSlider is low, change Color and can't run
    private void CountSlider()
    {
        if (gazeSlider == null) return;
        gazeSlider.value -= 0.001f;

        if (gazeSlider.value <= 0.5f)
        {
            if (gazeSlider.value <= 0.3f)
                SettingSliderColor(2);
            SettingSliderColor(1);
        }
        else
            SettingSliderColor(0);

        // gazeSlider.value <=0 일 때 못뛰게 하기
        if (gazeSlider.value <= 0)
        {

            return;
        }


        
    }


    // Change Slider Color with Values
    private void SettingSliderColor(int _num)
    {
        fillImage.color = fillColors[_num];
    }
    // Setting GameManager GameState
    public void SetGameState(GameState _state)
    {
        GameManager.Instance.gameState = _state;
    }
}
