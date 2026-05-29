using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    [Header("서랍 설정")]
    public float openDistance = 0.4f;    // 서랍 열리는 거리
    public float openSpeed = 3f;          // 열리는 속도
    public float interactDistance = 2f;   // 상호작용 가능 거리

    [Header("열리는 방향 (보통 Z축)")]
    public Vector3 openDirection = Vector3.forward;

    [Header("UI")]
    public GameObject interactUI;         // "E 상호작용" UI (없으면 무시)

    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Transform playerTransform;

    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + openDirection * openDistance;
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactDistance)
        {
            if (interactUI != null)
                interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                isOpen = !isOpen;
        }
        else
        {
            if (interactUI != null)
                interactUI.SetActive(false);
        }

        // 서랍 움직임
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * openSpeed
        );
    }
}