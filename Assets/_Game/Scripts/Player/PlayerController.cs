using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [Header("마우스 설정")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;  // 카메라 오브젝트 드래그

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // 마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MouseLook();
        Move();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 위아래 시야 제한
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라 위아래 회전
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 플레이어 좌우 회전
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        // 지면 체크
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // WASD 이동
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        // 중력
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}