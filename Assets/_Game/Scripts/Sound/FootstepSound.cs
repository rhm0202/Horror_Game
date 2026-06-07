using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSound : MonoBehaviour
{
    [Header("플레이어 참조")]
    [SerializeField] private Transform player;

    [Header("발소리 클립")]
    [SerializeField] private AudioClip[] footstepClips;

    [Header("설정")]
    [SerializeField] private float stepInterval = 0.4f;
    [SerializeField] private float moveThreshold = 0.05f;

    private AudioSource audioSource;
    private Vector3 lastPlayerPos;
    private float stepTimer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (player != null)
            lastPlayerPos = player.position;
    }

    void Update()
    {
        if (player == null) return;

        // 이전 프레임과 현재 프레임의 위치 차이로 이동 감지
        float moved = Vector3.Distance(player.position, lastPlayerPos);
        bool isMoving = moved > moveThreshold * Time.deltaTime;
        lastPlayerPos = player.position;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}