using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    Ghost ghost;

    void Awake() => ghost = GetComponentInParent<Ghost>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox") && ghost.IsAttacking)
            ghost.OnAttackHit();
    }
}
