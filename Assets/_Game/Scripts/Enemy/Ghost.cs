using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private enum GhostState
    {
        Patrol,
        Chase,
        Attack
    }

    [Header("순찰 지점")]
    [SerializeField] private Transform[] waypoints;

    [Header("플레이어")]
    [SerializeField] private Transform player;

    [Header("이동 설정")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 3.5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float arrivalDistance = 0.2f;
    [SerializeField] private float waitTime = 1f;

    [Header("공격 설정")]
    [SerializeField] private float attackDistance = 1.2f;
    [SerializeField] private float hitDistance = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float gameOverDelay = 0.5f;

    [Header("애니메이션")]
    [SerializeField] private Animator nurseAnimator;

    private GhostState currentState = GhostState.Patrol;
    public bool IsAttacking => currentState == GhostState.Attack && attackTimer > 0f;
    private SphereCollider detectionCollider;
    private float baseDetectionRadius;

    private int currentWaypointIndex;
    private int waypointDirection = 1;
    private float waitTimer;
    private float attackTimer;

    private bool isWaiting;
    private bool playerInDetectionRange;
    private bool playerInSafeRoom;

    private static readonly int IsMovingHash =
        Animator.StringToHash("IsMoving");

    private static readonly int IsRunningHash =
        Animator.StringToHash("IsRunning");

    private static readonly int AttackHash =
        Animator.StringToHash("Attack");

    private void Awake()
    {
        detectionCollider = GetComponent<SphereCollider>();
        if (detectionCollider != null)
            baseDetectionRadius = detectionCollider.radius;

        if (nurseAnimator == null)
            nurseAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoint가 등록되지 않았습니다.");
            enabled = false;
            return;
        }

        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (player == null)
        {
            Debug.LogError("Player를 찾지 못했습니다.");
            enabled = false;
            return;
        }

        if (nurseAnimator == null)
        {
            Debug.LogError("Nurse Animator가 등록되지 않았습니다.");
            enabled = false;
            return;
        }

        SetMovementAnimation(false, false);
    }

    private void Update()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        switch (currentState)
        {
            case GhostState.Patrol:
                UpdatePatrol();
                break;

            case GhostState.Chase:
                UpdateChase();
                break;

            case GhostState.Attack:
                UpdateAttack();
                break;
        }
    }

    private void UpdatePatrol()
    {
        if (playerInDetectionRange && !playerInSafeRoom)
        {
            currentState = GhostState.Chase;
            SetDetectionRange(true);
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        if (targetWaypoint == null)
        {
            SetMovementAnimation(false, false);
            return;
        }

        Vector3 targetPosition = targetWaypoint.position;
        targetPosition.y = transform.position.y;

        float distance = Vector3.Distance(
            transform.position,
            targetPosition
        );

        if (distance <= arrivalDistance)
        {
            WaitAtWaypoint();
        }
        else
        {
            MoveTowardsTarget(
                targetPosition,
                patrolSpeed,
                false
            );
        }
    }

    private void UpdateChase()
    {
        if (!playerInDetectionRange || playerInSafeRoom)
        {
            currentState = GhostState.Patrol;
            SetDetectionRange(false);
            SetMovementAnimation(false, false);
            return;
        }

        Vector3 targetPosition = player.position;
        targetPosition.y = transform.position.y;

        float distanceToPlayer = Vector3.Distance(
            transform.position,
            targetPosition
        );

        if (distanceToPlayer <= attackDistance)
        {
            currentState = GhostState.Attack;
            return;
        }

        MoveTowardsTarget(
            targetPosition,
            chaseSpeed,
            true
        );
    }

    private void UpdateAttack()
    {
        SetMovementAnimation(false, false);

        // 애니메이션 재생 중이면 제자리 대기
        if (attackTimer > 0f) return;

        if (playerInSafeRoom || !playerInDetectionRange)
        {
            currentState = GhostState.Patrol;
            SetDetectionRange(false);
            return;
        }

        Vector3 flatPos = player.position;
        flatPos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, flatPos);

        if (dist > attackDistance)
        {
            currentState = GhostState.Chase;
            return;
        }

        FaceTarget(player.position);
        PlayAttack();
        attackTimer = attackCooldown;
    }

    private void MoveTowardsTarget(
        Vector3 targetPosition,
        float speed,
        bool isRunning
    )
    {
        isWaiting = false;
        waitTimer = 0f;

        SetMovementAnimation(true, isRunning);
        FaceTarget(targetPosition);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f)
        {
            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void WaitAtWaypoint()
    {
        SetMovementAnimation(false, false);

        if (!isWaiting)
        {
            isWaiting = true;
            waitTimer = 0f;
        }

        waitTimer += Time.deltaTime;

        if (waitTimer >= waitTime)
        {
            currentWaypointIndex += waypointDirection;
            if (currentWaypointIndex >= waypoints.Length - 1 || currentWaypointIndex <= 0)
                waypointDirection *= -1;

            isWaiting = false;
            waitTimer = 0f;
        }
    }

    private void SetMovementAnimation(
        bool isMoving,
        bool isRunning
    )
    {
        nurseAnimator.SetBool(IsMovingHash, isMoving);
        nurseAnimator.SetBool(
            IsRunningHash,
            isMoving && isRunning
        );
    }

    private void PlayAttack()
    {
        nurseAnimator.ResetTrigger(AttackHash);
        nurseAnimator.SetTrigger(AttackHash);
    }

    private void SetDetectionRange(bool alerted)
    {
        if (detectionCollider != null)
            detectionCollider.radius = alerted ? baseDetectionRadius * 2f : baseDetectionRadius;
    }

    public void OnAttackHit()
    {
        StartCoroutine(GameOverAfterAnimation());
    }

    private IEnumerator GameOverAfterAnimation()
    {
        yield return new WaitForSeconds(gameOverDelay);
        GameManager.Instance.SetState(GameManager.GameState.GameOver);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerInDetectionRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerInDetectionRange = false;
    }

    public void SetPlayerInSafeRoom(bool isInSafeRoom)
    {
        playerInSafeRoom = isInSafeRoom;

        if (isInSafeRoom)
        {
            currentState = GhostState.Patrol;
            SetMovementAnimation(false, false);
        }
    }
}