using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("문 설정")]
    private GameObject door;
    public float openAngle = 90f;
    public float openSpeed = 3f;
    public float interactDistance = 2.5f;

    [Header("UI")]
    public GameObject interactUI;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Transform playerTransform;

    void Start()
    {
        door = this.gameObject;
        closedRotation = door.transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, door.transform.position);

        if (distance <= interactDistance)
        {
            if (interactUI != null)
                interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;
            }
        }
        else
        {
            if (interactUI != null)
                interactUI.SetActive(false);
        }

        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        door.transform.rotation = Quaternion.Lerp(
            door.transform.rotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );
    }
}