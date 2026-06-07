using UnityEngine;

public class SafeRoom : MonoBehaviour
{
    [SerializeField] Ghost ghost;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ghost.SetPlayerInSafeRoom(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ghost.SetPlayerInSafeRoom(false);
    }
}
