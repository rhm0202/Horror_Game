using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    Ghost ghost;
    Collider col;

    void Awake()
    {
        ghost = GetComponentInParent<Ghost>();
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void EnableHitbox() => col.enabled = true;
    public void DisableHitbox() => col.enabled = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            ghost.OnAttackHit();
            col.enabled = false;
        }
    }
}
