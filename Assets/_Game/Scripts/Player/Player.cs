using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] Transform cameraTransform;
    CharacterController cc;
    float verticalRotation = 0f;
=======
    [Header("이동 설정")]
    [SerializeField] float moveSpeed = 3f;      // 이동 속도
    [SerializeField] float gravity = -9.81f;    // 중력 값

    [Header("마우스 설정")]
    [SerializeField] float mouseSensitivity = 2f; // 마우스 감도
    [SerializeField] Transform cameraTransform;   // 카메라 회전용 Transform

    [Header("발소리")]
    [SerializeField] AudioClip footstepClip;      // 발소리 오디오 클립
    [SerializeField] float footstepInterval = 0.5f; // 발소리 재생 간격

    CharacterController cc;   // 캐릭터 컨트롤러
    Vector3 velocity;         // 중력/낙하 속도
    float verticalRotation = 0f; // 카메라 상하 회전 값

    float footstepTimer;      // 발소리 타이머
>>>>>>> Stashed changes

    void Start()
    {
        cc = GetComponent<CharacterController>();

        // 마우스 커서 잠금 (게임 FPS처럼)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();          // 이동 처리
        Rotate();        // 시점 회전 처리
        HandleFootstep(); // 발소리 처리
    }

    void Move()
    {
<<<<<<< Updated upstream
=======
        // 땅에 있을 때 살짝 아래로 붙여줌 (CharacterController 안정화)
        if (cc.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // WASD 입력
>>>>>>> Stashed changes
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 이동 방향 계산 (플레이어 기준)
        Vector3 move = transform.right * h + transform.forward * v;
<<<<<<< Updated upstream
        cc.SimpleMove(move * moveSpeed);
=======

        // 이동 적용
        cc.Move(move * moveSpeed * Time.deltaTime);

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
>>>>>>> Stashed changes
    }

    void Rotate()
    {
        // 마우스 좌우 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 캐릭터 좌우 회전
        transform.Rotate(0f, mouseX, 0f);

        // 카메라 상하 회전
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleFootstep()
    {
        // 이동 입력 체크
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 실제 이동 중인지 판단
        bool isMoving = (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);

        // 땅에 있고 + 이동 중이면 발소리 재생
        if (cc.isGrounded && isMoving)
        {
            footstepTimer -= Time.deltaTime;

            // 일정 시간마다 발소리 재생
            if (footstepTimer <= 0f)
            {
                SoundManager.Instance.PlayFootstep(footstepClip);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            // 멈추면 타이머 초기화
            footstepTimer = 0f;
        }
    }
}